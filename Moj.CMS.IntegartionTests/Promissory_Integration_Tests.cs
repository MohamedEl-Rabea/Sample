using Moj.CMS.Application.AppServices.Promissory.Commands.AddCasePromissory;
using Moj.CMS.Application.AppServices.Promissory.Dtos;
using Moj.CMS.Application.AppServices.Promissory.Queries;
using Moj.CMS.Application.AppServices.Promissory.Queries.GetAllPromissories;
using Moj.CMS.Domain.Aggregates.Promissory;
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

namespace Moj.CMS.IntegartionTests
{
    public class Promissory_Integration_Tests : CmsIntegrationTestBase<Promissory_Integration_Tests>
    {

        public Promissory_Integration_Tests(WebApplicationTestHostFactoryFixture<Promissory_Integration_Tests> host) : base(host)
        {
        }

        [Fact]
        public async Task Should_Create_Promissory_Succefully_Api()
        {
            // Arrange
            var createdCase = await CreateCase();
            var promissoryNumber = createdCase.CasePromissories.FirstOrDefault()?.PromissoryNumber ?? "1";
            var promissoryInputList = CreatePromissoryInputListDto(createdCase.CaseNumber, promissoryNumber + "000");

            // Act
            var response = await PostAsync($"api/case/promissory/create", promissoryInputList);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.Created);
            UsingDbContext(dbContext =>
            {
                var casePromissoryInput = promissoryInputList.First();
                var promissoryInput = casePromissoryInput.PromissoryDtoList.First();
                var createdPromissory = dbContext.Promissories.FirstOrDefault();
                createdPromissory.ShouldNotBeNull();
                createdPromissory.Number.ShouldBe(promissoryInput.PromissoryNumber);
                createdPromissory.TypeId.ShouldBe(promissoryInput.PromissoryTypeId);
                createdPromissory.PromissoryIssueDate.ShouldBe(promissoryInput.PromissoryIssueDate);

                var createdCaseHistory = dbContext.CaseHistory.FirstOrDefault();
                createdCaseHistory.ShouldNotBeNull();
                createdCaseHistory.CaseNumber.ShouldBe(casePromissoryInput.CaseNumber);
                createdCaseHistory.Operation.ShouldBe(CaseOperationEnum.AddPromissory);
            });
        }

