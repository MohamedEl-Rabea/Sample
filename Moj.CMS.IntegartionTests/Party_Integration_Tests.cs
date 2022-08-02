using Moj.CMS.Application.AppServices.Party.Commands.AddParty;
using Moj.CMS.Application.AppServices.Party.Queries;
using Moj.CMS.Domain.Aggregates.Party;
using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Domain.Shared.Values;
using Moj.CMS.IntegartionTests.Helpers;
using Moj.CMS.Shared.Requests;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using Moj.CMS.Shared.Wrapper;
using Moj.CMS.Shared.DTO;

namespace Moj.CMS.IntegartionTests
{
    public class Party_Integration_Tests : CmsIntegrationTestBase<Party_Integration_Tests>
    {
        public Party_Integration_Tests(WebApplicationTestHostFactoryFixture<Party_Integration_Tests> host) : base(host)
        {

        }

        [Fact]
        public async Task Should_Create_Party_successfully_Api()
        {
            // Arrange
            var partiesListInput = GetPartiesDtoList("1", "2");

            // Act
            var response = await PostAsync($"api/party/create", partiesListInput);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.Created);
            UsingDbContext(dbContext =>
            {
                var inputParty = partiesListInput.First();
                var createdParty = dbContext.Parties.FirstOrDefault();
                createdParty.ShouldNotBeNull();
                createdParty.PartyNumber.ShouldBe(inputParty.PartyNumber);
                createdParty.PartyLocationId.ShouldBe(inputParty.PartyLocationId);
                createdParty.FullName.ShouldBe(inputParty.FullName);
                createdParty.Gender.ShouldBe(inputParty.Gender);
                createdParty.NationalityCode.ShouldBe(inputParty.NationalityCode);
                createdParty.PartyTypeId.ShouldBe(inputParty.PartyTypeId);
                createdParty.DateOfBirth.ShouldBe(inputParty.DateOfBirth);
                createdParty.CurrentIdentityNumber.ShouldBe(inputParty.PartyIdentityNumber);
            });
        }

        [Fact]
        public async Task Should_Create_Parties_List_Contains_Existing_Party_successfully_Api()
        {
            // Arrange
            var newPartyNumber = "3";
            var createdParty = await CreateParty();
            var partiesListInput = GetPartiesDtoList(createdParty.PartyNumber, newPartyNumber);

            // Act
            var response = await PostAsync($"api/party/create", partiesListInput);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.Conflict);

