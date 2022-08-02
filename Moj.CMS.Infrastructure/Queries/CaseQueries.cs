using Microsoft.EntityFrameworkCore;
using Moj.CMS.Application.AppServices.Case.Queries.Dtos;
using Moj.CMS.Application.AppServices.Case.Queries.GetAllCases;
using Moj.CMS.Application.AppServices.Case.Queries.GetCaseBasicDetails;
using Moj.CMS.Application.AppServices.Case.Queries.GetCaseClaims;
using Moj.CMS.Application.AppServices.Case.Queries.GetCaseEvents;
using Moj.CMS.Application.AppServices.Case.Queries.GetCaseParties;
using Moj.CMS.Application.AppServices.Case.Queries.GetCasePromissories;
using Moj.CMS.Application.AppServices.Case.Queries.GetCaseSummary;
using Moj.CMS.Application.AppServices.Case.Queries.GetDashboardData;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Application.Models;
using Moj.CMS.Domain.Aggregates.Case;
using Moj.CMS.Domain.Aggregates.CaseHistory;
using Moj.CMS.Domain.Aggregates.Claim;
using Moj.CMS.Domain.Aggregates.Court;
using Moj.CMS.Domain.Aggregates.Party;
using Moj.CMS.Domain.Aggregates.Promissory;
using Moj.CMS.Domain.Lookups;
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
    public class CaseQueries : ICaseQueries
    {
        private readonly IQueryBuilderCreator<CMSDbContext> _queryBuilderCreator;

        public CaseQueries(IQueryBuilderCreator<CMSDbContext> queryBuilderCreator)
        {
            _queryBuilderCreator = queryBuilderCreator;
        }

        public async Task<PagedResult<CaseListItemDto>> GetAllAsync(PagedRequest<CaseListItemDto> request)
        {
            using (var queryBuilder = _queryBuilderCreator.Create())
            {
                var query = from cs in queryBuilder.Query<Case>()
                            from caseDetails in cs.CaseDetails.OrderByDescending(d => d.CreationTime).Take(1)
                            join user in queryBuilder.Query<User>() on cs.LastModifierUserId ?? cs.CreatorUserId equals user.Id
                            join court in queryBuilder.Query<Court>() on caseDetails.CourtCode equals court.Code
                            join division in queryBuilder.Query<Court>().SelectMany(c => c.Divisions) on caseDetails.DivisionCode equals division.Code
                            join judge in queryBuilder.Query<Judge>() on caseDetails.JudgeCode equals judge.Code
                            join type in queryBuilder.Query<CaseType>() on (int)cs.CaseType equals type.Id
                            join status in queryBuilder.Query<CaseStatus>() on (int)cs.CaseStatus equals status.Id
                            select new CaseListItemDto
                            {
                                Id = cs.Id,
                                CaseNumber = cs.CaseNumber,
                                ReceiveDate = cs.DatesInfo.ReceiveDate,
                                JudgeAcceptanceDate = cs.DatesInfo.JudgeAcceptanceDate,
                                CaseType = type.Name,
                                CaseTypeId = (int)cs.CaseType,
                                CaseStatus = status.Name,
                                CaseStatusId = (int)cs.CaseStatus,
                                Court = court.Name,
                                CourtCode = court.Code,
                                Division = division.Name,
                                DivisionCode = division.Code,
                                Judge = judge.Name,
                                JudgeCode = judge.Code,
                                IsUpdate = cs.LastModificationTime.HasValue,
                                LastUpdate = cs.LastModificationTime ?? cs.CreationTime,
                                UpdatedBy = user.Name,
                                RequestersCount = cs.CaseParties.Count(c => c.PartyClassificationId == PartyClassificationEnum.Requester),
                                RespondentsCount = cs.CaseParties.Count(c => c.PartyClassificationId == PartyClassificationEnum.Respondent),
                                TotalRequiredAmount = new MoneyDto { Value = cs.TotalRequiredAmount.Value, CurrencyIso = cs.TotalRequiredAmount.CurrencyIso },
                                TotalRemainingAmount = new MoneyDto { Value = cs.TotalRemainingAmount.Value, CurrencyIso = cs.TotalRemainingAmount.CurrencyIso },
                            };

                var result = await query.ToPagedListAsync(request);
                return result;
            }
        }

        public async Task<IEnumerable<GetCasePromissoriesDto>> GetCasePromissoriesAsync(int caseId)
        {
            using var queryBuilder = _queryBuilderCreator.Create();
            var query = await (from caseAgg in queryBuilder.Query<Case>()
                               from casePromissory in caseAgg.CasePromissories
                               join promissory in queryBuilder.Query<Promissory>() on casePromissory.PromissoryNumber equals promissory.Number
                               join promissoryType in queryBuilder.Query<PromissoryType>() on (int)promissory.TypeId equals promissoryType.Id
                               where caseAgg.Id == caseId
                               select new
                               {
                                   PromissoryNumber = promissory.Number,
                                   PromissoryTypeName = promissoryType.Name,
                                   PromissoryTypeDesription = promissoryType.Description,
                                   PromissoryTypeId = promissoryType.Id
                               }).ToListAsync();

            var casePromissoriesInfo = query.GroupBy(c => new
            {
                c.PromissoryTypeName,
                c.PromissoryTypeId,
                c.PromissoryTypeDesription
            }, (promissoryType, promissories) => new GetCasePromissoriesDto
            {
                Count = promissories.Count(),
                PromissoriesNumbers = string.Join(", ", promissories.Select(p => p.PromissoryNumber)),
                PromissoryTypeId = promissoryType.PromissoryTypeId,
                PromissoryTypeName = promissoryType.PromissoryTypeName,
                PromissoryTypeDesription = promissoryType.PromissoryTypeDesription
            });

            return casePromissoriesInfo;
        }

        //TODO: Needs review
        public async Task<CaseBasicDetailsDto> GetCaseBasicDetailsAsync(int caseId)
        {
            using var queryBuilder = _queryBuilderCreator.Create();

            var caseBasicDetailsList = await (from caseAgg in queryBuilder.Query<Case>()
                                              from caseDetails in caseAgg.CaseDetails
                                              from casePromissory in caseAgg.CasePromissories.DefaultIfEmpty()
                                              join promissory in queryBuilder.Query<Promissory>() on casePromissory.PromissoryNumber equals promissory.Number into nullablePromsGrp
                                              join court in queryBuilder.Query<Court>() on caseDetails.CourtCode equals court.Code
                                              join judge in queryBuilder.Query<Judge>() on caseDetails.JudgeCode equals judge.Code
                                              join division in queryBuilder.Query<Court>().SelectMany(c => c.Divisions) on caseDetails.DivisionCode equals division.Code
                                              join claim in queryBuilder.Query<Claim>() on caseAgg.CaseNumber equals claim.CaseNumber into claims
                                              from newClaim in claims.DefaultIfEmpty()
                                              where caseAgg.Id == caseId
                                              select new
                                              {
                                                  CaseNumber = caseAgg.CaseNumber,
                                                  CourtName = court.Name,
                                                  JudgeName = judge.Name,
                                                  MahasaJudgeName = judge.Name,
                                                  DivisionName = division.Name,
                                                  Amount = ((decimal?)newClaim.RequiredAmount.Value) ?? 0,
                                                  CurrencyIso = newClaim.RequiredAmount.CurrencyIso ?? "",
                                                  ClaimNumber = ((int?)newClaim.Id) ?? 0
                                              }).ToListAsync();

            var caseBasicDetails = caseBasicDetailsList.GroupBy(cd => cd.CaseNumber,
                (caseNumber, caseDetails) => new CaseBasicDetailsDto
                {
                    CaseNumber = caseNumber,
                    CourtName = caseDetails.First().CourtName,
                    DivisionName = caseDetails.First().DivisionName,
                    JudgeName = caseDetails.First().JudgeName,
                    MahasaJudgeName = caseDetails.First().MahasaJudgeName,
                    Amount = new MoneyDto
                    {
                        Value = caseDetails.GroupBy(d => d.ClaimNumber).Select(g => g.First().Amount).Sum(),
                        CurrencyIso = caseDetails.First().CurrencyIso
                    }
                }).FirstOrDefault();

            return caseBasicDetails;
        }

        public async Task<CaseSummaryDto> GetCaseSummaryAsync(int caseId)
        {
            var caseSummary = new CaseSummaryDto();

            using var queryBuilder = _queryBuilderCreator.Create();

            caseSummary.CaseDetails = (await (from caseAgg in queryBuilder.Query<Case>()
                                              join caseType in queryBuilder.Query<CaseType>() on (int)caseAgg.CaseType equals caseType.Id
                                              join caseStatus in queryBuilder.Query<CaseStatus>() on (int)caseAgg.CaseStatus equals caseStatus.Id
                                              where caseAgg.Id == caseId
                                              select new
                                              {
                                                  caseAgg.CaseNumber,
                                                  CaseType = caseType.Name,
                                                  CaseStatus = caseStatus.Name,
                                                  caseAgg.DatesInfo.ReceiveDate,
                                                  StatusId = caseStatus.Id,
                                                  TypeId = caseType.Id,
                                                  caseAgg.CaseParties,
                                                  caseAgg.CasePromissories,
                                                  caseAgg.CloseDate,
                                              }).ToListAsync())
                                                .Select(d => new DetailsDto
                                                {
                                                    CaseNumber = d.CaseNumber,
                                                    CaseType = d.CaseType,
                                                    CaseStatus = d.CaseStatus,
                                                    ReceiveDate = d.ReceiveDate,
                                                    StatusId = d.StatusId,
                                                    TypeId = d.TypeId,
                                                    PartiesCount = d.CaseParties.Count,
                                                    PromissoriesCount = d.CasePromissories.Count,
                                                    CloseDate = d.CloseDate,
                                                }).FirstOrDefault();

            if (caseSummary.CaseDetails == null)
                return caseSummary;

            caseSummary.CaseDetails.CaseEventsCount = await queryBuilder.Query<CaseHistory>()
                .Where(h => h.CaseNumber == caseSummary.CaseDetails.CaseNumber)
                .CountAsync();

            var financialDetailsQuery = from caseAgg in queryBuilder.Query<Case>()
                                        join claim in queryBuilder.Query<Claim>() on caseAgg.CaseNumber equals claim.CaseNumber
                                        where caseAgg.Id == caseId
                                        select new
                                        {
                                            caseAgg.Id,
                                            CurrentAmount = claim.RequiredAmount.Value,
                                            RemainingAmount = claim.RemainingAmount.Value,
                                            claim.RequiredAmount.CurrencyIso
                                        };

            caseSummary.FinancialDetails = await financialDetailsQuery.GroupBy(c => new { c.Id, c.CurrencyIso },
                                                (claim, group) => new FinancialDto
                                                {
                                                    CurrentAmount = new MoneyDto
                                                    {
                                                        Value = group.Sum(g => g.CurrentAmount),
                                                        CurrencyIso = claim.CurrencyIso
                                                    },
                                                    RemainingAmount = new MoneyDto
                                                    {
                                                        Value = group.Sum(g => g.RemainingAmount),
                                                        CurrencyIso = claim.CurrencyIso
                                                    },
                                                    ClaimsCount = group.Count()
                                                }).FirstOrDefaultAsync();
            return caseSummary;
        }

        public async Task<IEnumerable<CasePartyRolesInfo>> GetPartiesInfoByCaseNumberAsync(string caseNumber)
        {
            using var queryBuilder = _queryBuilderCreator.Create();

            var partiesNumbers = await (from caseAgg in queryBuilder.Query<Case>()
                                        from caseParty in caseAgg.CaseParties
                                        join party in queryBuilder.Query<Party>() on caseParty.PartyNumber equals party.PartyNumber
                                        where caseAgg.CaseNumber == caseNumber
                                        select new CasePartyRolesInfo { PartyNumber = party.PartyNumber, PartyRole = caseParty.PartyRoleId }).ToListAsync();
            return partiesNumbers;
        }

        public async Task<CasePartiesDto> GetCasePartiesAsync(int caseId)
        {
            var caseParties = new CasePartiesDto();

            using var queryBuilder = _queryBuilderCreator.Create();

            var partiesList = await (from caseAgg in queryBuilder.Query<Case>()
                                     from caseParty in caseAgg.CaseParties
                                     join party in queryBuilder.Query<Party>() on caseParty.PartyNumber equals party.PartyNumber
                                     join type in queryBuilder.Query<PartyType>() on (int)party.PartyTypeId equals type.Id
                                     join role in queryBuilder.Query<PartyRole>() on (int)caseParty.PartyRoleId equals role.Id
                                     where caseAgg.Id == caseId
                                     select new PartyDto
                                     {
                                         Number = party.PartyNumber,
                                         Name = party.FullName,
                                         Type = type.Name,
                                         Role = role.Name,
                                         RoleType = caseParty.PartyRoleId
                                     }).ToListAsync();

            caseParties.Parties = partiesList;
            caseParties.Summary = new PartyRoleSummary
            {
                ComplaintCount = partiesList.Count(p => PartyRoleLookup.Find(p.RoleType).IsComplaint),
                AccusedCount = partiesList.Count(p => !PartyRoleLookup.Find(p.RoleType).IsComplaint)
            };
            return caseParties;
        }

        public async Task<IEnumerable<CaseEventsDto>> GetCaseEventsAsync(int caseId)
        {
            using var queryBuilder = _queryBuilderCreator.Create();
            var caseEvents = await (from caseAgg in queryBuilder.Query<Case>()
                                    join caseHistory in queryBuilder.Query<CaseHistory>() on caseAgg.CaseNumber equals caseHistory.CaseNumber
                                    join caseOperation in queryBuilder.Query<CaseOperation>() on (int)caseHistory.Operation equals caseOperation.Id
                                    join user in queryBuilder.Query<User>() on caseHistory.CreatorUserId equals user.Id
                                    where caseAgg.Id == caseId
                                    select new CaseEventsDto
                                    {
                                        Date = caseHistory.OperationDateTime,
                                        OperationId = caseOperation.Id,
                                        Operation = caseOperation.Name,
                                        Details = caseHistory.Details.CurrentCultureText,
                                        UserName = user.Name
                                    }).OrderByDescending(e => e.Date).ToListAsync();

            return caseEvents;
        }

        public async Task<int> GetCaseIdByCaseNumberAsync(string caseNumber)
        {
            using (var queryBuilder = _queryBuilderCreator.Create())
            {
                var caseId = await queryBuilder.Query<Case>()
                            .Where(p => p.CaseNumber == caseNumber)
                            .Select(p => p.Id).FirstOrDefaultAsync();
                return caseId;
            }
        }

        public async Task<List<string>> GetCaseNumbersByPartyIdAsync(int partyId)
        {
            using (var queryBuilder = _queryBuilderCreator.Create())
            {
                var caseNumbers = await (from caseAgg in queryBuilder.Query<Case>()
                                         from caseParty in caseAgg.CaseParties
                                         join party in queryBuilder.Query<Party>() on caseParty.PartyNumber equals party.PartyNumber
                                         where party.Id == partyId
                                         select caseAgg.CaseNumber).ToListAsync();
                return caseNumbers;
            }
        }

        public async Task<List<string>> GetCaseNumbersByPromissoryNumberAsync(string promissoryNumber)
        {
            using (var queryBuilder = _queryBuilderCreator.Create())
            {
                var caseNumbers = await (from caseAgg in queryBuilder.Query<Case>()
                                         from casePromosory in caseAgg.CasePromissories
                                         where casePromosory.PromissoryNumber == promissoryNumber
                                         select caseAgg.CaseNumber).ToListAsync();
                return caseNumbers;
            }
        }

        public async Task<SummaryDto> GetDashboardSummaryAsync()
        {
            SummaryDto summaryDto = new SummaryDto();

            using var queryBuilder = _queryBuilderCreator.Create();

            summaryDto.TodayCasesCount = await queryBuilder.Query<Case>()
                .Where(c => c.DatesInfo.ReceiveDate.Date == DateTime.Now.Date)
                .CountAsync();

            var todayClaims = await (from claim in queryBuilder.Query<Claim>()
                                     from claimHistory in claim.ClaimHistoryList.DefaultIfEmpty()
                                     where claim.ClaimDateTime.Date == DateTime.Now.Date
                                     select new
                                     {
                                         claim.Id,
                                         claim.RequiredAmount.Value,
                                         claim.RequiredAmount.CurrencyIso,
                                         AdjustmentReason = claimHistory == default ? 0 : claimHistory.EffectType
                                     }).ToListAsync();

            summaryDto.TodayClaims = todayClaims.GroupBy(c => c.Id).Select(c => c.First()).GroupBy(c =>
                   c.CurrencyIso, (iso, amounts) => new MoneyDto
                   {
                       CurrencyIso = iso,
                       Value = amounts.Sum(c => c.Value)
                   }).FirstOrDefault() ?? MoneyDto.Default;

            summaryDto.Reports = todayClaims.Count(c => c.AdjustmentReason == FinancialEffectTypeEnum.WaiverRecord);
            return summaryDto;
        }

        public async Task<IEnumerable<CaseDto>> GetLastCasesAsync()
        {
            using var queryBuilder = _queryBuilderCreator.Create();
            var cases = await (from caseAgg in queryBuilder.Query<Case>()
                               from caseDetails in caseAgg.CaseDetails
                               join court in queryBuilder.Query<Court>() on caseDetails.CourtCode equals court.Code
                               join division in queryBuilder.Query<Court>().SelectMany(c => c.Divisions) on caseDetails.DivisionCode equals division.Code
                               orderby caseAgg.DatesInfo.ReceiveDate descending
                               select new CaseDto
                               {
                                   CaseId = caseAgg.Id,
                                   CaseNumber = caseAgg.CaseNumber,
                                   Date = caseAgg.DatesInfo.ReceiveDate.FormatDateTime(),
                                   CourtName = court.Name,
                                   DivisionName = division.Name
                               }).Take(10).ToListAsync();

            return cases;
        }

        public async Task<GetCaseClaimsDto> GetCaseClaimsAsync(int caseId)
        {
            var result = new GetCaseClaimsDto();
            using var queryBuilder = _queryBuilderCreator.Create();
            var caseClaimsQuery = await (from claim in queryBuilder.Query<Claim>()
                                         join caseAgg in queryBuilder.Query<Case>() on claim.CaseNumber equals caseAgg.CaseNumber
                                         join ComplaintParty in queryBuilder.Query<Party>() on claim.ComplaintPartyNumber equals ComplaintParty.PartyNumber
                                         from claimDetails in claim.ClaimDetailsList
                                         join accusedparty in queryBuilder.Query<Party>() on claimDetails.PartyNumber equals accusedparty.PartyNumber
                                         where caseAgg.Id == caseId
                                         select new
                                         {
                                             ClaimNumber = claim.Id.ToString(),
                                             ComplaintName = ComplaintParty.FullName,
                                             RemainingAmount = claim.RemainingAmount,
                                             RequiredAmount = claim.RequiredAmount,
                                             AdjustmentHistoryData = claim.ClaimHistoryList,
                                             ClaimDetailsHistory = claimDetails.ClaimDetailsHistoryList,
                                             AccusedPartyNumber = accusedparty.PartyNumber,
                                             AccusedName = accusedparty.FullName,
                                             AccusedRequiredAmount = claimDetails.RequiredAmount,
                                             BillingAmount = claimDetails.BillingAmount,
                                         }).ToListAsync();

            result.ClaimList = caseClaimsQuery
                .GroupBy(query => query.ClaimNumber,
                    (claimNumber, claimDetails) => new ClaimDto
                    {
                        ClaimNumber = claimNumber,
                        ComplaintPartyName = claimDetails.FirstOrDefault().ComplaintName,
                        RequiredAmount = MoneyDto.MapFromValueObject(claimDetails.FirstOrDefault().RequiredAmount),
                        RemainingAmount = MoneyDto.MapFromValueObject(claimDetails.FirstOrDefault().RemainingAmount),
                        ClaimDetails = claimDetails.Select(d => new ClaimDetailsDto
                        {
                            PartyName = d.AccusedName,
                            RequiredAmount = MoneyDto.MapFromValueObject(d.AccusedRequiredAmount),
                            BillingAmount = MoneyDto.MapFromValueObject(d.BillingAmount),
                            CollectedByReports = new MoneyDto
                            {
                                Value = d.ClaimDetailsHistory.Where(h => h.PartyNumber == d.AccusedPartyNumber).Sum(h => h.OldRequiredAmount.Value - h.NewRequiredAmount.Value),
                                CurrencyIso = d.ClaimDetailsHistory.FirstOrDefault(h => h.PartyNumber == d.AccusedPartyNumber)?.OldRequiredAmount.CurrencyIso,
                            }
                        }).ToList()
                    }).ToList();

            return result;
        }

        public async Task<IEnumerable<CaseClaimAdjustmentChannelDto>> GetCaseAdjustmentChannelsAsync(int caseId)
        {
            using var queryBuilder = _queryBuilderCreator.Create();
            var result = (await (from claim in queryBuilder.Query<Claim>()
                                 from claimHistory in claim.ClaimHistoryList
                                 join caseAgg in queryBuilder.Query<Case>() on claim.CaseNumber equals caseAgg.CaseNumber
                                 join reason in queryBuilder.Query<FinancialEffectType>() on (int)claimHistory.EffectType equals reason.Id
                                 where caseAgg.Id == caseId
                                 select new
                                 {
                                     ClaimNumber = claim.Id.ToString(),
                                     claimHistory.EffectType,
                                     AdjustmentReasonString = reason.Name,
                                     claimHistory.TotalAmountBefore,
                                     claimHistory.TotalAmountAfter,
                                     AdjustmentDate = claimHistory.CreationTime
                                 }).ToListAsync())
                                 .GroupBy(c => c.EffectType, (reason, adjustmentGroup) => new CaseClaimAdjustmentChannelDto
                                 {
                                     AdjustmentReason = reason,
                                     AdjustmentReasonString = adjustmentGroup.Max(j => j.AdjustmentReasonString),
                                     AdjustmentDetails = adjustmentGroup.Select(g => new CollectionChannelsDetailsDto
                                     {
                                         ClaimNumber = g.ClaimNumber,
                                         NewAmount = MoneyDto.MapFromValueObject(g.TotalAmountAfter),
                                         OldAmount = MoneyDto.MapFromValueObject(g.TotalAmountBefore),
                                         ReportDate = g.AdjustmentDate
                                     })
                                 });
            return result;
        }
    }
}