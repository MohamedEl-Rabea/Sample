using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Moj.CMS.Application.AppServices.Case.Queries.GetEffectsAndDiscount;
using Moj.CMS.Application.AppServices.Claims.Queries;
using Moj.CMS.Application.AppServices.Claims.Queries.Dtos;
using Moj.CMS.Application.AppServices.Party.Queries.Dtos;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Application.Models;
using Moj.CMS.Domain.Aggregates.Case;
using Moj.CMS.Domain.Aggregates.Claim;
using Moj.CMS.Domain.Aggregates.Court;
using Moj.CMS.Domain.Aggregates.Party;
using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Domain.Shared.LookupModels;
using Moj.CMS.Domain.Shared.Values;
using Moj.CMS.Infrastructure.Contexts;
using Moj.CMS.Shared;
using Moj.CMS.Shared.DTO;
using Moj.CMS.Shared.Extensions;
using Moj.CMS.Shared.Interfaces;
using Moj.CMS.Shared.Requests;
using Moj.CMS.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moj.CMS.Infrastructure.Queries
{
    public class ClaimsQueries : IClaimQueries
    {
        private readonly IQueryBuilderCreator<CMSDbContext> _queryBuilderCreator;
        private readonly IStringLocalizer<CMSLocalizer> _localizer;

        public ClaimsQueries(IStringLocalizer<CMSLocalizer> localizer, IQueryBuilderCreator<CMSDbContext> queryBuilderCreator)
        {
            _queryBuilderCreator = queryBuilderCreator;
            _localizer = localizer;
        }

        public async Task<PagedResult<GetAllClaimsDto>> GetAllAsync(PagedRequest<GetAllClaimsDto> request)
        {
            using (var queryBuilder = _queryBuilderCreator.Create())
            {
                var query = from claim in queryBuilder.Query<Claim>()
                            join user in queryBuilder.Query<User>() on claim.LastModifierUserId ?? claim.CreatorUserId equals user.Id
                            join claimStatus in queryBuilder.Query<ClaimFinancialStatus>() on (int)claim.ClaimStatus.FinancialStatus equals claimStatus.Id
                            join party in queryBuilder.Query<Party>() on claim.ComplaintPartyNumber equals party.PartyNumber
                            join cs in queryBuilder.Query<Case>() on claim.CaseNumber equals cs.CaseNumber
                            from cd in cs.CaseDetails.OrderByDescending(d => d.CreationTime).Take(1)
                            join judge in queryBuilder.Query<Judge>() on cd.JudgeCode equals judge.Code
                            join court in queryBuilder.Query<Court>() on cd.CourtCode equals court.Code
                            join division in queryBuilder.Query<Court>().SelectMany(c => c.Divisions)
                            on cd.CourtCode equals division.Code
                            select new GetAllClaimsDto
                            {
                                PartyIdentityNumber = party.CurrentIdentityNumber,
                                ClaimId = claim.Id,
                                CaseNumber = claim.CaseNumber,
                                CaseId = cs.Id,
                                JudgeName = judge.Name,
                                JudgeCode = judge.Code,
                                CourtName = court.Name,
                                CourtCode = court.Code,
                                DivisionName = division.Name,
                                DivisionCode = division.Code,
                                ClaimDateTime = claim.ClaimDateTime,
                                ClaimNumber = claim.Id.ToString(),
                                StatusName = claimStatus.Name,
                                StatusId = claimStatus.Id,
                                RemainingAmount = new MoneyDto { Value = claim.RemainingAmount.Value, CurrencyIso = claim.RemainingAmount.CurrencyIso },
                                RequiredAmount = new MoneyDto { Value = claim.RequiredAmount.Value, CurrencyIso = claim.RequiredAmount.CurrencyIso },
                                IsUpdate = claim.LastModificationTime.HasValue,
                                LastUpdate = claim.LastModificationTime ?? claim.CreationTime,
                                UpdatedBy = user.Name,
                            };

                var result = await query.ToPagedListAsync(request);
                return result;
            }
        }

        public async Task<IEnumerable<ClaimBasicDetailsDto>> GetCaseClaimsAsync(string caseNumber)
        {
            using var queryBuilder = _queryBuilderCreator.Create();

            var caseClaims = await (from claim in queryBuilder.Query<Claim>()
                                    join party in queryBuilder.Query<Party>() on claim.ComplaintPartyNumber equals party.PartyNumber
                                    where claim.CaseNumber == caseNumber
                                    select new ClaimBasicDetailsDto
                                    {
                                        ClaimNumber = claim.Id.ToString(),
                                        ComplaintPartyNumber = claim.ComplaintPartyNumber,
                                        ComplaintPartyName = party.FullName,
                                        RemainingAmount = MoneyDto.MapFromValueObject(claim.RemainingAmount),
                                        RequiredAmount = MoneyDto.MapFromValueObject(claim.RequiredAmount)
                                    }).ToListAsync();
            return caseClaims;
        }

        public async Task<IEnumerable<CaseClaimStatusDto>> GetCaseClaimsStatusAsync(string caseNumber)
        {
            using var queryBuilder = _queryBuilderCreator.Create();
            var result = await (from claim in queryBuilder.Query<Claim>()
                                where claim.CaseNumber == caseNumber
                                select new CaseClaimStatusDto
                                {
                                    ClaimId = claim.Id,
                                    ClaimStatus = claim.ClaimStatus.FinancialStatus
                                }).ToListAsync();
            return result;
        }

        public async Task<int> GetClaimIdByNumberAsync(string claimNumber)
        {
            using var queryBuilder = _queryBuilderCreator.Create();
            var claim = await queryBuilder.Query<Claim>().Where(c => c.Id.ToString() == claimNumber).Select(c => c.Id).FirstOrDefaultAsync();
            return claim;
        }

        public async Task<IEnumerable<GetEffectsAndDiscountDto>> GetClaimEffectsAsync(int caseId)
        {
            using var queryBuilder = _queryBuilderCreator.Create();
            var result = await (from caseAgg in queryBuilder.Query<Case>()
                                join claim in queryBuilder.Query<Claim>() on caseAgg.CaseNumber equals claim.CaseNumber
                                from claimHistory in claim.ClaimHistoryList
                                join effectType in queryBuilder.Query<FinancialEffectType>() on (int)claimHistory.EffectType equals effectType.Id
                                join user in queryBuilder.Query<User>() on claimHistory.CreatorUserId equals user.Id
                                where caseAgg.Id == caseId
                                select new GetEffectsAndDiscountDto
                                {
                                    ClaimNumber = claim.Id.ToString(),
                                    EffectType = effectType.Name,
                                    EffectTypeId = effectType.Id,
                                    CreationTime = claimHistory.CreationTime,
                                    NewTotalAmount = MoneyDto.MapFromValueObject(claimHistory.TotalAmountAfter),
                                    OldTotalAmount = MoneyDto.MapFromValueObject(claimHistory.TotalAmountBefore),
                                    EffectAmount = MoneyDto.MapFromValueObject(claimHistory.TotalAmountBefore.Subtract(claimHistory.TotalAmountAfter)),
                                    CreatorUser = user.Name
                                }).ToListAsync();

            return result;
        }

        public async Task<IEnumerable<string>> GetClaimAccusedPartyNumbersAsync(string claimNumber)
        {
            using var queryBuilder = _queryBuilderCreator.Create();

            var result = await queryBuilder.Query<Claim>()
                .Where(c => c.Id.ToString() == claimNumber)
                .SelectMany(c => c.ClaimDetailsList)
                .Select(cd => cd.PartyNumber)
                .ToListAsync();

            return result;
        }

        public async Task<MoneyDto> GetClaimTotalRemainingAmountAsync(string claimNumber)
        {
            using var queryBuilder = _queryBuilderCreator.Create();

            var result = await queryBuilder.Query<Claim>()
                .Where(c => c.Id.ToString() == claimNumber)
                .Select(c => c.TotalRemainingAmount)
                .FirstOrDefaultAsync();

            return new MoneyDto { Value = result.Value, CurrencyIso = result.CurrencyIso };
        }

        public async Task<IEnumerable<PartyAccountStatementDto>> GetPartyAccountStatement(int partyId)
        {
            var result = new List<PartyAccountStatementDto> {
                    new PartyAccountStatementDto{
                    FinancialTransactionNumber="001",
                    FinancialTransactionRef="120354697",
                    FinancialTransactionSource="???? ??????? ?????? ????",
                    FinalBalanceAmount=MoneyDto.Default,
                    FinancialTransactionTransferedAmount=MoneyDto.Default,
                    FinancialTransactionPaidAmount=MoneyDto.Default,
                    FinancialTransactionRemainingAmount=MoneyDto.Default,
                    FinancialTransactionType=FinancialTransactionTypeEnum.PaySadadInvoice,
                    FinancialTransactionDate=DateTime.UtcNow,
                    FinancialTransactionCaseNumber="134"
                }};
            return result.ToList();
        }
    }
}
