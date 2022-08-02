using Moj.CMS.Application.AppServices.VIban.Queries.GetAllVIbans;
using Moj.CMS.Domain.Aggregates.VIban;
using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Domain.Shared.Values;
using Moj.CMS.IntegartionTests.Helpers;
using Moj.CMS.Shared.DTO;
using Moj.CMS.Shared.Requests;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Moj.CMS.IntegartionTests
{
    public class VIban_Integration_Tests : CmsIntegrationTestBase<VIban_Integration_Tests>
    {

        public VIban_Integration_Tests(WebApplicationTestHostFactoryFixture<VIban_Integration_Tests> host) : base(host)
        {
        }

        [Fact]
        public async Task Should_Get_All_VIban_Paged_Successfully_Query()
        {
            // Arrange
            int count = 15;
            var createdVIbans = await CreateVIbansForQueryAsync(count);
            var request = GetVIbanListRequest();

            // Act
            var response = await ExecuteQueryAsync(new GetAllVIbansQuery { PagedRequest = request });

            // Assert
            var expectedPageCount = (int)Math.Ceiling(count / (double)request.PageSize);
            response.Succeeded.ShouldBeTrue();
            response.TotalPages.ShouldBe(expectedPageCount);
            response.TotalCount.ShouldBe(count);
            response.PageSize.ShouldBe(request.PageSize);
            response.CurrentPage.ShouldBe(request.PageNumber + 1);
            response.Data.Count.ShouldBe(request.PageSize);

            var firstVIban = createdVIbans.First();
            var returenedVIban = response.Data.FirstOrDefault(d => d.AccountNumber == firstVIban.Number);
            returenedVIban.Alias.ShouldBe(firstVIban.Alias);
            returenedVIban.IsActive.ShouldBe(firstVIban.IsActive);
            returenedVIban.IssueDate.ShouldBe(firstVIban.IssueDate);
            returenedVIban.ReferenceNumber.ShouldBe(firstVIban.ReferenceDetails.ReferenceNumber);
            returenedVIban.ReferenceType.ShouldBe(firstVIban.ReferenceDetails.ReferenceType);
        }

        [Fact]
        public async Task Should_Get_All_VIbans_Filtered_By_AccountNumber_Successfully_Query()
        {
            // Arrange
            int count = 15;
            var createdVIbans = await CreateVIbansForQueryAsync(count);
            var createdAccountNumberFilter = createdVIbans.First().Number;
            var vIbanFilter = GetVIbanFilter(accountNumber: createdAccountNumberFilter);
            var request = GetVIbanListRequest(vIbanFilter);

            // Act
            var response = await ExecuteQueryAsync(new GetAllVIbansQuery { PagedRequest = request });

            // Assert
            response.Succeeded.ShouldBeTrue();
            response.TotalPages.ShouldBe(1);
            response.TotalCount.ShouldBe(1);
            response.PageSize.ShouldBe(request.PageSize);
            response.CurrentPage.ShouldBe(request.PageNumber + 1);
            response.Data.Count.ShouldBe(1);
            var filteredVIban = response.Data.FirstOrDefault();
            filteredVIban.AccountNumber.ShouldBe(vIbanFilter.AccountNumber);
        }

        [Fact]
        public async Task Should_Get_All_VIbans_Filtered_By_Alias_Successfully_Query()
        {
            // Arrange
            int count = 15;
            var createdVIbans = await CreateVIbansForQueryAsync(count);
            var createdAliasFilter = createdVIbans.First().Alias;
            var vIbanFilter = GetVIbanFilter(alias: createdAliasFilter);
            var request = GetVIbanListRequest(vIbanFilter);

            // Act
            var response = await ExecuteQueryAsync(new GetAllVIbansQuery { PagedRequest = request });

            // Assert
            response.Succeeded.ShouldBeTrue();
            response.TotalPages.ShouldBe(1);
            response.TotalCount.ShouldBe(1);
            response.PageSize.ShouldBe(request.PageSize);
            response.CurrentPage.ShouldBe(request.PageNumber + 1);
            response.Data.Count.ShouldBe(1);
            var filteredVIban = response.Data.FirstOrDefault();
            filteredVIban.Alias.ShouldBe(vIbanFilter.Alias);
        }

        [Fact]
        public async Task Should_Get_All_VIbans_Filtered_By_ReferenceDetails_Successfully_Query()
        {
            // Arrange
            int count = 5;
            int vIbanCountWithPartyReferenceType = 1;
            var filterReferenceNumber = "party reference number";
            var filterReferenceType = VIbanReferenceTypeEnum.Party;
            await CreateVIbansForQueryAsync(count, referenceNumber: "test reference number", referenceType: VIbanReferenceTypeEnum.Case);
            await CreateVIbansForQueryAsync(vIbanCountWithPartyReferenceType, referenceNumber: filterReferenceNumber, referenceType: filterReferenceType);
            var vIbanFilter = GetVIbanFilter(referenceNumber: filterReferenceNumber, referenceType: (int)filterReferenceType);
            var request = GetVIbanListRequest(vIbanFilter);

            // Act
            var response = await ExecuteQueryAsync(new GetAllVIbansQuery { PagedRequest = request });

            // Assert
            var expectedPageCount = (int)Math.Ceiling(vIbanCountWithPartyReferenceType / (double)request.PageSize);
            response.Succeeded.ShouldBeTrue();
            response.TotalPages.ShouldBe(expectedPageCount);
            response.TotalCount.ShouldBe(vIbanCountWithPartyReferenceType);
            response.PageSize.ShouldBe(request.PageSize);
            response.CurrentPage.ShouldBe(request.PageNumber + 1);
            response.Data.Count.ShouldBe(vIbanCountWithPartyReferenceType);
            var filteredVIban = response.Data.FirstOrDefault();
            filteredVIban.ReferenceNumber.ShouldBe(vIbanFilter.ReferenceNumber);
            vIbanFilter.ReferenceTypeId.ShouldBe((int)filteredVIban.ReferenceType);
        }

        [Fact]
        public async Task Should_Get_All_VIbans_Filtered_By_IssueDateRange_Successfully_Query()
        {
            // Arrange
            int vIbansBeforeLastWeekCount = 20, vIbansFromLastWeekCount = 15, vIbansFromLastTwoDaysCount = 5, vIbansAfterTwoDaysCount = 6;
            var issueDateRange = new DateRangeDto
            {
                From = DateTime.Now.AddDays(-7),
                To = DateTime.Now
            };
            await CreateVIbansForQueryAsync(vIbansBeforeLastWeekCount, issueDate: issueDateRange.From.AddDays(-1));
            await CreateVIbansForQueryAsync(vIbansFromLastWeekCount, issueDate: issueDateRange.From);
            await CreateVIbansForQueryAsync(vIbansFromLastTwoDaysCount, issueDate: issueDateRange.To.Value.AddDays(-1));
            await CreateVIbansForQueryAsync(vIbansAfterTwoDaysCount, issueDate: issueDateRange.To.Value.AddDays(2));
            var vIbanFilter = GetVIbanFilter(issueDateRange: issueDateRange);
            var request = GetVIbanListRequest(vIbanFilter);

            // Act
            var response = await ExecuteQueryAsync(new GetAllVIbansQuery { PagedRequest = request });

            // Assert
            var expectedTotalVIbans = vIbansFromLastWeekCount + vIbansFromLastTwoDaysCount;
            var expectedPageCount = (int)Math.Ceiling((expectedTotalVIbans / (double)request.PageSize));
            var expectedPageDataCount = expectedTotalVIbans > request.PageSize ? request.PageSize : expectedTotalVIbans;
            response.Succeeded.ShouldBeTrue();
            response.TotalPages.ShouldBe(expectedPageCount);
            response.TotalCount.ShouldBe(expectedTotalVIbans);
            response.PageSize.ShouldBe(request.PageSize);
            response.CurrentPage.ShouldBe(request.PageNumber + 1);
            response.Data.Count.ShouldBe(expectedPageDataCount);
            response.Data.ShouldAllBe(c => c.IssueDate >= issueDateRange.From && c.IssueDate <= issueDateRange.To);
        }

        [Fact]
        public async Task Should_Get_All_VIbans_Filtered_By_IssueDateFrom_Successfully_Query()
        {
            // Arrange
            int vIbansBeforeLastWeekCount = 20, vIbansFromLastWeekCount = 15, vIbansAfterTwoDaysCount = 5;
            var issueDateRange = new DateRangeDto
            {
                From = DateTime.Now.AddDays(-7)
            };
            await CreateVIbansForQueryAsync(vIbansBeforeLastWeekCount, issueDate: issueDateRange.From.AddDays(-1));
            await CreateVIbansForQueryAsync(vIbansFromLastWeekCount, issueDate: issueDateRange.From);
            await CreateVIbansForQueryAsync(vIbansAfterTwoDaysCount, issueDate: CLock.Now.AddDays(2));
            var vIbanFilter = GetVIbanFilter(issueDateRange: issueDateRange);
            var request = GetVIbanListRequest(vIbanFilter);

            // Act
            var response = await ExecuteQueryAsync(new GetAllVIbansQuery { PagedRequest = request });

            // Assert
            var expectedTotalVIbans = vIbansFromLastWeekCount + vIbansAfterTwoDaysCount;
            var expectedPageCount = (int)Math.Ceiling((expectedTotalVIbans / (double)request.PageSize));
            var expectedPageDataCount = expectedTotalVIbans > request.PageSize ? request.PageSize : expectedTotalVIbans;
            response.Succeeded.ShouldBeTrue();
            response.TotalPages.ShouldBe(expectedPageCount);
            response.TotalCount.ShouldBe(expectedTotalVIbans);
            response.PageSize.ShouldBe(request.PageSize);
            response.CurrentPage.ShouldBe(request.PageNumber + 1);
            response.Data.Count.ShouldBe(expectedPageDataCount);
            response.Data.ShouldAllBe(c => c.IssueDate >= issueDateRange.From);
        }

        [Fact]
        public async Task Should_Get_All_VIbans_Filtered_By_IssueDateTo_Successfully_Query()
        {
            // Arrange
            int vIbansBeforeLastWeekCount = 20, vIbansFromLastWeekCount = 15;
            var issueDateRange = new DateRangeDto
            {
                To = DateTime.Now.AddDays(-7)
            };
            await CreateVIbansForQueryAsync(vIbansBeforeLastWeekCount, issueDate: issueDateRange.To.Value.AddDays(-1));
            await CreateVIbansForQueryAsync(vIbansFromLastWeekCount, issueDate: issueDateRange.To.Value.AddDays(1));
            var vIbanFilter = GetVIbanFilter(issueDateRange: issueDateRange);
            var request = GetVIbanListRequest(vIbanFilter);

            // Act
            var response = await ExecuteQueryAsync(new GetAllVIbansQuery { PagedRequest = request });

            // Assert
            var expectedTotalVIbans = vIbansBeforeLastWeekCount;
            var expectedPageCount = (int)Math.Ceiling((expectedTotalVIbans / (double)request.PageSize));
            var expectedPageDataCount = expectedTotalVIbans > request.PageSize ? request.PageSize : expectedTotalVIbans;
            response.Succeeded.ShouldBeTrue();
            response.TotalPages.ShouldBe(expectedPageCount);
            response.TotalCount.ShouldBe(expectedTotalVIbans);
            response.PageSize.ShouldBe(request.PageSize);
            response.CurrentPage.ShouldBe(request.PageNumber + 1);
            response.Data.Count.ShouldBe(expectedPageDataCount);
            response.Data.ShouldAllBe(c => c.IssueDate <= issueDateRange.To);
        }


        #region Private Methods

        private async Task<List<VIban>> CreateVIbansForQueryAsync(int count, string alias = null, string vIbanNumber = null,
            string referenceNumber = null,
            VIbanReferenceTypeEnum? referenceType = null,
            decimal CAP = 10000,
            DateTime? issueDate = null)
        {
            var createdCasesList = new List<VIban>();

            for (int vIbanIndex = 1; vIbanIndex <= count; vIbanIndex++)
            {
                var createdVIban = await CreateVIbanAsync(alias: string.IsNullOrEmpty(alias) ? $"alias_{vIbanIndex}" : alias,
                    vIbanNumber: string.IsNullOrEmpty(vIbanNumber) ? $"vIbanNumber_{vIbanIndex}" : vIbanNumber,
                    referenceNumber: string.IsNullOrEmpty(referenceNumber) ? $"ReferenceNumber_{vIbanIndex}" : referenceNumber,
                    referenceType: referenceType ?? VIbanReferenceTypeEnum.Case,
                    CAP: CAP,
                    issueDate: issueDate ?? CLock.Now);

                createdCasesList.Add(createdVIban);
            }
            return createdCasesList;
        }

        private async Task<VIban> CreateVIbanAsync(string alias = "Test Name", string vIbanNumber = "Test Number",
            string referenceNumber = "Reference Number",
            VIbanReferenceTypeEnum referenceType = VIbanReferenceTypeEnum.Case,
            string parentAccount = "Test Parent Account",
            string bankName = "Test Bank Name",
            decimal CAP = 100000,
            DateTime? issueDate = null)
        {
            var vIban = await ObjectFactory.CreateVIbanAsync(alias, vIbanNumber, bankName,
                parentAccount,
                referenceNumber,
                referenceType,
                CAP,
                issueDate);

            UsingDbContext(dbContext =>
            {
                dbContext.VIbans.Add(vIban);
                dbContext.SaveChanges();
            });

            return vIban;
        }

        private PagedRequest<VIbanDto> GetVIbanListRequest(VIbanFilter vIbanFilter = null)
        {
            var request = new PagedRequest<VIbanDto>()
            {
                PageNumber = 0,
                PageSize = 10,
                Sort = new List<Sort>(),
                Filter = vIbanFilter ?? GetVIbanFilter()
            };
            return request;
        }

        private VIbanFilter GetVIbanFilter(string accountNumber = null, string alias = null, string referenceNumber = null,
            int? referenceType = null,
            DateRangeDto issueDateRange = null)
        {
            return new VIbanFilter
            {
                AccountNumber = accountNumber,
                Alias = alias,
                IssueDateRange = issueDateRange,
                ReferenceNumber = referenceNumber,
                ReferenceTypeId = referenceType
            };
        }

        #endregion
    }
}
