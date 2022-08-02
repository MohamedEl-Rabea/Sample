using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moj.CMS.Application.AppServices.Party.Queries;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Application.Models;
using Moj.CMS.Domain.Aggregates.Case;
using Moj.CMS.Domain.Aggregates.Claim;
using Moj.CMS.Domain.Aggregates.Claim.ValueObjects;
using Moj.CMS.Domain.Aggregates.Court;
using Moj.CMS.Domain.Aggregates.Party;
using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Domain.Shared.LookupModels;
using Moj.CMS.Infrastructure.Contexts;
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
    public class PartyQueries : IPartyQueries
    {
        private readonly IQueryBuilderCreator<CMSDbContext> _queryBuilderCreator;
        private readonly IMapper _mapper;

        public PartyQueries(IQueryBuilderCreator<CMSDbContext> queryBuilderCreator, IMapper mapper)

        {
            _queryBuilderCreator = queryBuilderCreator;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PartyBasicInfoDto>> GetPartiesBasicInfoByIdsAsync(IEnumerable<int> partiesIds)
        {
            using (var queryBuilder = _queryBuilderCreator.Create())
            {
                var partyInfo = await queryBuilder.Query<Party>()
                            .Where(p => partiesIds.Contains(p.Id))
                            .Select(p => new PartyBasicInfoDto { Name = p.FullName, Id = p.Id }).ToListAsync();
                return partyInfo;
            }
        }

        public async Task<IEnumerable<PartyBasicInfoDto>> GetPartiesBasicInfoByNumbersAsync(IEnumerable<string> partiesNumbers)
        {
            using (var queryBuilder = _queryBuilderCreator.Create())
            {
                var partyInfo = await (from party in queryBuilder.Query<Party>()
                                       from partyIdentity in party.PartyIdentities.Where(pi => pi.IsActive)
                                       where partiesNumbers.Contains(party.PartyNumber)
                                       select new PartyBasicInfoDto
                                       {
                                           Id = party.Id,
                                           Number = party.PartyNumber,
                                           Name = party.FullName,
                                           PartyIdentityTypeId = partyIdentity.PartyIdentityTypeId
                                       }).ToListAsync();
                return partyInfo;
            }
        }

        public async Task<int> GetPartyIdByPartyNumberAsync(string partyNumber)
        {
            using (var queryBuilder = _queryBuilderCreator.Create())
            {
                var partyId = await queryBuilder.Query<Party>()
                            .Where(p => p.PartyNumber == partyNumber)
                            .Select(p => p.Id)
                            .FirstOrDefaultAsync();
                return partyId;
            }
        }

        public async Task<IEnumerable<PartyListDto>> GetAllPartiesAsync()
        {
            using (var queryBuilder = _queryBuilderCreator.Create())
            {
                var parties = await queryBuilder.Query<Party>().ToListAsync();
                var result = _mapper.Map<IEnumerable<PartyListDto>>(parties);
                return result;
            }
        }

        public async Task<PartyBasicInfoDto> GetPartyIdByNumberAsync(string partyNumber)
        {
            using var queryBuilder = _queryBuilderCreator.Create();
            var partyInfo = await (from party in queryBuilder.Query<Party>()
                                   from partyIdentity in party.PartyIdentities.Where(pi => pi.IsActive)
                                   where party.PartyNumber == partyNumber
                                   select new PartyBasicInfoDto
                                   {
                                       Id = party.Id,
                                       Number = party.PartyNumber,
                                       Name = party.FullName,
                                       PartyIdentityTypeId = partyIdentity.PartyIdentityTypeId
                                   })
                .FirstOrDefaultAsync();
            return partyInfo;
        }

        public async Task<IEnumerable<int>> GetPartiesBasicInfoByIdentityNumbersAsync(IEnumerable<string> identitiesNumbers)
        {
            using (var queryBuilder = _queryBuilderCreator.Create())
            {
                var partyId = await queryBuilder.Query<Party>()
                            .Where(p => identitiesNumbers.Contains(p.CurrentIdentityNumber))
                            .Select(p => p.Id).ToListAsync();
                return partyId;
            }
        }

        public async Task<PagedResult<PartyListItemDto>> GetAllPartiesAsync(PagedRequest<PartyListItemDto> request)
        {
            using (var queryBuilder = _queryBuilderCreator.Create())
            {
                var allParties = await (from party in queryBuilder.Query<Party>()
                                        join user in queryBuilder.Query<User>() on party.LastModifierUserId ?? party.CreatorUserId equals user.Id
                                        from partyIdentity in party.PartyIdentities.Where(id => id.IsActive).Take(1)
                                        join partyType in queryBuilder.Query<PartyType>() on (int)party.PartyTypeId equals partyType.Id
                                        join nationality in queryBuilder.Query<Nationality>() on party.NationalityCode equals nationality.Code
                                        join partyIdentityType in queryBuilder.Query<PartyIdentityType>() on (int)partyIdentity.PartyIdentityTypeId equals partyIdentityType.Id
                                        select new PartyListItemDto
                                        {
                                            Id = party.Id,
                                            IdentityNumber = party.CurrentIdentityNumber,
                                            Number = party.PartyNumber,
                                            Name = party.FullName,
                                            NationalityText = nationality.Name,
                                            NationalityCode = nationality.Code,
                                            PartyTypeId = partyType.Id,
                                            PartyTypeText = partyType.Name,
                                            IdentityTypeText = partyIdentityType.Name,
                                            IdentityTypeId = partyIdentityType.Id,
                                            IsUpdate = party.LastModificationTime.HasValue,
                                            LastUpdate = party.LastModificationTime ?? party.CreationTime,
                                            UpdatedBy = user.Name,
                                            TotalCreditAmountValue = party.TotalCreditAmount.Value,
                                            TotalDebtAmountValue = party.TotalDebtAmount.Value,
                                            Currency = party.TotalCreditAmount.CurrencyIso
                                        }).ToPagedListAsync(request);

                await SetPartiesCaseCountAsync(allParties.Data, queryBuilder);
                return allParties;
            }
        }

        public async Task<PartyListItemDto> GetPartyBasicDetailsAsync(int partyId)
        {
            using (var queryBuilder = _queryBuilderCreator.Create())
            {
                var partyBasicInfo = (await (from party in queryBuilder.Query<Party>()
                                             join caseParty in queryBuilder.Query<Case>().SelectMany(c => c.CaseParties) on party.PartyNumber equals caseParty.PartyNumber
                                             join user in queryBuilder.Query<User>() on party.LastModifierUserId ?? party.CreatorUserId equals user.Id
                                             from partyIdentity in party.PartyIdentities.Where(id => id.IsActive).Take(1)
                                             join partyType in queryBuilder.Query<PartyType>() on (int)party.PartyTypeId equals partyType.Id
                                             join partyIdentityType in queryBuilder.Query<PartyIdentityType>() on (int)partyIdentity.PartyIdentityTypeId equals partyIdentityType.Id
                                             join location in queryBuilder.Query<PartyLocation>() on (int)party.PartyLocationId equals location.Id
                                             join nationality in queryBuilder.Query<Nationality>() on party.NationalityCode equals nationality.Code
                                             where party.Id == partyId
                                             select new
                                             {
                                                 Id = party.Id,
                                                 PartyNumber = party.PartyNumber,
                                                 Name = party.FullName,
                                                 IdentityTypeText = partyIdentityType.Name,
                                                 IdentityNumber = party.CurrentIdentityNumber,
                                                 DateOfBirth = party.DateOfBirth,
                                                 Gender = party.Gender,
                                                 PartyClassification = caseParty.PartyClassificationId,
                                                 NationalityText = nationality.Name,
                                                 LocationText = location.Name,
                                                 PartyTypeText = partyType.Name,
                                                 TotalCreditAmountValue = party.TotalCreditAmount.Value,
                                                 TotalDebtAmountValue = party.TotalDebtAmount.Value,
                                                 Currency = party.TotalCreditAmount.CurrencyIso
                                             }).ToListAsync()).GroupBy(cp => cp.PartyNumber,
                                              (key, grp) => new PartyListItemDto
                                              {
                                                  Id = partyId,
                                                  DateOfBirth = grp.Min(p => p.DateOfBirth),
                                                  Gender = grp.Min(p => p.Gender),
                                                  NationalityText = grp.Min(p => p.NationalityText),
                                                  LocationText = grp.Min(p => p.LocationText),
                                                  IdentityNumber = grp.Min(p => p.IdentityNumber),
                                                  Number = grp.Min(p => p.PartyNumber),
                                                  Name = grp.Min(p => p.Name),
                                                  PartyTypeText = grp.Min(p => p.PartyTypeText),
                                                  TotalCreditAmountValue = grp.Min(p => p.TotalCreditAmountValue),
                                                  TotalDebtAmountValue = grp.Min(p => p.TotalDebtAmountValue),
                                                  Currency = grp.Min(p => p.Currency),
                                                  IdentityTypeText = grp.Min(p => p.PartyTypeText),
                                                  CreditCaseCount = grp.Count(p => p.PartyClassification == PartyClassificationEnum.Requester),
                                                  DebtCaseCount = grp.Count(p => p.PartyClassification == PartyClassificationEnum.Respondent)
                                              }).FirstOrDefault();
                return partyBasicInfo;
            }
        }

        public async Task<PartyCaseListDto> GetPartyCasesAsync(int partyId)
        {

            using var queryBuilder = _queryBuilderCreator.Create();
            var partyCases = await (from party in queryBuilder.Query<Party>()
                                    from caseAgg in queryBuilder.Query<Case>()
                                    from caseParty in caseAgg.CaseParties
                                    where party.PartyNumber == caseParty.PartyNumber
                                    from caseDetails in caseAgg.CaseDetails.Where(cd => cd.IsCurrent).Take(1)
                                    join judge in queryBuilder.Query<Judge>() on caseDetails.JudgeCode equals judge.Code
                                    join court in queryBuilder.Query<Court>() on caseDetails.CourtCode equals court.Code
                                    join division in queryBuilder.Query<Court>().SelectMany(c => c.Divisions) on caseDetails.DivisionCode equals division.Code
                                    join type in queryBuilder.Query<CaseType>() on (int)caseAgg.CaseType equals type.Id
                                    join status in queryBuilder.Query<CaseStatus>() on (int)caseAgg.CaseStatus equals status.Id
                                    join partyType in queryBuilder.Query<PartyType>() on (int)party.PartyTypeId equals partyType.Id
                                    join role in queryBuilder.Query<PartyRole>() on (int)caseParty.PartyRoleId equals role.Id
                                    where party.Id == partyId
                                    orderby caseAgg.DatesInfo.ReceiveDate descending
                                    select new PartyCaseListItemDto
                                    {
                                        CaseId = caseAgg.Id,
                                        PartyNumber = party.PartyNumber,
                                        PartyClassification = caseParty.PartyClassificationId,
                                        CaseNumber = caseAgg.CaseNumber,
                                        ReceiveDate = caseAgg.DatesInfo.ReceiveDate,
                                        JudgeAcceptanceDate = caseAgg.DatesInfo.JudgeAcceptanceDate,
                                        CaseType = type.Name,
                                        CaseTypeId = (int)caseAgg.CaseType,
                                        CaseStatus = status.Name,
                                        CaseStatusId = (int)caseAgg.CaseStatus,
                                        Court = court.Name,
                                        CourtCode = court.Code,
                                        Division = division.Name,
                                        DivisionCode = division.Code,
                                        Judge = judge.Name,
                                        JudgeCode = judge.Code,
                                        IsUpdate = caseAgg.LastModificationTime.HasValue,
                                        LastUpdate = caseAgg.LastModificationTime ?? caseAgg.CreationTime,
                                        RequestersCount = caseAgg.CaseParties.Count(c => c.PartyClassificationId == PartyClassificationEnum.Requester),
                                        RespondentsCount = caseAgg.CaseParties.Count(c => c.PartyClassificationId == PartyClassificationEnum.Respondent),
                                        CaseBasicAmount = new MoneyDto { Value = caseAgg.CaseBasicAmount.Value, CurrencyIso = caseAgg.CaseBasicAmount.CurrencyIso },
                                        ApprovedAmount = new MoneyDto { Value = caseAgg.ApprovedAmount.Value, CurrencyIso = caseAgg.ApprovedAmount.CurrencyIso },
                                        TotalRequiredAmount = new MoneyDto { Value = caseAgg.TotalRequiredAmount.Value, CurrencyIso = caseAgg.TotalRequiredAmount.CurrencyIso },
                                        TotalRemainingAmount = new MoneyDto { Value = caseAgg.TotalRemainingAmount.Value, CurrencyIso = caseAgg.TotalRemainingAmount.CurrencyIso },
                                    }).ToListAsync();
            await SetPartyCasesAmountAsync(partyCases, queryBuilder);
            var accuesePartyCases = partyCases.Where(p => p.PartyClassification == PartyClassificationEnum.Respondent);
            var complaintPartyCases = partyCases.Where(p => p.PartyClassification == PartyClassificationEnum.Requester);
            var allPartyCase = new PartyCaseListDto
            {
                AccusedCaseList = accuesePartyCases,
                ComplaintCaseList = complaintPartyCases
            };
            return allPartyCase;
        }


        public async Task<PartyDebtsSummaryDto> GetPartyDebtsSummary(int partyId)
        {
            using var queryBuilder = _queryBuilderCreator.Create();

            var partyDebtList = await (from party in queryBuilder.Query<Party>()
                                       from caseAgg in queryBuilder.Query<Case>()
                                           //join caseParty in caseAgg.CaseParties // no work 
                                       from caseParty in caseAgg.CaseParties
                                       where party.PartyNumber == caseParty.PartyNumber &&
                                       caseParty.PartyClassificationId == PartyClassificationEnum.Respondent
                                       join claimDetails in queryBuilder.Query<ClaimDetails>()
                                       on party.PartyNumber equals claimDetails.PartyNumber
                                       where party.Id == partyId
                                       orderby caseAgg.DatesInfo.ReceiveDate descending
                                       select new
                                       {
                                           PartyId = party.Id,
                                           caseId = caseAgg.Id,
                                           TotalRemainingAmount = MoneyDto.MapFromValueObject(claimDetails.RequiredAmount), // To do
                                           TotalRequiredAmount = MoneyDto.MapFromValueObject(claimDetails.RequiredAmount),
                                           TotalPaidAmount = MoneyDto.Default  // To do  
                                       }).ToListAsync();

            var result = partyDebtList.GroupBy(p => p.PartyId, (id, partyDebtsSummary) => new PartyDebtsSummaryDto
            {
                PartyId = id,
                CasesCount = partyDebtsSummary.Select(c => c.caseId).Distinct().Count(),
                TotalDebtsAmount = new MoneyDto
                {
                    Value = partyDebtsSummary.Sum(c => c.TotalRequiredAmount.Value),
                    CurrencyIso = partyDebtsSummary.Select(c => c.TotalRequiredAmount.CurrencyIso).FirstOrDefault(),
                },
                TotalPaidAmount = new MoneyDto
                {
                    Value = partyDebtsSummary.Sum(c => c.TotalPaidAmount.Value),
                    CurrencyIso = partyDebtsSummary.Select(c => c.TotalPaidAmount.CurrencyIso).FirstOrDefault(),
                },
                TotalRemainingAmount = new MoneyDto
                {
                    Value = partyDebtsSummary.Sum(c => c.TotalRemainingAmount.Value),
                    CurrencyIso = partyDebtsSummary.Select(c => c.TotalRemainingAmount.CurrencyIso).FirstOrDefault(),
                },
            }).FirstOrDefault();
            return result;
        }

        public async Task<PartyCreditsSummaryDto> GetPartyCreditsSummary(int partyId)
        {
            using var queryBuilder = _queryBuilderCreator.Create();
            var partyCreditList = await (from party in queryBuilder.Query<Party>()
                                         join claim in queryBuilder.Query<Claim>()
                                         on party.PartyNumber equals claim.ComplaintPartyNumber
                                         where party.Id == partyId
                                         select new
                                         {
                                             PartyId = party.Id,
                                             CaseNumber = claim.CaseNumber,
                                             TotalCollectedAmount = claim.TotalRequiredAmount.Subtract(claim.TotalRemainingAmount),
                                             TotalRequiredAmount = new MoneyDto { Value = claim.TotalRequiredAmount.Value, CurrencyIso = claim.TotalRequiredAmount.CurrencyIso },
                                             TotalRemainingAmount = new MoneyDto { Value = claim.TotalRemainingAmount.Value, CurrencyIso = claim.TotalRemainingAmount.CurrencyIso },
                                         }).ToListAsync();

            var result = partyCreditList.GroupBy(p => p.PartyId, (id, partyCreditsSummary) => new PartyCreditsSummaryDto
            {
                PartyId = id,
                CasesCount = partyCreditsSummary.Select(c => c.CaseNumber).Distinct().Count(),
                TotalCreditsAmount = new MoneyDto
                {
                    Value = partyCreditsSummary.Sum(c => c.TotalRequiredAmount.Value),
                    CurrencyIso = partyCreditsSummary.Select(c => c.TotalRequiredAmount.CurrencyIso).FirstOrDefault(),
                },
                TotalCollectedAmount = new MoneyDto
                {
                    Value = partyCreditsSummary.Sum(c => c.TotalCollectedAmount.Value),
                    CurrencyIso = partyCreditsSummary.Select(c => c.TotalCollectedAmount.CurrencyIso).FirstOrDefault(),
                },
                TotalRemainingAmount = new MoneyDto
                {
                    Value = partyCreditsSummary.Sum(c => c.TotalRemainingAmount.Value),
                    CurrencyIso = partyCreditsSummary.Select(c => c.TotalRemainingAmount.CurrencyIso).FirstOrDefault(),
                },
            }).FirstOrDefault();
            return result;
        }

        private async Task SetPartiesCaseCountAsync(IEnumerable<PartyListItemDto> allParties, IQueryBuilder queryBuilder)
        {
            var partyNumbers = allParties.Select(p => p.Number);
            var query = await (from caseAgg in queryBuilder.Query<Case>()
                               from party in caseAgg.CaseParties
                               where partyNumbers.Contains(party.PartyNumber)
                               select new
                               {
                                   party.PartyNumber,
                                   party.PartyClassificationId
                               }).ToListAsync();
            var caseCountResult = query.GroupBy(q => new { q.PartyNumber, q.PartyClassificationId })
                .Select(qr => new
                {
                    qr.Key.PartyNumber,
                    qr.Key.PartyClassificationId,
                    CaseCount = qr.Count()
                });

            foreach (var party in allParties)
            {
                party.CreditCaseCount = caseCountResult.SingleOrDefault(c => c.PartyNumber == party.Number
                && c.PartyClassificationId == PartyClassificationEnum.Requester)?.CaseCount ?? 0;

                party.DebtCaseCount = caseCountResult.SingleOrDefault(c => c.PartyNumber == party.Number
                && c.PartyClassificationId == PartyClassificationEnum.Respondent)?.CaseCount ?? 0;
            }
        }
        private async Task SetPartyCasesAmountAsync(IEnumerable<PartyCaseListItemDto> allPartyCases, IQueryBuilder queryBuilder)
        {
            var casesNumbers = allPartyCases.Select(c => c.CaseNumber);
            var allcaseClaims = await (queryBuilder.Query<Claim>()
                                .Include(c => c.ClaimDetailsList)
                                .Where(c => casesNumbers.Contains(c.CaseNumber))
                                .ToListAsync());

            var complaintGroupedResult = GetComplaintGroupedResult(allcaseClaims, allPartyCases);
            var accusedGroupedResult = GetAccusedGroupedResult(allcaseClaims, allPartyCases);
            foreach (var partyCase in allPartyCases)
            {
                var accuesedResult = accusedGroupedResult.SingleOrDefault
                                                        (c => c.PartyNumber == partyCase.PartyNumber
                                                        && partyCase.CaseNumber == c.CaseNumber);

                var complaintResult = complaintGroupedResult.SingleOrDefault
                                                       (c => c.PartyNumber == partyCase.PartyNumber
                                                        && partyCase.CaseNumber == c.CaseNumber);

                partyCase.TotalPartyCaseAccuesedAmount = accuesedResult?.TotalPartyCaseAccuesedAmount ?? MoneyDto.Default;
                partyCase.TotalRemainingPartyCaseAccuesedAmount = accuesedResult?.TotalRemainingPartyCaseAccuesedAmount ?? MoneyDto.Default;

                partyCase.TotalPartyCaseComplaintAmount = complaintResult?.TotalPartyCaseComplaintAmount ?? MoneyDto.Default;
                partyCase.TotalPaidPartyCaseComplaintAmount = complaintResult?.TotalPaidPartyCaseComplaintAmount ?? MoneyDto.Default;
                partyCase.TotalRemainingPartyCaseComplaintAmount = complaintResult?.TotalRemainingPartyCaseComplaintAmount ?? MoneyDto.Default;
            }

        }

        private IEnumerable<PartyAmountResult> GetComplaintGroupedResult(IEnumerable<Claim> allcaseClaims, IEnumerable<PartyCaseListItemDto> allPartyCases)
        {

            var complaintResult = from claim in allcaseClaims
                                  join partyCase in allPartyCases
                                  on claim.ComplaintPartyNumber equals partyCase.PartyNumber
                                  where claim.CaseNumber == partyCase.CaseNumber
                                  select new
                                  {
                                      claimId = claim.Id,
                                      caseNumber = partyCase.CaseNumber,
                                      partyNumber = partyCase.PartyNumber,
                                      TotalRemainingAmount = new MoneyDto { Value = claim.TotalRemainingAmount.Value, CurrencyIso = claim.TotalRemainingAmount.CurrencyIso },
                                      TotalRequiredAmount = new MoneyDto { Value = claim.TotalRequiredAmount.Value, CurrencyIso = claim.TotalRequiredAmount.CurrencyIso },
                                      TotalPaidAmount = claim.TotalRequiredAmount.Subtract(claim.TotalRemainingAmount)
                                  };

            var complaintGroupedResult = complaintResult.GroupBy(p =>
                                        new { p.caseNumber, p.partyNumber }
                                            , (key, grp) => new PartyAmountResult
                                            {
                                                CaseNumber = key.caseNumber,
                                                CaseClaimCount = grp.Select(c => c.claimId).Count(),
                                                PartyNumber = key.partyNumber,
                                                TotalPartyCaseComplaintAmount = new MoneyDto(grp.Min(c => c.TotalRequiredAmount.CurrencyIso), grp.Sum(c => c.TotalRequiredAmount.Value)),
                                                TotalRemainingPartyCaseComplaintAmount = new MoneyDto(grp.Min(c => c.TotalRemainingAmount.CurrencyIso), grp.Sum(c => c.TotalRemainingAmount.Value)),
                                            });

            return complaintGroupedResult;
        }

        private IEnumerable<PartyAmountResult> GetAccusedGroupedResult(IEnumerable<Claim> allcaseClaims, IEnumerable<PartyCaseListItemDto> allPartyCases)
        {
            var accuesedResult = from claim in allcaseClaims
                                 from claimDetails in claim.ClaimDetailsList
                                 join partyCase in allPartyCases
                                 on claimDetails.PartyNumber equals partyCase.PartyNumber
                                 where claim.CaseNumber == partyCase.CaseNumber
                                 select new
                                 {
                                     claimId = claim.Id,
                                     caseNumber = partyCase.CaseNumber,
                                     partyNumber = partyCase.PartyNumber,
                                     TotalRemainingAmount = claimDetails.RequiredAmount.Subtract(claimDetails.BillingAmount),// To do need revisit
                                     TotalRequiredAmount = new MoneyDto { Value = claimDetails.RequiredAmount.Value, CurrencyIso = claimDetails.RequiredAmount.CurrencyIso },
                                     TotalPaidAmount = new MoneyDto { Value = claimDetails.BillingAmount.Value, CurrencyIso = claimDetails.BillingAmount.CurrencyIso },
                                 };

            var accuesdGroupedResult = accuesedResult.GroupBy(p =>
                                        new { p.caseNumber, p.partyNumber }, (key, grp) => new PartyAmountResult
                                        {
                                            CaseNumber = key.caseNumber,
                                            CaseClaimCount = grp.Select(c => c.claimId).Count(),
                                            PartyNumber = key.partyNumber,
                                            TotalPartyCaseAccuesedAmount = new MoneyDto(grp.Min(c => c.TotalRequiredAmount.CurrencyIso), grp.Sum(c => c.TotalRequiredAmount.Value)),
                                            TotalRemainingPartyCaseAccuesedAmount = new MoneyDto(grp.Min(c => c.TotalRemainingAmount.CurrencyIso), grp.Sum(c => c.TotalRemainingAmount.Value)),
                                        });

            return accuesdGroupedResult;
        }
    }
}