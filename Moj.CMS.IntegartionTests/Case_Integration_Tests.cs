using Moj.CMS.Application.AppServices.Case.Commands.AddCase;
using Moj.CMS.Application.AppServices.Case.Commands.AddCaseParty;
using Moj.CMS.Application.AppServices.Case.Commands.UpdateCaseCourtDetailsCommand;
using Moj.CMS.Application.AppServices.Case.Queries;
using Moj.CMS.Application.AppServices.Case.Queries.GetAllCases;
using Moj.CMS.Application.AppServices.Claims.Commands.CreateFinancialClaim;
using Moj.CMS.Application.AppServices.Party.Commands.AddParty;
using Moj.CMS.Application.AppServices.Promissory.Dtos;
using Moj.CMS.Domain.Aggregates.Party;
using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Domain.Shared.Values;
using Moj.CMS.IntegartionTests.Helpers;
using Moj.CMS.Shared.DTO;
using Moj.CMS.Shared.Requests;
using Moj.CMS.Shared.Wrapper;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using Case = Moj.CMS.Domain.Aggregates.Case.Case;

namespace Moj.CMS.IntegartionTests
{
    public class Case_Integration_Tests : CmsIntegrationTestBase<Case_Integration_Tests>
    {

        public Case_Integration_Tests(WebApplicationTestHostFactoryFixture<Case_Integration_Tests> host) : base(host)
        {
        }

        [Fact]
        public async Task Should_Create_Case_Successfully_Api()
        {
            // Arrange
            var caseListInput = GetCaseFullDetailsDtoList();

            // Act
            var response = await PostAsync($"api/case", caseListInput);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.Created);
            UsingDbContext(dbContext =>
            {
                var inputCase = caseListInput.First();
                var createdCase = dbContext.Cases.FirstOrDefault();
                createdCase.ShouldNotBeNull();
                createdCase.CaseNumber.ShouldBe(inputCase.BasicDetails.CaseNumber);
                createdCase.ApprovedAmount.ValueEquals(inputCase.BasicDetails.RequiredAmount.ToValueObject()).ShouldBeTrue();
                createdCase.CaseBasicAmount.ValueEquals(inputCase.BasicDetails.CaseBasicAmount.ToValueObject()).ShouldBeTrue();
                var promissoryNumber = createdCase.CasePromissories.Single().PromissoryNumber;
                var requesterParty = inputCase.Requesters.First();
                var respondentParty = inputCase.Respondents.First();

                var createdRequesterCaseParty = createdCase.CaseParties.First(p => p.PartyNumber == requesterParty.Details.PartyNumber);
                createdRequesterCaseParty.IsApplicant.ShouldBe(requesterParty.IsApplicant);
                createdRequesterCaseParty.PartyClassificationId.ShouldBe(PartyClassificationEnum.Requester);
                createdRequesterCaseParty.PromissoryNumber.ShouldBe(promissoryNumber);

                var createdRespondentCaseParty = createdCase.CaseParties.First(p => p.PartyNumber == respondentParty.Details.PartyNumber);
                createdRespondentCaseParty.IsApplicant.ShouldBe(respondentParty.IsApplicant);
                createdRespondentCaseParty.PartyClassificationId.ShouldBe(PartyClassificationEnum.Respondent);
                createdRespondentCaseParty.PromissoryNumber.ShouldBe(promissoryNumber);

                var createdCaseHistory = dbContext.CaseHistory.FirstOrDefault();
                createdCaseHistory.ShouldNotBeNull();
                createdCaseHistory.CaseNumber.ShouldBe(createdCase.CaseNumber);
                createdCaseHistory.Operation.ShouldBe(CaseOperationEnum.CreateCase);
            });
        }