        [Fact]
        public async Task Should_Get_All_Promissories_Paged_Successfully()
        {
            // Arrange
            int count = 10;
            await CreatePromissoryListForQueryAsync(count);
            var requestFilter = GetPromissoryPagedApiRequest();

            // Act
            var response = await GetAsync<PagedResult<GetAllPromissoriesDto>>($"api/case/promissory/Get", requestFilter);

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
        public async Task Should_Get_All_Promissories_By_Filtered_By_Number_Successfully()
        {
            // Arrange
            int count = 10;
            var createdPromissories = await CreatePromissoryListForQueryAsync(count);
            var promissoryNumberFilter = createdPromissories.First().Number;
            var promissoryFilter = GetPromissoryFilter(number: promissoryNumberFilter);
            var requestFilter = GetPromissoryPagedApiRequest(promissoryFilter);

            // Act
            var response = await GetAsync<PagedResult<GetAllPromissoriesDto>>($"api/case/promissory/Get", requestFilter);

            // Assert
            response.Succeeded.ShouldBeTrue();
            response.Data.TotalPages.ShouldBe(1);
            response.Data.TotalCount.ShouldBe(1);
            response.Data.PageSize.ShouldBe(requestFilter.PageSize);
            response.Data.CurrentPage.ShouldBe(requestFilter.PageNumber + 1);
            response.Data.Data.Count.ShouldBe(1);
            var filteredPromissory = response.Data.Data.FirstOrDefault();
            filteredPromissory.PromissoryNumber.ShouldBe(requestFilter.PromissoryFilter.Number);
        }

        [Fact]
        public async Task Should_Get_All_Promissories_By_Filtered_By_Type_Successfully()
        {
            // Arrange
            int courtOrderCount = 5;
            int otherCount = 7;
            await CreatePromissoryListForQueryAsync(otherCount);
            await CreatePromissoryListForQueryAsync(courtOrderCount, PromissoryTypeEnum.CourtOrder);
            var promissoryFilter = GetPromissoryFilter(typeId: (int)PromissoryTypeEnum.CourtOrder);
            var requestFilter = GetPromissoryPagedApiRequest(promissoryFilter);

            // Act
            var response = await GetAsync<PagedResult<GetAllPromissoriesDto>>($"api/case/promissory/Get", requestFilter);

            // Assert
            var expectedPageCount = (int)Math.Ceiling(courtOrderCount / (double)requestFilter.PageSize);
            response.Succeeded.ShouldBeTrue();
            response.Data.TotalPages.ShouldBe(expectedPageCount);
            response.Data.TotalCount.ShouldBe(courtOrderCount);
            response.Data.PageSize.ShouldBe(requestFilter.PageSize);
            response.Data.CurrentPage.ShouldBe(requestFilter.PageNumber + 1);
            response.Data.Data.Count.ShouldBe(courtOrderCount);
            response.Data.Data.ShouldAllBe(c => c.TypeId == PromissoryTypeEnum.CourtOrder);
        }

        [Fact]
        public async Task Should_Get_All_Promissories_Filtered_By_IssueDateRange_Successfully()
        {
            // Arrange
            int promissoriesFromLastWeekCount = 15,
                promissoriesFromLastTwoDaysCount = 5,
                promissoriesBeforeLastWeekCount = 20;

            var issueDateRange = new DateRangeDto
            {
                From = DateTime.Now.AddDays(-7),
                To = DateTime.Now
            };
            await CreatePromissoryListForQueryAsync(promissoriesFromLastWeekCount, issueDate: issueDateRange.From);
            await CreatePromissoryListForQueryAsync(promissoriesFromLastTwoDaysCount, issueDate: CLock.Now.AddDays(-2));
            await CreatePromissoryListForQueryAsync(promissoriesBeforeLastWeekCount, issueDate: issueDateRange.From.AddDays(-1));
            var promissoryFilter = GetPromissoryFilter(promissoryDateRange: issueDateRange);
            var requestFilter = GetPromissoryPagedApiRequest(promissoryFilter);

            // Act
            var response = await GetAsync<PagedResult<GetAllPromissoriesDto>>($"api/case/promissory/Get", requestFilter);

            // Assert
            var expectedTotalPromissories = promissoriesFromLastWeekCount + promissoriesFromLastTwoDaysCount;
            var expectedPageCount = (int)Math.Ceiling((expectedTotalPromissories / (double)requestFilter.PageSize));
            var expectedPageDataCount = expectedTotalPromissories > requestFilter.PageSize ? requestFilter.PageSize : expectedTotalPromissories;
            response.Succeeded.ShouldBeTrue();
            response.Data.TotalPages.ShouldBe(expectedPageCount);
            response.Data.TotalCount.ShouldBe(expectedTotalPromissories);
            response.Data.PageSize.ShouldBe(requestFilter.PageSize);
            response.Data.CurrentPage.ShouldBe(requestFilter.PageNumber + 1);
            response.Data.Data.Count.ShouldBe(expectedPageDataCount);
            response.Data.Data.ShouldAllBe(c => c.PromissoryDate >= issueDateRange.From && c.PromissoryDate <= issueDateRange.To);
        }

        #region Private Methods

        private List<AddCasePromissoryDto> CreatePromissoryInputListDto(string caseNumber, string promissoryNumber)
        {
            return new List<AddCasePromissoryDto>
            {
                new AddCasePromissoryDto()
                {
                    CaseNumber = caseNumber,
                    PromissoryDtoList = new List<PromissoryDto>
                    {
                        new PromissoryDto
                        {
                            PromissoryNumber = promissoryNumber,
                            PromissoryTypeId = PromissoryTypeEnum.Contract,
                            PromissoryIssueDate = CLock.Now.AddDays(10)
                        }
                    }
                }
            };
        }

        private async Task<Domain.Aggregates.Case.Case> CreateCase(string promissoryNumber = "1")
        {
            var caseAggregate = await ObjectFactory.CreateCaseAsync(promissoryNumber: promissoryNumber);
            UsingDbContext(dbContext =>
            {
                dbContext.Cases.Add(caseAggregate);
                dbContext.SaveChanges();
            });
            return caseAggregate;
        }

        private async Task<IEnumerable<Promissory>> CreatePromissoryListForQueryAsync(int count = 10,
            PromissoryTypeEnum typeId = PromissoryTypeEnum.Contract,
            DateTime? issueDate = null)
        {
            List<Promissory> createdPromissoryList = new List<Promissory>();

            for (int promissoryIndex = 1; promissoryIndex <= count; promissoryIndex++)
            {
                var createdPromissory = await CreatePromissoryAsync(promissoryIndex.ToString(), typeId, issueDate);
                await CreateCase(createdPromissory.Number);

                createdPromissoryList.Add(createdPromissory);
            }
            return createdPromissoryList;
        }

        private async Task<Promissory> CreatePromissoryAsync(string number = "1",
            PromissoryTypeEnum typeId = PromissoryTypeEnum.Contract,
            DateTime? issueDate = null)
        {
            var promissory = await ObjectFactory.CreatePromissoryAsync(number: number, typeId: typeId, issueDate: issueDate);

            UsingDbContext(dbContext =>
            {
                dbContext.Promissories.Add(promissory);
                dbContext.SaveChanges();
            });

            return promissory;
        }

        private PromissoriesPagedApiRequest GetPromissoryPagedApiRequest(PromissoryFilter promissoryFilter = null)
        {
            var requestFilter = new PromissoriesPagedApiRequest
            {
                PageNumber = 0,
                PageSize = 10,
                Sort = new List<Sort>
                {
                    new Sort
                    {
                        Direction= 0,
                        Field= "PromissoryNumber",
                        Order= 1
                    }
                },
                PromissoryFilter = promissoryFilter ?? GetPromissoryFilter()
            };
            return requestFilter;
        }

        private PromissoryFilter GetPromissoryFilter(int? typeId = null, string number = "", DateRangeDto promissoryDateRange = null)
        {
            return new PromissoryFilter
            {
                Number = number,
                TypeId = typeId,
                PromissoryDateRange = promissoryDateRange
            };
        }

        #endregion
    }
}