            UsingDbContext(dbContext =>
            {
                var inputExistsParty = partiesListInput.First();
                var existsParty = dbContext.Parties.FirstOrDefault();
                existsParty.ShouldNotBeNull();
                existsParty.FullName.ShouldBe(createdParty.FullName);
                existsParty.CurrentIdentityNumber.ShouldBe(createdParty.CurrentIdentityNumber);

                var notExistsParty = dbContext.Parties.FirstOrDefault(c => c.PartyNumber == newPartyNumber);
                notExistsParty.ShouldBeNull();
            });
        }

        [Fact]
        public async Task Should_Get_All_Parties_Paged_Successfully()
        {
            // Arrange
            int count = 15;
            await CreatePartyListForQueryAsync(count);
            var partyFilter = GetPartyFilter();
            var requestFilter = GetPartiesPagedApiRequest(partyFilter);

            // Act
            var response = await GetAsync<PagedResult<PartyListItemDto>>($"api/party/Get", requestFilter);

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
        public async Task Should_Get_All_Parties_Filtered_By_Total_Credit_Amount_Range_Successfully()
        {
            // Arrange
            NumberRangeDto creditRange = new NumberRangeDto
            {
                Min = 1000,
                Max = 50000
            };

            decimal minMatchedCreatedCredit = 1000, underMinCreated = 100,
                    inBetweenMatchedCreatedCredit = 10000, exceededCreatedCredit = 100000;

            await CreatePartyListForQueryAsync(1, credit: minMatchedCreatedCredit);
            await CreatePartyListForQueryAsync(1, credit: inBetweenMatchedCreatedCredit);
            await CreatePartyListForQueryAsync(1, credit: exceededCreatedCredit);
            await CreatePartyListForQueryAsync(1, credit: underMinCreated);
            var partyFilter = GetPartyFilter(creditRange: creditRange);
            var requestFilter = GetPartiesPagedApiRequest(partyFilter);

            // Act
            var response = await GetAsync<PagedResult<PartyListItemDto>>($"api/party/Get", requestFilter);

            // Assert
            response.Succeeded.ShouldBeTrue();
            response.Data.TotalPages.ShouldBe(1);
            response.Data.TotalCount.ShouldBe(2);
            response.Data.PageSize.ShouldBe(requestFilter.PageSize);
            response.Data.CurrentPage.ShouldBe(requestFilter.PageNumber + 1);
            response.Data.Data.Count.ShouldBe(2);
            response.Data.Data.ShouldNotContain(c => c.TotalCreditAmountValue > creditRange.Max);
            response.Data.Data.ShouldNotContain(c => c.TotalCreditAmountValue < creditRange.Min);
        }

        [Fact]
        public async Task Should_Get_All_Parties_Filtered_By_Total_Credit_Amount_WithMaxOnly_Successfully()
        {
            // Arrange
            NumberRangeDto creditRange = new NumberRangeDto
            {
                Max = 50000
            };

            decimal minMatchedCreatedCredit = 1000, underMinCreated = 100,
                    inBetweenMatchedCreatedCredit = 10000, exceededCreatedCredit = 100000;

            await CreatePartyListForQueryAsync(1, credit: minMatchedCreatedCredit);
            await CreatePartyListForQueryAsync(1, credit: inBetweenMatchedCreatedCredit);
            await CreatePartyListForQueryAsync(1, credit: exceededCreatedCredit);
            await CreatePartyListForQueryAsync(1, credit: underMinCreated);
            var partyFilter = GetPartyFilter(creditRange: creditRange);
            var requestFilter = GetPartiesPagedApiRequest(partyFilter);

            // Act
            var response = await GetAsync<PagedResult<PartyListItemDto>>($"api/party/Get", requestFilter);

            // Assert
            response.Succeeded.ShouldBeTrue();
            response.Data.TotalPages.ShouldBe(1);
            response.Data.TotalCount.ShouldBe(3);
            response.Data.PageSize.ShouldBe(requestFilter.PageSize);
            response.Data.CurrentPage.ShouldBe(requestFilter.PageNumber + 1);
            response.Data.Data.Count.ShouldBe(3);
            response.Data.Data.ShouldContain(c => c.TotalCreditAmountValue <= creditRange.Max);
            response.Data.Data.ShouldNotContain(c => c.TotalCreditAmountValue > creditRange.Max);
        }

        [Fact]
        public async Task Should_Get_All_Parties_Filtered_By_Total_Credit_Amount_WithMinOnly_Successfully()
        {
            // Arrange
            NumberRangeDto creditRange = new NumberRangeDto
            {
                Min = 1000
            };

            decimal minMatchedCreatedCredit = 1000, underMinCreated = 100,
                    inBetweenMatchedCreatedCredit = 10000, exceededCreatedCredit = 100000;

            await CreatePartyListForQueryAsync(1, credit: minMatchedCreatedCredit);
            await CreatePartyListForQueryAsync(1, credit: inBetweenMatchedCreatedCredit);
            await CreatePartyListForQueryAsync(1, credit: exceededCreatedCredit);
            await CreatePartyListForQueryAsync(1, credit: underMinCreated);
            var partyFilter = GetPartyFilter(creditRange: creditRange);
            var requestFilter = GetPartiesPagedApiRequest(partyFilter);

            // Act
            var response = await GetAsync<PagedResult<PartyListItemDto>>($"api/party/Get", requestFilter);

            // Assert
            response.Succeeded.ShouldBeTrue();
            response.Data.TotalPages.ShouldBe(1);
            response.Data.TotalCount.ShouldBe(3);
            response.Data.PageSize.ShouldBe(requestFilter.PageSize);
            response.Data.CurrentPage.ShouldBe(requestFilter.PageNumber + 1);
            response.Data.Data.Count.ShouldBe(3);
            response.Data.Data.ShouldContain(c => c.TotalCreditAmountValue >= creditRange.Min);
            response.Data.Data.ShouldNotContain(c => c.TotalCreditAmountValue < creditRange.Min);
        }

        [Fact]
        public async Task Should_Get_All_Parties_Filtered_By_Total_Debt_Amount_Range_Successfully()
        {
            // Arrange
            NumberRangeDto debtRange = new NumberRangeDto
            {
                Min = 1000,
                Max = 50000
            };

            decimal minMatchedCreatedDebt = 1000, underMinCreated = 100,
                    inBetweenMatchedCreatedDebt = 10000, exceededCreatedCredit = 100000;

            await CreatePartyListForQueryAsync(1, debt: minMatchedCreatedDebt);
            await CreatePartyListForQueryAsync(1, debt: inBetweenMatchedCreatedDebt);
            await CreatePartyListForQueryAsync(1, debt: exceededCreatedCredit);
            await CreatePartyListForQueryAsync(1, debt: underMinCreated);
            var partyFilter = GetPartyFilter(debtRange: debtRange);
            var requestFilter = GetPartiesPagedApiRequest(partyFilter);

            // Act
            var response = await GetAsync<PagedResult<PartyListItemDto>>($"api/party/Get", requestFilter);

            // Assert
            response.Succeeded.ShouldBeTrue();
            response.Data.TotalPages.ShouldBe(1);
            response.Data.TotalCount.ShouldBe(2);
            response.Data.PageSize.ShouldBe(requestFilter.PageSize);
            response.Data.CurrentPage.ShouldBe(requestFilter.PageNumber + 1);
            response.Data.Data.Count.ShouldBe(2);
            response.Data.Data.ShouldNotContain(c => c.TotalDebtAmountValue > debtRange.Max);
            response.Data.Data.ShouldNotContain(c => c.TotalDebtAmountValue < debtRange.Min);
        }

        [Fact]
        public async Task Should_Get_All_Parties_Filtered_By_Total_Debt_Amount_WithMaxOnly_Successfully()
        {
            // Arrange
            NumberRangeDto debtRange = new NumberRangeDto
            {
                Max = 50000
            };

            decimal minMatchedCreatedDebt = 1000, underMinCreated = 100,
                    inBetweenMatchedCreatedDebt = 10000, exceededCreatedCredit = 100000;

            await CreatePartyListForQueryAsync(1, debt: minMatchedCreatedDebt);
            await CreatePartyListForQueryAsync(1, debt: inBetweenMatchedCreatedDebt);
            await CreatePartyListForQueryAsync(1, debt: exceededCreatedCredit);
            await CreatePartyListForQueryAsync(1, debt: underMinCreated);
            var partyFilter = GetPartyFilter(debtRange: debtRange);
            var requestFilter = GetPartiesPagedApiRequest(partyFilter);

            // Act
            var response = await GetAsync<PagedResult<PartyListItemDto>>($"api/party/Get", requestFilter);

            // Assert
            response.Succeeded.ShouldBeTrue();
            response.Data.TotalPages.ShouldBe(1);
            response.Data.TotalCount.ShouldBe(3);
            response.Data.PageSize.ShouldBe(requestFilter.PageSize);
            response.Data.CurrentPage.ShouldBe(requestFilter.PageNumber + 1);
            response.Data.Data.Count.ShouldBe(3);
            response.Data.Data.ShouldContain(c => c.TotalDebtAmountValue <= debtRange.Max);
            response.Data.Data.ShouldNotContain(c => c.TotalDebtAmountValue > debtRange.Max);
        }

        [Fact]
        public async Task Should_Get_All_Parties_Filtered_By_Total_Debt_Amount_WithMinOnly_Successfully()
        {
            // Arrange
            NumberRangeDto debtRange = new NumberRangeDto
            {
                Min = 1000
            };

            decimal minMatchedCreatedDebt = 1000, underMinCreated = 100,
                    inBetweenMatchedCreatedDebt = 10000, exceededCreatedCredit = 100000;

            await CreatePartyListForQueryAsync(1, debt: minMatchedCreatedDebt);
            await CreatePartyListForQueryAsync(1, debt: inBetweenMatchedCreatedDebt);
            await CreatePartyListForQueryAsync(1, debt: exceededCreatedCredit);
            await CreatePartyListForQueryAsync(1, debt: underMinCreated);
            var partyFilter = GetPartyFilter(debtRange: debtRange);
            var requestFilter = GetPartiesPagedApiRequest(partyFilter);

            // Act
            var response = await GetAsync<PagedResult<PartyListItemDto>>($"api/party/Get", requestFilter);

            // Assert
            response.Succeeded.ShouldBeTrue();
            response.Data.TotalPages.ShouldBe(1);
            response.Data.TotalCount.ShouldBe(3);
            response.Data.PageSize.ShouldBe(requestFilter.PageSize);
            response.Data.CurrentPage.ShouldBe(requestFilter.PageNumber + 1);
            response.Data.Data.Count.ShouldBe(3);
            response.Data.Data.ShouldContain(c => c.TotalDebtAmountValue >= debtRange.Min);
            response.Data.Data.ShouldNotContain(c => c.TotalDebtAmountValue < debtRange.Min);
        }

        [Fact]
        public async Task Should_Get_All_Parties_Filtered_By_IdentityNumber_Successfully()
        {
            // Arrange
            int count = 15;
            var createdParties = await CreatePartyListForQueryAsync(count);
            var createdPartyIdentityNumberFilter = createdParties.Last().CurrentIdentityNumber;
            var partyFilter = GetPartyFilter(identityNumber: createdPartyIdentityNumberFilter);
            var requestFilter = GetPartiesPagedApiRequest(partyFilter);

            // Act
            var response = await GetAsync<PagedResult<PartyListItemDto>>($"api/party/Get", requestFilter);

            // Assert
            response.Succeeded.ShouldBeTrue();
            response.Data.TotalPages.ShouldBe(1);
            response.Data.TotalCount.ShouldBe(1);
            response.Data.PageSize.ShouldBe(requestFilter.PageSize);
            response.Data.CurrentPage.ShouldBe(requestFilter.PageNumber + 1);
            response.Data.Data.Count.ShouldBe(1);
        }

        [Fact]
        public async Task Should_Get_All_Parties_Filtered_By_PartyType_Successfully()
        {
            // Arrange
            int individualPartyTypeCount = 15, companyPartyTypeCount = 10,
               saudiNationalIdCount = 15, passportCount = 10;

            await CreatePartyListForQueryAsync(individualPartyTypeCount, partyType: PartyTypeEnum.Individual, identityTypeEnum: PartyIdentityTypeEnum.Passport);
            await CreatePartyListForQueryAsync(companyPartyTypeCount, partyType: PartyTypeEnum.Company, identityTypeEnum: PartyIdentityTypeEnum.Passport);
            await CreatePartyListForQueryAsync(saudiNationalIdCount, partyType: PartyTypeEnum.Individual, identityTypeEnum: PartyIdentityTypeEnum.SaudiNationalId);
            await CreatePartyListForQueryAsync(passportCount, partyType: PartyTypeEnum.Individual, identityTypeEnum: PartyIdentityTypeEnum.Passport);
            var partyFilter = GetPartyFilter(partyTypeEnum: PartyTypeEnum.Individual);
            var requestFilter = GetPartiesPagedApiRequest(partyFilter);

            // Act
            var response = await GetAsync<PagedResult<PartyListItemDto>>($"api/party/Get", requestFilter);

            // Assert
            var expectedCount = individualPartyTypeCount + passportCount + saudiNationalIdCount;
            var expectedPageCount = (int)Math.Ceiling((expectedCount / (double)requestFilter.PageSize));
            var expectedPageDataCount = expectedCount > requestFilter.PageSize ? requestFilter.PageSize : expectedCount;


            response.Succeeded.ShouldBeTrue();
            response.Data.TotalPages.ShouldBe(expectedPageCount);
            response.Data.TotalCount.ShouldBe(expectedCount);
            response.Data.PageSize.ShouldBe(requestFilter.PageSize);
            response.Data.CurrentPage.ShouldBe(requestFilter.PageNumber + 1);
            response.Data.Data.Count.ShouldBe(expectedPageDataCount);
            response.Data.Data.ShouldAllBe(p => p.PartyTypeId == (int)PartyTypeEnum.Individual);

        }


        [Fact]
        public async Task Should_Get_All_Parties_Filtered_By_IdentityType_And_IdentityNumber_Successfully()
        {
            // Arrange
            int individualPartyTypeCount = 15, companyPartyTypeCount = 15, saudiNationalIdCount = 15, passportCount = 10;
            var individualPartyTypeWithPartyIdentity = await CreatePartyListForQueryAsync(individualPartyTypeCount, partyType: PartyTypeEnum.Individual, identityTypeEnum: PartyIdentityTypeEnum.Passport);
            var IdentityNumber = individualPartyTypeWithPartyIdentity.Last().CurrentIdentityNumber;
            await CreatePartyListForQueryAsync(companyPartyTypeCount, partyType: PartyTypeEnum.Company, identityTypeEnum: PartyIdentityTypeEnum.Passport);
            await CreatePartyListForQueryAsync(saudiNationalIdCount, partyType: PartyTypeEnum.Individual, identityTypeEnum: PartyIdentityTypeEnum.SaudiNationalId);
            await CreatePartyListForQueryAsync(passportCount, partyType: PartyTypeEnum.Individual, identityTypeEnum: PartyIdentityTypeEnum.Passport);
            var partyFilter = GetPartyFilter(partyIdentityType: PartyIdentityTypeEnum.Passport, identityNumber: IdentityNumber);
            var requestFilter = GetPartiesPagedApiRequest(partyFilter);

            // Act
            var response = await GetAsync<PagedResult<PartyListItemDto>>($"api/party/Get", requestFilter);

            // Assert
            response.Succeeded.ShouldBeTrue();
            response.Data.TotalPages.ShouldBe(1);
            response.Data.TotalCount.ShouldBe(1);
            response.Data.PageSize.ShouldBe(requestFilter.PageSize);
            response.Data.CurrentPage.ShouldBe(requestFilter.PageNumber + 1);
            response.Data.Data.Count.ShouldBe(1);
            response.Data.Data.First().IdentityNumber.ShouldBe(requestFilter.PartyFilter.IdentityNumber);
            response.Data.Data.First().IdentityTypeId.Equals(requestFilter.PartyFilter.IdentityTypeId);
        }

        private PartiesPagedApiRequest GetPartiesPagedApiRequest(PartyFilter partyFilter = null)
        {
            var requestFilter = new PartiesPagedApiRequest()
            {
                PageNumber = 0,
                PageSize = 10,
                Sort = new List<Sort>
                {
                    new Sort
                    {
                        Direction= 0,
                        Field= "Number",
                        Order= 1
                    }
                },
                PartyFilter = partyFilter ?? GetPartyFilter()

            };
            return requestFilter;
        }

        private PartyFilter GetPartyFilter(string identityNumber = null, PartyIdentityTypeEnum? partyIdentityType = null, PartyTypeEnum? partyTypeEnum = null, NumberRangeDto creditRange = null, NumberRangeDto debtRange = null)
        {
            return new PartyFilter
            {
                IdentityNumber = identityNumber,
                Name = null,
                NationalityCode = null,
                IdentityTypeId = (int?)partyIdentityType ?? null,
                Number = null,
                PartyTypeId = (int?)partyTypeEnum ?? null,
                TotalCreditAmount = creditRange,
                TotalDebtAmount = debtRange,
            };
        }

        private async Task<IEnumerable<Party>> CreatePartyListForQueryAsync(int count = 10, PartyIdentityTypeEnum identityTypeEnum = PartyIdentityTypeEnum.Passport, PartyTypeEnum partyType = PartyTypeEnum.Individual, decimal credit = default, decimal debt = default)
        {
            List<Party> createdPartyList = new List<Party>();

            for (int partyIndex = 1; partyIndex <= count; partyIndex++)
            {
                var createParty = await CreateParty(partyNumber: partyIndex.ToString(), partyType: partyType, identityTypeEnum: identityTypeEnum, credit: credit, debt: debt);
                createdPartyList.Add(createParty);
            }
            return createdPartyList;
        }

        private async Task<Party> CreateParty(string partyNumber = "123456", PartyIdentityTypeEnum identityTypeEnum = PartyIdentityTypeEnum.SaudiNationalId, PartyTypeEnum partyType = PartyTypeEnum.Individual, decimal credit = 0, decimal debt = 0)
        {
            var party = await ObjectFactory.CreatePartyAsync(partyNumber, partyType: partyType, identityTypeEnum: identityTypeEnum, credit: credit, debt: debt);

            UsingDbContext(dbContext =>
            {
                dbContext.Parties.Add(party);
                dbContext.SaveChanges();
            });
            return party;
        }

        private static List<PartyDto> GetPartiesDtoList(params string[] partiesNumbers)
        {
            var partiesList = new List<PartyDto>();
            foreach (var partyNumber in partiesNumbers)
            {
                partiesList.Add(new PartyDto
                {
                    PartyNumber = partyNumber,
                    FullName = $"partyNumber-{partyNumber}-TestFullName",
                    NationalityCode = $"Sar",
                    PartyIdentityNumber = $"Identity-{partyNumber}",
                    Gender = Gender.Male,
                    DateOfBirth = CLock.Now,
                    PartyIdentityTypeId = PartyIdentityTypeEnum.BorderNumber,
                    PartyLocationId = PartyLocationEnum.InsideGulf,
                    PartyTypeId = PartyTypeEnum.Individual
                });
            }
            return partiesList;
        }
    }
}