        [Fact]
        public async Task Should_Assign_Party_To_Case_Successfully_Api()
        {
            // Arrange
            var caseInfo = await CreateCaseAsync();
            var createdParty = await CreatePartyAsync();
            var assignPartiesInputDtoList = GetAssignPartyInputDto(caseInfo.Case, createdParty);

            // Act
            var response = await PostAsync($"api/case/party", assignPartiesInputDtoList);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.Created);
            UsingDbContext(dbContext =>
            {
                var existsCase = dbContext.Cases.FirstOrDefault(c => c.CaseNumber == caseInfo.Case.CaseNumber);
                existsCase.ShouldNotBeNull();

                var createdCaseParty = existsCase.CaseParties.FirstOrDefault(p => p.PartyNumber == createdParty.PartyNumber);
                var inputCaseParty = assignPartiesInputDtoList.First().Requesters.First();
                createdCaseParty.ShouldNotBeNull();
                createdCaseParty.PartyNumber.ShouldBe(createdParty.PartyNumber);
                createdCaseParty.PartyRoleId.ShouldBe(inputCaseParty.PartyRole);
                createdCaseParty.IsApplicant.ShouldBe(inputCaseParty.IsApplicant);
                createdCaseParty.PartyClassificationId.ShouldBe(PartyClassificationEnum.Requester);

                var createdCaseHistory = dbContext.CaseHistory.FirstOrDefault();
                createdCaseHistory.ShouldNotBeNull();
                createdCaseHistory.CaseNumber.ShouldBe(existsCase.CaseNumber);
                createdCaseHistory.Operation.ShouldBe(CaseOperationEnum.AddParty);
            });
        }

        [Fact]
        public async Task Should_Get_All_Cases_Paged_Successfully_Api()
        {
            // Arrange
            int count = 15;
            await CreateCaseListForQueryAsync(count);
            var requestFilter = GetCasesPagedApiRequest();

            // Act
            var response = await GetAsync<PagedResult<CaseListItemDto>>($"api/case/Get", requestFilter);

            // Assert
            var expectedPageCount = (int)Math.Ceiling((count / (double)requestFilter.PageSize));
            response.Succeeded.ShouldBeTrue();
            response.Data.TotalPages.ShouldBe(expectedPageCount);
            response.Data.TotalCount.ShouldBe(count);
            response.Data.PageSize.ShouldBe(requestFilter.PageSize);
            response.Data.CurrentPage.ShouldBe(requestFilter.PageNumber + 1);
            response.Data.Data.Count.ShouldBe(requestFilter.PageSize);
        }

        [Fact]
        public async Task Should_Get_All_Cases_Filtered_By_CaseNumber_Successfully()
        {
            // Arrange
            int count = 15;
            var createdCases = await CreateCaseListForQueryAsync(count);
            var createdCaseNumberFilter = createdCases.Take(1).Select(c => c.CaseNumber);
            var caseFilter = GetCaseFilter(caseNumbers: createdCaseNumberFilter.ToArray());
            var requestFilter = GetCasesPagedApiRequest(caseFilter);

            // Act
            var response = await GetAsync<PagedResult<CaseListItemDto>>($"api/case/Get", requestFilter);

            // Assert
            response.Succeeded.ShouldBeTrue();
            response.Data.TotalPages.ShouldBe(1);
            response.Data.TotalCount.ShouldBe(1);
            response.Data.PageSize.ShouldBe(requestFilter.PageSize);
            response.Data.CurrentPage.ShouldBe(requestFilter.PageNumber + 1);
            response.Data.Data.Count.ShouldBe(1);
            var filteredCase = response.Data.Data.FirstOrDefault();
            filteredCase.CaseNumber.ShouldBe(requestFilter.CaseFilter.CaseNumbers[0]);
        }

        [Fact]
        public async Task Should_Get_All_Cases_Filtered_By_Status_Successfully()
        {
            // Arrange
            int activeCount = 7, closedCount = 5;
            await CreateCaseListForQueryAsync(activeCount);
            await CreateCaseListForQueryAsync(closedCount, caseStatus: CaseStatusEnum.Closed);
            var caseFilter = GetCaseFilter(caseStatus: CaseStatusEnum.Active);
            var requestFilter = GetCasesPagedApiRequest(caseFilter);

            // Act
            var response = await GetAsync<PagedResult<CaseListItemDto>>($"api/case/Get", requestFilter);

            // Assert
            var expectedPageCount = (int)Math.Ceiling(activeCount / (double)requestFilter.PageSize);
            response.Succeeded.ShouldBeTrue();
            response.Data.TotalPages.ShouldBe(expectedPageCount);
            response.Data.TotalCount.ShouldBe(activeCount);
            response.Data.PageSize.ShouldBe(requestFilter.PageSize);
            response.Data.CurrentPage.ShouldBe(requestFilter.PageNumber + 1);
            response.Data.Data.Count.ShouldBe(activeCount);
            response.Data.Data.ShouldAllBe(c => c.CaseStatusId == (int)CaseStatusEnum.Active);
        }

        [Fact]
        public async Task Should_Get_All_Cases_Filtered_By_Type_Successfully()
        {
            // Arrange
            int financialCasesCount = 15, personalCasesCount = 8;
            await CreateCaseListForQueryAsync(financialCasesCount, caseType: CaseTypeEnum.Financial);
            await CreateCaseListForQueryAsync(personalCasesCount, caseType: CaseTypeEnum.Personal);
            var caseFilter = GetCaseFilter(caseType: CaseTypeEnum.Financial);
            var requestFilter = GetCasesPagedApiRequest(caseFilter);

            // Act
            var response = await GetAsync<PagedResult<CaseListItemDto>>($"api/case/Get", requestFilter);

            // Assert
            var expectedPageCount = (int)Math.Ceiling((financialCasesCount / (double)requestFilter.PageSize));
            var expectedPageDataCount = financialCasesCount > requestFilter.PageSize ? requestFilter.PageSize : financialCasesCount;
            response.Succeeded.ShouldBeTrue();
            response.Data.TotalPages.ShouldBe(expectedPageCount);
            response.Data.TotalCount.ShouldBe(financialCasesCount);
            response.Data.PageSize.ShouldBe(requestFilter.PageSize);
            response.Data.CurrentPage.ShouldBe(requestFilter.PageNumber + 1);
            response.Data.Data.Count.ShouldBe(expectedPageDataCount);
            response.Data.Data.ShouldAllBe(c => c.CaseTypeId == (int)CaseTypeEnum.Financial);
        }

        [Fact]
        public async Task Should_Get_All_Cases_Filtered_By_JudgeAcceptanceDateRange_Successfully()
        {
            // Arrange
            int accepctedCaseFromLastWeekCount = 15, accepctedCaseFromLastTwoDaysCount = 5, accepctedCaseBeforeLastWeekCount = 20;
            var judgeAcceptanceDateRange = new DateRangeDto
            {
                From = DateTime.Now.AddDays(-7),
                To = DateTime.Now
            };
            await CreateCaseListForQueryAsync(accepctedCaseFromLastWeekCount, acceptanceDate: judgeAcceptanceDateRange.From);
            await CreateCaseListForQueryAsync(accepctedCaseFromLastTwoDaysCount, acceptanceDate: CLock.Now.AddDays(-2));
            await CreateCaseListForQueryAsync(accepctedCaseBeforeLastWeekCount, acceptanceDate: judgeAcceptanceDateRange.From.AddDays(-1));
            var caseFilter = GetCaseFilter(judgeAcceptanceDateRange: judgeAcceptanceDateRange);
            var requestFilter = GetCasesPagedApiRequest(caseFilter);

            // Act
            var response = await GetAsync<PagedResult<CaseListItemDto>>($"api/case/Get", requestFilter);

            // Assert
            var expectedTotalCases = accepctedCaseFromLastWeekCount + accepctedCaseFromLastTwoDaysCount;
            var expectedPageCount = (int)Math.Ceiling((expectedTotalCases / (double)requestFilter.PageSize));
            var expectedPageDataCount = expectedTotalCases > requestFilter.PageSize ? requestFilter.PageSize : expectedTotalCases;
            response.Succeeded.ShouldBeTrue();
            response.Data.TotalPages.ShouldBe(expectedPageCount);
            response.Data.TotalCount.ShouldBe(expectedTotalCases);
            response.Data.PageSize.ShouldBe(requestFilter.PageSize);
            response.Data.CurrentPage.ShouldBe(requestFilter.PageNumber + 1);
            response.Data.Data.Count.ShouldBe(expectedPageDataCount);
            response.Data.Data.ShouldAllBe(c => c.JudgeAcceptanceDate >= judgeAcceptanceDateRange.From && c.JudgeAcceptanceDate <= judgeAcceptanceDateRange.To);
        }

        [Fact]
        public async Task Should_Get_All_Cases_Filtered_By_ReceiveDateRange_Successfully()
        {
            // Arrange
            int receiveCaseFromLastWeekCount = 15, receiveCaseFromLastTwoDaysCount = 5, receiveCaseBeforeLastWeekCount = 20;
            DateRangeDto receiveDateRange = new DateRangeDto
            {
                From = DateTime.Now.AddDays(-7),
                To = DateTime.Now
            };
            await CreateCaseListForQueryAsync(receiveCaseFromLastWeekCount, receiveDate: receiveDateRange.From);
            await CreateCaseListForQueryAsync(receiveCaseFromLastTwoDaysCount, receiveDate: CLock.Now.AddDays(-2));
            await CreateCaseListForQueryAsync(receiveCaseBeforeLastWeekCount, receiveDate: receiveDateRange.From.AddDays(-1));
            var caseFilter = GetCaseFilter(receiveDateRange: receiveDateRange);
            var requestFilter = GetCasesPagedApiRequest(caseFilter);

            // Act
            var response = await GetAsync<PagedResult<CaseListItemDto>>($"api/case/Get", requestFilter);

            // Assert

            var receiveTotalCases = receiveCaseFromLastWeekCount + receiveCaseFromLastTwoDaysCount;
            var expectedPageCount = (int)Math.Ceiling((receiveTotalCases / (double)requestFilter.PageSize));
            var expectedPageDataCount = receiveTotalCases > requestFilter.PageSize ? requestFilter.PageSize : receiveTotalCases;
            response.Succeeded.ShouldBeTrue();
            response.Data.TotalPages.ShouldBe(expectedPageCount);
            response.Data.TotalCount.ShouldBe(receiveTotalCases);
            response.Data.PageSize.ShouldBe(requestFilter.PageSize);
            response.Data.CurrentPage.ShouldBe(requestFilter.PageNumber + 1);
            response.Data.Data.Count.ShouldBe(expectedPageDataCount);
            response.Data.Data.ShouldAllBe(c => c.ReceiveDate >= receiveDateRange.From && c.ReceiveDate <= receiveDateRange.To);
        }

        [Fact]
        public async Task Should_Get_All_Cases_Filtered_By_Court_Successfully()
        {
            // Arrange
            int courtOneCasecount = 15, courtTwoCasecount = 5;
            var courtCodes = Utilities.SeedingCourts.Take(2);
            var courtOneCode = courtCodes.First().Code;
            var courtTwoCode = courtCodes.Last().Code;
            var divisionOfCourtTwoCode = Utilities.SeedingDivisions.Where(d => d.CourtCode == courtTwoCode).First().Code;
            await CreateCaseListForQueryAsync(courtOneCasecount, courtCode: courtOneCode);
            await CreateCaseListForQueryAsync(courtTwoCasecount, courtCode: courtTwoCode, divisionCode: divisionOfCourtTwoCode);

            var caseFilter = GetCaseFilter(courtCode: courtOneCode);
            var requestFilter = GetCasesPagedApiRequest(caseFilter);

            // Act
            var response = await GetAsync<PagedResult<CaseListItemDto>>($"api/case/Get", requestFilter);

            // Assert

            var expectedPageCount = (int)Math.Ceiling((courtOneCasecount / (double)requestFilter.PageSize));
            var expectedPageDataCount = courtOneCasecount > requestFilter.PageSize ? requestFilter.PageSize : courtOneCasecount;
            response.Succeeded.ShouldBeTrue();
            response.Data.TotalPages.ShouldBe(expectedPageCount);
            response.Data.TotalCount.ShouldBe(courtOneCasecount);
            response.Data.PageSize.ShouldBe(requestFilter.PageSize);
            response.Data.CurrentPage.ShouldBe(requestFilter.PageNumber + 1);
            response.Data.Data.Count.ShouldBe(expectedPageDataCount);
            response.Data.Data.ShouldAllBe(c => c.CourtCode == requestFilter.CaseFilter.CourtCode);
        }

        [Fact]
        public async Task Should_Get_All_Cases_Filtered_By_Division_Successfully()
        {
            // Arrange
            int divisionOneCasecount = 15, divisionTwoCaseCount = 5;
            var divisionCodes = Utilities.SeedingDivisions.Take(2);
            var divisionOne = divisionCodes.First();
            var divisionTwo = divisionCodes.Last();
            await CreateCaseListForQueryAsync(divisionOneCasecount, courtCode: divisionOne.CourtCode, divisionCode: divisionOne.Code);
            await CreateCaseListForQueryAsync(divisionTwoCaseCount, courtCode: divisionTwo.CourtCode, divisionCode: divisionTwo.Code);
            var caseFilter = GetCaseFilter(divisionCode: divisionOne.Code);
            var requestFilter = GetCasesPagedApiRequest(caseFilter);

            // Act
            var response = await GetAsync<PagedResult<CaseListItemDto>>($"api/case/Get", requestFilter);

            // Assert
            var expectedPageCount = (int)Math.Ceiling((divisionOneCasecount / (double)requestFilter.PageSize));
            var expectedPageDataCount = divisionOneCasecount > requestFilter.PageSize ? requestFilter.PageSize : divisionOneCasecount;
            response.Succeeded.ShouldBeTrue();
            response.Data.TotalPages.ShouldBe(expectedPageCount);
            response.Data.TotalCount.ShouldBe(divisionOneCasecount);
            response.Data.PageSize.ShouldBe(requestFilter.PageSize);
            response.Data.CurrentPage.ShouldBe(requestFilter.PageNumber + 1);
            response.Data.Data.Count.ShouldBe(expectedPageDataCount);
            response.Data.Data.ShouldAllBe(c => c.DivisionCode == requestFilter.CaseFilter.DivisionCode);
        }

        [Fact]
        public async Task Should_Get_All_Cases_Filtered_By_Judge_Successfully()
        {
            // Arrange
            int judgeOneCasecount = 15, judgeTwoCasecount = 5;
            var judgeCodes = Utilities.SeedingJudges.Take(2);
            var judgeOneCode = judgeCodes.First().Code;
            var judgeTwoCode = judgeCodes.Last().Code;
            await CreateCaseListForQueryAsync(judgeOneCasecount, judgeCode: judgeOneCode);
            await CreateCaseListForQueryAsync(judgeTwoCasecount, judgeCode: judgeTwoCode);
            var caseFilter = GetCaseFilter(judgeCode: judgeOneCode);
            var requestFilter = GetCasesPagedApiRequest(caseFilter);

            // Act
            var response = await GetAsync<PagedResult<CaseListItemDto>>($"api/case/Get", requestFilter);

            // Assert
            var expectedPageCount = (int)Math.Ceiling((judgeOneCasecount / (double)requestFilter.PageSize));
            var expectedPageDataCount = judgeOneCasecount > requestFilter.PageSize ? requestFilter.PageSize : judgeOneCasecount;
            response.Succeeded.ShouldBeTrue();
            response.Data.TotalPages.ShouldBe(expectedPageCount);
            response.Data.TotalCount.ShouldBe(judgeOneCasecount);
            response.Data.PageSize.ShouldBe(requestFilter.PageSize);
            response.Data.CurrentPage.ShouldBe(requestFilter.PageNumber + 1);
            response.Data.Data.Count.ShouldBe(expectedPageDataCount);
            response.Data.Data.ShouldAllBe(c => c.JudgeCode == requestFilter.CaseFilter.JudgeCode);
        }

        [Fact]
        public async Task Should_Get_All_Cases_Filtered_By_Court_And_Division_And_Judge_Successfully()
        {
            // Arrange
            var courtCodes = Utilities.SeedingCourts.Take(2);
            var courtOneCode = courtCodes.First().Code;
            var courtTwoCode = courtCodes.Last().Code;
            var divisionOfCourtOneCode = Utilities.SeedingDivisions.Where(d => d.CourtCode == courtOneCode).First().Code;
            var divisionOfCourtTwoCode = Utilities.SeedingDivisions.Where(d => d.CourtCode == courtTwoCode).First().Code;
            var judgeOne = Utilities.SeedingJudges.First();
            var judgeTwo = Utilities.SeedingJudges.Last();
            int courtOneDivisionOneJudgeOneCount = 20, courtTwoDivisionTwoJudgeOneCount = 20;
            await CreateCaseListForQueryAsync(courtOneDivisionOneJudgeOneCount, courtCode: courtOneCode, divisionCode: divisionOfCourtOneCode, judgeCode: judgeOne.Code);
            await CreateCaseListForQueryAsync(courtTwoDivisionTwoJudgeOneCount, courtCode: courtTwoCode, divisionCode: divisionOfCourtTwoCode);
            var caseFilter = GetCaseFilter(courtCode: courtOneCode, divisionCode: divisionOfCourtOneCode, judgeCode: judgeOne.Code);
            var requestFilter = GetCasesPagedApiRequest(caseFilter);

            // Act
            var response = await GetAsync<PagedResult<CaseListItemDto>>($"api/case/Get", requestFilter);

            // Assert
            var expectedPageCount = (int)Math.Ceiling((courtOneDivisionOneJudgeOneCount / (double)requestFilter.PageSize));
            var expectedPageDataCount = courtOneDivisionOneJudgeOneCount > requestFilter.PageSize ? requestFilter.PageSize : courtOneDivisionOneJudgeOneCount;
            response.Succeeded.ShouldBeTrue();
            response.Data.TotalPages.ShouldBe(expectedPageCount);
            response.Data.TotalCount.ShouldBe(courtOneDivisionOneJudgeOneCount);
            response.Data.PageSize.ShouldBe(requestFilter.PageSize);
            response.Data.CurrentPage.ShouldBe(requestFilter.PageNumber + 1);
            response.Data.Data.Count.ShouldBe(expectedPageDataCount);
            response.Data.Data.ShouldAllBe(c => c.CourtCode == requestFilter.CaseFilter.CourtCode);
            response.Data.Data.ShouldAllBe(c => c.DivisionCode == requestFilter.CaseFilter.DivisionCode);
            response.Data.Data.ShouldAllBe(c => c.JudgeCode == requestFilter.CaseFilter.JudgeCode);
        }


        [Fact]
        public async Task Should_Get_All_Cases_Filtered_By_TotalRequiredAmountWith_MIN_MAX_Successfully()
        {
            // Arrange
            var casesWith_50_RequiredAmountCount = 15;
            var casesWith_100_RequiredAmountCount = 11;
            var casesWith_200_RequiredAmountCount = 14;
            var casesWith_300_RequiredAmountCount = 15;
            var totalRequiredAmount50 = Money.Create("SAR", 50);
            var totalRequiredAmount100 = Money.Create("SAR", 100);
            var totalRequiredAmount200 = Money.Create("SAR", 200);
            var totalRequiredAmount300 = Money.Create("SAR", 300);
            await CreateCaseListForQueryAsync(casesWith_50_RequiredAmountCount, totalRequiredAmount: totalRequiredAmount50);
            await CreateCaseListForQueryAsync(casesWith_100_RequiredAmountCount, totalRequiredAmount: totalRequiredAmount100);
            await CreateCaseListForQueryAsync(casesWith_200_RequiredAmountCount, totalRequiredAmount: totalRequiredAmount200);
            await CreateCaseListForQueryAsync(casesWith_300_RequiredAmountCount, totalRequiredAmount: totalRequiredAmount300);
            var caseFilter = GetCaseFilter(totalRequiredAmount: new NumberRangeDto { Min = totalRequiredAmount100.Value, Max = totalRequiredAmount200.Value });
            var requestFilter = GetCasesPagedApiRequest(caseFilter);
            var matchedDataCount = casesWith_100_RequiredAmountCount + casesWith_200_RequiredAmountCount;

            // Act
            var response = await GetAsync<PagedResult<CaseListItemDto>>($"api/case/Get", requestFilter);

            // Assert
            var expectedPageCount = (int)Math.Ceiling(matchedDataCount / (double)requestFilter.PageSize);
            var expectedDataPerPageCount = matchedDataCount > requestFilter.PageSize ? requestFilter.PageSize : matchedDataCount;
            response.Succeeded.ShouldBeTrue();
            response.Data.TotalPages.ShouldBe(expectedPageCount);
            response.Data.TotalCount.ShouldBe(matchedDataCount);
            response.Data.PageSize.ShouldBe(requestFilter.PageSize);
            response.Data.CurrentPage.ShouldBe(requestFilter.PageNumber + 1);
            response.Data.Data.Count.ShouldBe(expectedDataPerPageCount);
            response.Data.Data.ShouldNotContain(c => c.TotalRequiredAmount.Value > totalRequiredAmount200.Value);
            response.Data.Data.ShouldNotContain(c => c.TotalRequiredAmount.Value < totalRequiredAmount100.Value);
        }

        [Fact]
        public async Task Should_Get_All_Cases_Filtered_By_TotalRequiredAmountWith_MAXonly_Successfully()
        {
            // Arrange
            var casesWith_50_RequiredAmountCount = 15;
            var casesWith_100_RequiredAmountCount = 11;
            var casesWith_200_RequiredAmountCount = 14;
            var casesWith_300_RequiredAmountCount = 15;
            var totalRequiredAmount50 = Money.Create("SAR", 50);
            var totalRequiredAmount100 = Money.Create("SAR", 100);
            var totalRequiredAmount200 = Money.Create("SAR", 200);
            var totalRequiredAmount300 = Money.Create("SAR", 300);
            await CreateCaseListForQueryAsync(casesWith_50_RequiredAmountCount, totalRequiredAmount: totalRequiredAmount50);
            await CreateCaseListForQueryAsync(casesWith_100_RequiredAmountCount, totalRequiredAmount: totalRequiredAmount100);
            await CreateCaseListForQueryAsync(casesWith_200_RequiredAmountCount, totalRequiredAmount: totalRequiredAmount200);
            await CreateCaseListForQueryAsync(casesWith_300_RequiredAmountCount, totalRequiredAmount: totalRequiredAmount300);
            var caseFilter = GetCaseFilter(totalRequiredAmount: new NumberRangeDto { Max = totalRequiredAmount200.Value });
            var requestFilter = GetCasesPagedApiRequest(caseFilter);
            var matchedDataCount = casesWith_50_RequiredAmountCount + casesWith_100_RequiredAmountCount + casesWith_200_RequiredAmountCount;

            // Act
            var response = await GetAsync<PagedResult<CaseListItemDto>>($"api/case/Get", requestFilter);

            // Assert
            var expectedPageCount = (int)Math.Ceiling((matchedDataCount) / (double)requestFilter.PageSize);
            var expectedDataPerPageCount = (matchedDataCount) > requestFilter.PageSize ? requestFilter.PageSize : (matchedDataCount);
            response.Succeeded.ShouldBeTrue();
            response.Data.TotalPages.ShouldBe(expectedPageCount);
            response.Data.TotalCount.ShouldBe(matchedDataCount);
            response.Data.PageSize.ShouldBe(requestFilter.PageSize);
            response.Data.CurrentPage.ShouldBe(requestFilter.PageNumber + 1);
            response.Data.Data.Count.ShouldBe(expectedDataPerPageCount);
            response.Data.Data.ShouldContain(c => c.TotalRequiredAmount.Value <= totalRequiredAmount200.Value);
            response.Data.Data.ShouldNotContain(c => c.TotalRequiredAmount.Value > totalRequiredAmount200.Value);
        }


        [Fact]
        public async Task Should_Get_All_Cases_Filtered_By_TotalRequiredAmountWith_MINonly_Successfully()
        {
            // Arrange
            var casesWith_50_RequiredAmountCount = 15;
            var casesWith_100_RequiredAmountCount = 11;
            var casesWith_200_RequiredAmountCount = 14;
            var casesWith_300_RequiredAmountCount = 15;
            var totalRequiredAmount50 = Money.Create("SAR", 50);
            var totalRequiredAmount100 = Money.Create("SAR", 100);
            var totalRequiredAmount200 = Money.Create("SAR", 200);
            var totalRequiredAmount300 = Money.Create("SAR", 300);
            await CreateCaseListForQueryAsync(casesWith_50_RequiredAmountCount, totalRequiredAmount: totalRequiredAmount50);
            await CreateCaseListForQueryAsync(casesWith_100_RequiredAmountCount, totalRequiredAmount: totalRequiredAmount100);
            await CreateCaseListForQueryAsync(casesWith_200_RequiredAmountCount, totalRequiredAmount: totalRequiredAmount200);
            await CreateCaseListForQueryAsync(casesWith_300_RequiredAmountCount, totalRequiredAmount: totalRequiredAmount300);
            var caseFilter = GetCaseFilter(totalRequiredAmount: new NumberRangeDto { Min = totalRequiredAmount100.Value });
            var requestFilter = GetCasesPagedApiRequest(caseFilter);
            var matchedDataCount = casesWith_100_RequiredAmountCount + casesWith_200_RequiredAmountCount + casesWith_300_RequiredAmountCount;

            // Act
            var response = await GetAsync<PagedResult<CaseListItemDto>>($"api/case/Get", requestFilter);

            // Assert
            var expectedPageCount = (int)Math.Ceiling(matchedDataCount / (double)requestFilter.PageSize);
            var expectedDataPerPageCount = matchedDataCount > requestFilter.PageSize ? requestFilter.PageSize : matchedDataCount;
            response.Succeeded.ShouldBeTrue();
            response.Data.TotalPages.ShouldBe(expectedPageCount);
            response.Data.TotalCount.ShouldBe(matchedDataCount);
            response.Data.PageSize.ShouldBe(requestFilter.PageSize);
            response.Data.CurrentPage.ShouldBe(requestFilter.PageNumber + 1);
            response.Data.Data.Count.ShouldBe(expectedDataPerPageCount);
            response.Data.Data.ShouldContain(c => c.TotalRequiredAmount.Value >= totalRequiredAmount100.Value);
            response.Data.Data.ShouldNotContain(c => c.TotalRequiredAmount.Value < totalRequiredAmount100.Value);
        }

        [Fact]
        public async Task Should_Update_Case_Court_Details_Successfully()
        {
            // Arrange
            var caseAgg = (await CreateCaseAsync()).Case;
            var updateCaseCourtDetailsDto = GetUpdateCaseCourtDetailsDto(caseAgg);

            // Act
            var response = await PostAsync($"api/case/Move", updateCaseCourtDetailsDto);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            UsingDbContext(dbContext =>
            {
                var updatedCase = dbContext.Cases.FirstOrDefault(x => x.CaseNumber == updateCaseCourtDetailsDto.CaseNumber);
                var currentCaseDetails = updatedCase.CaseDetails.SingleOrDefault(c => c.IsCurrent);
                currentCaseDetails.CourtCode.ShouldBe(updateCaseCourtDetailsDto.CourtCode);
                currentCaseDetails.DivisionCode.ShouldBe(updateCaseCourtDetailsDto.DivisionCode);
                currentCaseDetails.JudgeCode.ShouldBe(updateCaseCourtDetailsDto.JudgeCode);

                var expectedChangedCourtDetails = caseAgg.CaseDetails.FirstOrDefault(c => c.IsCurrent);
                var movedCreatedCaseDetails = updatedCase.CaseDetails.FirstOrDefault(c => c.Equals(expectedChangedCourtDetails));
                movedCreatedCaseDetails.IsCurrent.ShouldBeFalse();
                movedCreatedCaseDetails.ValueEquals(expectedChangedCourtDetails).ShouldBeTrue();

                var createdCaseHistory = dbContext.CaseHistory.FirstOrDefault(h => h.CaseNumber == updatedCase.CaseNumber);
                createdCaseHistory.ShouldNotBeNull();
                createdCaseHistory.Operation.ShouldBe(CaseOperationEnum.ChangeCourtDetails);
            });
        }

        [Fact]
        public async Task Should_Return_Validation_Errors_Upon_Empty_Input_Update_Request_Api()
        {
            // Arrange
            var emptyInput = "";
            var invalidUpdateCaseCourtDetailsDto = GetInvalidInputUpdateCaseCourtDetailsDto(emptyInput, emptyInput, emptyInput, emptyInput);

            // Act
            var response = await PostAsync($"api/case/Move", invalidUpdateCaseCourtDetailsDto);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            var responseContent = await DeerializeResponseContentAsync<Result>(response);
            responseContent.Succeeded.ShouldBeFalse();
            responseContent.Errors.Count.ShouldBe(8);
            responseContent.Errors.Any(err => err.Contains(nameof(invalidUpdateCaseCourtDetailsDto.CaseNumber))).ShouldBeTrue();
            responseContent.Errors.Any(err => err.Contains(nameof(invalidUpdateCaseCourtDetailsDto.CourtCode))).ShouldBeTrue();
            responseContent.Errors.Any(err => err.Contains(nameof(invalidUpdateCaseCourtDetailsDto.DivisionCode))).ShouldBeTrue();
            responseContent.Errors.Any(err => err.Contains(nameof(invalidUpdateCaseCourtDetailsDto.JudgeCode))).ShouldBeTrue();
        }

        [Fact]
        public async Task Should_Return_Validation_Errors_Upon_MaxLength_Input_Update_Request_Api()
        {
            // Arrange
            var exceededInput = "1455699992254786";
            var invalidUpdateCaseCourtDetailsDto = GetInvalidInputUpdateCaseCourtDetailsDto(exceededInput, exceededInput, exceededInput, exceededInput);

            // Act
            var response = await PostAsync($"api/case/Move", invalidUpdateCaseCourtDetailsDto);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            var responseContent = await DeerializeResponseContentAsync<Result>(response);
            responseContent.Succeeded.ShouldBeFalse();
            responseContent.Errors.Count.ShouldBe(4);
            responseContent.Errors.Any(err => err.Contains(nameof(invalidUpdateCaseCourtDetailsDto.CaseNumber))).ShouldBeTrue();
            responseContent.Errors.Any(err => err.Contains(nameof(invalidUpdateCaseCourtDetailsDto.CourtCode))).ShouldBeTrue();
            responseContent.Errors.Any(err => err.Contains(nameof(invalidUpdateCaseCourtDetailsDto.DivisionCode))).ShouldBeTrue();
            responseContent.Errors.Any(err => err.Contains(nameof(invalidUpdateCaseCourtDetailsDto.JudgeCode))).ShouldBeTrue();
        }

        [Fact]
        public async Task Should_Return_Validation_Errors_Upon_NonDigit_Input_Update_Request_Api()
        {
            // Arrange
            var nonDigitInput = "absds";
            var invalidUpdateCaseCourtDetailsDto = GetInvalidInputUpdateCaseCourtDetailsDto(nonDigitInput, nonDigitInput, nonDigitInput, nonDigitInput);

            // Act
            var response = await PostAsync($"api/case/Move", invalidUpdateCaseCourtDetailsDto);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            var responseContent = await DeerializeResponseContentAsync<Result>(response);
            responseContent.Succeeded.ShouldBeFalse();
            responseContent.Errors.Count.ShouldBe(4);
            responseContent.Errors.Any(err => err.Contains(nameof(invalidUpdateCaseCourtDetailsDto.CaseNumber))).ShouldBeTrue();
            responseContent.Errors.Any(err => err.Contains(nameof(invalidUpdateCaseCourtDetailsDto.CourtCode))).ShouldBeTrue();
            responseContent.Errors.Any(err => err.Contains(nameof(invalidUpdateCaseCourtDetailsDto.DivisionCode))).ShouldBeTrue();
            responseContent.Errors.Any(err => err.Contains(nameof(invalidUpdateCaseCourtDetailsDto.JudgeCode))).ShouldBeTrue();
        }

        private UpdateCaseCourtDetailsDto GetInvalidInputUpdateCaseCourtDetailsDto(string caseNumber, string courtCode, string divisionCode, string judgeCode)
        {
            return new UpdateCaseCourtDetailsDto
            {
                CaseNumber = caseNumber,
                CourtCode = courtCode,
                DivisionCode = divisionCode,
                JudgeCode = judgeCode
            };
        }
        private UpdateCaseCourtDetailsDto GetUpdateCaseCourtDetailsDto(Case caseAgg)
        {
            var caseCurrentDivision = caseAgg.CaseDetails.Single(cd => cd.IsCurrent);
            var division = Utilities.SeedingDivisions.FirstOrDefault(d => caseCurrentDivision.DivisionCode != d.Code);
            var courtCode = division.CourtCode;
            var updateCaseCourtDetailsDto = new UpdateCaseCourtDetailsDto
            {
                CaseNumber = caseAgg.CaseNumber,
                CourtCode = courtCode,
                DivisionCode = division.Code,
                JudgeCode = Utilities.SeedingJudges.Last().Code
            };
            return updateCaseCourtDetailsDto;
        }

        #region Methods
        private CasesPagedApiRequest GetCasesPagedApiRequest(CaseFilter caseFilter = null)
        {
            var requestFilter = new CasesPagedApiRequest()
            {
                PageNumber = 0,
                PageSize = 10,
                Sort = new List<Sort>
                {
                    new Sort
                    {
                        Direction= 0,
                        Field= "CaseNumber",
                        Order= 1
                    }
                },
                CaseFilter = caseFilter ?? GetCaseFilter()
            };
            return requestFilter;
        }

        private CaseFilter GetCaseFilter(string[] caseNumbers = null, CaseStatusEnum? caseStatus = null, CaseTypeEnum? caseType = null,
            string courtCode = null,
            string divisionCode = null,
            string judgeCode = null,
            DateRangeDto judgeAcceptanceDateRange = null,
            DateRangeDto receiveDateRange = null,
            NumberRangeDto totalRequiredAmount = null)
        {
            return new CaseFilter
            {
                JudgeAcceptanceDateRange = judgeAcceptanceDateRange,
                ReceiveDateRange = receiveDateRange,
                CaseNumbers = caseNumbers ?? new string[] { },
                CaseStatusId = (int?)caseStatus ?? null,
                CaseTypeId = (int?)caseType ?? null,
                CourtCode = courtCode,
                DivisionCode = divisionCode,
                JudgeCode = judgeCode,
                TotalRequiredAmountRange = totalRequiredAmount
            };
        }

        private static List<AddCasePartiesDto> GetAssignPartyInputDto(Case caseAgg, Party createdParty)
        {
            return new List<AddCasePartiesDto>
            {
                new AddCasePartiesDto
                {
                    CaseNumber = caseAgg.CaseNumber,
                    Requesters = new List<CasePartyDto>
                    {
                        new CasePartyDto
                        {
                            IsApplicant = true,
                            PartyRole = PartyRoleEnum.OriginalDebtor,
                            Details = new PartyDto
                            {
                                FullName = "Complaint Party1",
                                PartyNumber = createdParty.PartyNumber,
                                Gender = Gender.Male,
                                DateOfBirth = CLock.Now.AddYears(-30),
                                NationalityCode = "Sar",
                                PartyIdentityNumber = "Identity1",
                                PartyIdentityTypeId = PartyIdentityTypeEnum.SaudiNationalId,
                                PartyLocationId = PartyLocationEnum.InsideSaudi,
                                PartyTypeId = PartyTypeEnum.Individual
                            }
                        }
                    }
                }
            };
        }

        private async Task<IEnumerable<Case>> CreateCaseListForQueryAsync(int count = 10,
            CaseTypeEnum caseType = CaseTypeEnum.Financial,
            CaseStatusEnum caseStatus = CaseStatusEnum.Active,
            string courtCode = null,
            string divisionCode = null,
            string judgeCode = null,
            DateTime? receiveDate = null,
            DateTime? acceptanceDate = null,
            Money totalRequiredAmount = null,
            Money totalRemainingAmount = null)

        {
            List<Case> createdCasesList = new List<Case>();

            for (int caseIndex = 1; caseIndex <= count; caseIndex++)
            {
                var createdCase = await CreateCaseAsync(caseIndex.ToString(), caseType, caseStatus,
                    receiveDate: receiveDate,
                    acceptanceDate: acceptanceDate,
                    judgeCode: judgeCode,
                    divisionCode: divisionCode,
                    courtCode: courtCode,
                    totalRequiredAmount: totalRequiredAmount,
                    totalRemainingAmount: totalRemainingAmount);

                createdCasesList.Add(createdCase.Case);
            }
            return createdCasesList;
        }

        private async Task<(Case Case, int PromissoryId)> CreateCaseAsync(string number = "1",
            CaseTypeEnum caseType = CaseTypeEnum.Financial,
            CaseStatusEnum caseStatus = CaseStatusEnum.Active,
            DateTime? receiveDate = null,
            DateTime? acceptanceDate = null,
            string judgeCode = null,
            string divisionCode = null,
            string courtCode = null,
            Money totalRemainingAmount = null,
            Money totalRequiredAmount = null)
        {
            var promissory = await ObjectFactory.CreatePromissoryAsync(number);
            var caseAgg = await ObjectFactory.CreateCaseAsync(caseNumber: number, promissoryNumber: promissory.Number,
                caseType: caseType,
                caseStatus: caseStatus,
                receiveDate: receiveDate,
                acceptanceDate: acceptanceDate,
                judgeCode: judgeCode,
                courtCode: courtCode,
                divisionCode: divisionCode,
                totalRemainingAmount: totalRemainingAmount,
                totalRequiredAmount: totalRequiredAmount);

            UsingDbContext(dbContext =>
            {
                dbContext.Promissories.Add(promissory);
                dbContext.SaveChanges();

                dbContext.Cases.Add(caseAgg);
                dbContext.SaveChanges();
            });

            return (caseAgg, promissory.Id);
        }

        private async Task<Party> CreatePartyAsync()
        {
            var partyAgg = await ObjectFactory.CreatePartyAsync();

            UsingDbContext(dbContext =>
            {
                dbContext.Parties.Add(partyAgg);
                dbContext.SaveChanges();
            });

            return partyAgg;
        }

        private static List<CaseFullDetailsDto> GetCaseFullDetailsDtoList()
        {
            var courtDivision = Utilities.SeedingDivisions.First();
            var judge = Utilities.SeedingJudges.First();
            return new List<CaseFullDetailsDto>() { new CaseFullDetailsDto
            {
                BasicDetails = new CaseDetailsDto
                {
                    CaseNumber = "123",
                    CaseStatus = CaseStatusEnum.Active,
                    CaseType = CaseTypeEnum.Financial,
                    CourtCode = courtDivision.CourtCode,
                    DivisionCode = courtDivision.Code,
                    JudgeCode = judge.Code,
                    JudgeAcceptanceDate = CLock.Now,
                    ReceiveDate = CLock.Now,
                    RequiredAmount = new MoneyDto{CurrencyIso = "SAR", Value = 1000},
                    CaseBasicAmount = new MoneyDto{CurrencyIso = "SAR", Value = 2000}
                },
                PromissoryList = new List<PromissoryDto>
                {
                    new PromissoryDto
                    {
                        PromissoryNumber = "1",
                        PromissoryTypeId = PromissoryTypeEnum.Contract,
                        PromissoryIssueDate = CLock.Now.AddDays(10)
                    }
                },
                Requesters = new List<CasePartyDto>
                {
                    new CasePartyDto
                    {
                        PartyRole = PartyRoleEnum.Applicant,
                        IsApplicant = true,
                        Details = new PartyDto
                        {
                            FullName = "Complaint Party1",
                            PartyNumber = "1",
                            Gender = Gender.Male,
                            DateOfBirth = CLock.Now.AddYears(-30),
                            NationalityCode = "Sar",
                            PartyIdentityNumber = "Identity1",
                            PartyIdentityTypeId = PartyIdentityTypeEnum.SaudiNationalId,
                            PartyLocationId = PartyLocationEnum.InsideSaudi,
                            PartyTypeId = PartyTypeEnum.Individual
                        }
                    }
                },
                Respondents = new List<CasePartyDto>
                {
                    new CasePartyDto
                    {
                        PartyRole = PartyRoleEnum.OriginalDebtor,
                        IsApplicant = false,
                        Details = new PartyDto
                        {
                            FullName = "Accused Party1",
                            PartyNumber = "2",
                            Gender = Gender.Male,
                            DateOfBirth = CLock.Now.AddYears(-30),
                            NationalityCode = "Sar",
                            PartyIdentityNumber = "Identity2",
                            PartyIdentityTypeId = PartyIdentityTypeEnum.SaudiNationalId,
                            PartyLocationId = PartyLocationEnum.InsideSaudi,
                            PartyTypeId = PartyTypeEnum.Individual
                        }
                    }
                },
                Claims = new List<ClaimDto>
                {
                    new ClaimDto
                    {
                        ClaimDateTime = CLock.Now,
                        DebtTypeId = DebtTypeEnum.Alimony,
                        ComplaintPartyNumber = "1",
                        RequiredAmount = new MoneyDto{Value= 2500, CurrencyIso="SAR"},
                        RemainingAmount = new MoneyDto{Value= 2500, CurrencyIso="SAR"},
                        BasicAmount = new MoneyDto{Value= 2500, CurrencyIso="SAR"},
                        ClaimDetails = new List<ClaimDetailsDto>
                        {
                            new ClaimDetailsDto
                            {
                                RequiredAmount = new MoneyDto{Value= 2500, CurrencyIso="SAR"},
                                BillingAmount = new MoneyDto{Value= 2500, CurrencyIso="SAR"},
                                AccusedPartyNumber = "2"
                            }
                        }
                    }
                }
            }};
        }
        #endregion
    }
}