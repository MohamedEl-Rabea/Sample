using Microsoft.EntityFrameworkCore;
using Moj.CMS.Application.AppServices.Claims.Commands.ClaimIncreaseDicrease;
using Moj.CMS.Application.AppServices.Claims.Commands.CloseClaim;
using Moj.CMS.Application.AppServices.Claims.Commands.CreateFinancialClaim;
using Moj.CMS.Domain.Aggregates.Case.ValueObjects;
using Moj.CMS.Domain.ParameterObjects.Case;
using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Domain.Shared.Values;
using Moj.CMS.IntegartionTests.Helpers;
using Moj.CMS.Shared.DTO;
using Moj.CMS.Shared.Wrapper;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Moj.CMS.IntegartionTests
{
    public class Claim_Integration_Tests : CmsIntegrationTestBase<Claim_Integration_Tests>
    {
        public Claim_Integration_Tests(WebApplicationTestHostFactoryFixture<Claim_Integration_Tests> host) : base(host)
        {
        }

        [Fact]
        public async Task Should_Create_Claim_Succefully_Api()
        {
            // Arrange
            var caseParties = new List<CaseParty>();
            var complaintPartyNumber = "1";
            var accusedPartyNumber = "2";
            caseParties.Add(await AddPartyAsync(PartyRoleEnum.OriginalCreditor, complaintPartyNumber));
            caseParties.Add(await AddPartyAsync(PartyRoleEnum.OriginalDebtor, accusedPartyNumber));
            var caseAgg = await CreateCaseAsync(caseParties: caseParties);
            var claimListInput = CreateClaimDtoList(caseAgg.CaseNumber, complaintPartyNumber, accusedPartyNumber);

            // Act
            var response = await PostAsync($"api/case/claim/Create", claimListInput);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.Created);
            UsingDbContext(dbContext =>
            {
                var inputClaim = claimListInput.First();
                var createdClaim = dbContext.Claims.Include(x => x.ClaimDetailsList).FirstOrDefault();
                createdClaim.ShouldNotBeNull();
                createdClaim.CaseNumber.ShouldBe(inputClaim.CaseNumber);
                createdClaim.IsJoint.ShouldBe(inputClaim.Claim.IsJoint);
                createdClaim.ComplaintPartyNumber.ShouldBe(inputClaim.Claim.ComplaintPartyNumber);
                createdClaim.ClaimDateTime.ShouldBe(inputClaim.Claim.ClaimDateTime);
                createdClaim.PromissoryNumber.ShouldBe(inputClaim.PromissoryNumber);
                createdClaim.BasicAmount.ValueEquals(inputClaim.Claim.BasicAmount.ToValueObject()).ShouldBeTrue();
                createdClaim.RemainingAmount.ValueEquals(inputClaim.Claim.RemainingAmount.ToValueObject()).ShouldBeTrue();
                createdClaim.RequiredAmount.ValueEquals(inputClaim.Claim.RequiredAmount.ToValueObject()).ShouldBeTrue();
                createdClaim.TotalRemainingAmount.ValueEquals(inputClaim.Claim.RemainingAmount.ToValueObject()).ShouldBeTrue();
                createdClaim.TotalRequiredAmount.ValueEquals(inputClaim.Claim.RequiredAmount.ToValueObject()).ShouldBeTrue();
                createdClaim.DebtTypeId.ShouldBe(inputClaim.Claim.DebtTypeId);

                var inputClaimDetails = inputClaim.Claim.ClaimDetails.First();
                var createdClaimDetails = createdClaim.ClaimDetailsList.First();
                createdClaimDetails.ShouldNotBeNull();
                createdClaimDetails.PartyNumber.ShouldBe(inputClaimDetails.AccusedPartyNumber);
                createdClaimDetails.RequiredAmount.ValueEquals(inputClaimDetails.RequiredAmount.ToValueObject()).ShouldBeTrue();
                createdClaimDetails.BillingAmount.ValueEquals(inputClaimDetails.BillingAmount.ToValueObject()).ShouldBeTrue();

                var caseHistory = dbContext.CaseHistory.FirstOrDefault();
                caseHistory.CaseNumber.ShouldBe(inputClaim.CaseNumber);
                caseHistory.Operation.ShouldBe(CaseOperationEnum.AddClaim);

                var caseAgg = dbContext.Cases.First(x => x.CaseNumber == createdClaim.CaseNumber);
                caseAgg.ShouldNotBeNull();
                caseAgg.TotalRequiredAmount.ValueEquals(createdClaim.TotalRequiredAmount).ShouldBeTrue();
                caseAgg.TotalRemainingAmount.ValueEquals(createdClaim.TotalRemainingAmount).ShouldBeTrue();
            });
        }

        [Fact]
        public async Task Should_Close_Claim_Successfully_Api()
        {
            // Arrange
            var claim = await CreateClaimAsync();
            var closeClaimInput = CreateCloseClaimDto(claim.Id.ToString());

            // Act
            var response = await PostAsync($"api/case/claim/Close", closeClaimInput);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            UsingDbContext(dbContext =>
            {
                var closedClaim = dbContext.Claims.FirstOrDefault(c => c.Id == claim.Id);
                closedClaim.ShouldNotBeNull();
                closedClaim.CloseDetails.ShouldNotBeNull();
                closedClaim.CloseDetails.ReferenceNumber.ShouldBe(closeClaimInput.ReferenceNumber);
                closedClaim.CloseDetails.CloseDate.ShouldBe(closeClaimInput.CloseDate);
                closedClaim.ClaimStatus.Status.ShouldBe(ClaimStatusEnum.Closed);

                var caseHistory = dbContext.CaseHistory.FirstOrDefault(h => h.CaseNumber == claim.CaseNumber);
                caseHistory.ShouldNotBeNull();
                caseHistory.Operation.ShouldBe(CaseOperationEnum.CloseClaim);
            });
        }

        [Fact]
        public async Task Should_Return_Validation_Errors_Upon_Invalid_Close_Request_Api()
        {
            // Arrange
            var invalidCloseClaimInput = CreateCloseClaimDto(claimNumber: "InvalidClaimNumber", referenceNumber: "");

            // Act
            var response = await PostAsync($"api/case/claim/Close", invalidCloseClaimInput);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            var responseContent = await DeerializeResponseContentAsync<Result>(response);
            responseContent.Succeeded.ShouldBeFalse();
            responseContent.Errors.Count.ShouldBe(2);
            responseContent.Errors.Any(err => err.Contains(nameof(invalidCloseClaimInput.ClaimNumber))).ShouldBeTrue();
            responseContent.Errors.Any(err => err.Contains(nameof(invalidCloseClaimInput.ReferenceNumber))).ShouldBeTrue();
        }

        [Fact]
        public async Task Should_Return_Validation_Errors_Upon_Invalid_Effect_Request_Api()
        {
            // Arrange
            var invalidEffectClaimInput = GetAddClaimEffectInputDto(claimNumber: "InvalidClaimNumber", (FinancialEffectTypeEnum)4, new MoneyDto("SAR", -200));

            // Act
            var response = await PostAsync($"api/case/claim/effect", invalidEffectClaimInput);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            var responseContent = await DeerializeResponseContentAsync<Result>(response);
            responseContent.Succeeded.ShouldBeFalse();
            responseContent.Errors.Count.ShouldBe(3);
            responseContent.Errors.Any(err => err.Contains(nameof(invalidEffectClaimInput.ClaimNumber))).ShouldBeTrue();
            responseContent.Errors.Any(err => err.Contains(nameof(invalidEffectClaimInput.EffectType))).ShouldBeTrue();
            responseContent.Errors.Any(err => err.Contains(nameof(invalidEffectClaimInput.EffectAmount))).ShouldBeTrue();
        }

        [Fact]
        public async Task Should_Add_Claim_Extra_Succefully_Api()
        {
            //Arrange
            var claim = await CreateClaimAsync();
            var addClaimExtraInput = GetAddClaimEffectInputDto(claim.Id.ToString(), FinancialEffectTypeEnum.NewspaperAdvertisement, new MoneyDto("SAR", 200));

            //Act
            var response = await PostAsync($"api/case/claim/Effect", addClaimExtraInput);

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            UsingDbContext(dbContext =>
            {
                var addedAmount = addClaimExtraInput.EffectAmount.ToValueObject();
                var updatedClaim = dbContext.Claims.FirstOrDefault(c => c.Id == claim.Id);
                updatedClaim.ShouldNotBeNull();
                updatedClaim.RequiredAmount.ValueEquals(claim.RequiredAmount).ShouldBeTrue();
                updatedClaim.RemainingAmount.ValueEquals(claim.RemainingAmount).ShouldBeTrue();
                updatedClaim.TotalRequiredAmount.ValueEquals(claim.TotalRequiredAmount.Add(addedAmount)).ShouldBeTrue();
                updatedClaim.TotalRemainingAmount.ValueEquals(claim.TotalRemainingAmount.Add(addedAmount)).ShouldBeTrue();

                updatedClaim.Extras.ShouldNotBeEmpty();
                var addedExtra = updatedClaim.Extras.First();
                addedExtra.Amount.ValueEquals(addedAmount).ShouldBeTrue();
                addedExtra.Remaining.ValueEquals(addedAmount).ShouldBeTrue();
                addedExtra.Type.ShouldBe(addClaimExtraInput.EffectType);

                var caseHistory = dbContext.CaseHistory.FirstOrDefault(h => h.CaseNumber == claim.CaseNumber);
                caseHistory.ShouldNotBeNull();
                caseHistory.Operation.ShouldBe(CaseOperationEnum.EditClaim);
            });
        }

        [Fact]
        public async Task Should_Add_Claim_Discount_Succefully_Api()
        {
            //Arrange
            var claim = await CreateClaimAsync();
            var cliamDiscountInput = GetAddClaimEffectInputDto(claim.Id.ToString(), FinancialEffectTypeEnum.WaiverRecord, new MoneyDto("SAR", 200));

            //Act
            var response = await PostAsync($"api/case/claim/Effect", cliamDiscountInput);

            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            UsingDbContext(dbContext =>
            {
                var discountAmount = cliamDiscountInput.EffectAmount.ToValueObject();
                var updatedClaim = dbContext.Claims.FirstOrDefault(c => c.Id == claim.Id);
                updatedClaim.ShouldNotBeNull();
                updatedClaim.RequiredAmount.ValueEquals(claim.RequiredAmount.Subtract(discountAmount)).ShouldBeTrue();
                updatedClaim.RemainingAmount.ValueEquals(claim.RemainingAmount.Subtract(discountAmount)).ShouldBeTrue();
                updatedClaim.BasicAmount.ValueEquals(claim.BasicAmount).ShouldBeTrue();
                updatedClaim.TotalRequiredAmount.ValueEquals(claim.TotalRequiredAmount.Subtract(discountAmount)).ShouldBeTrue();
                updatedClaim.TotalRemainingAmount.ValueEquals(claim.TotalRemainingAmount.Subtract(discountAmount)).ShouldBeTrue();

                var caseHistory = dbContext.CaseHistory.FirstOrDefault(h => h.CaseNumber == claim.CaseNumber);
                caseHistory.ShouldNotBeNull();
                caseHistory.Operation.ShouldBe(CaseOperationEnum.EditClaim);
            });
        }

        [Fact]
        public async Task Should_Reactivate_Closed_Case_Upon_New_Claim()
        {
            //Arrange
            var caseParties = new List<CaseParty>();
            var complaintPartyNumber = "1";
            var accusedPartyNumber = "2";
            caseParties.Add(await AddPartyAsync(PartyRoleEnum.OriginalCreditor, complaintPartyNumber));
            caseParties.Add(await AddPartyAsync(PartyRoleEnum.OriginalDebtor, accusedPartyNumber));
            var caseAgg = await CreateCaseAsync(caseParties: caseParties, isClosed: true);
            var claimListInput = CreateClaimDtoList(caseAgg.CaseNumber, complaintPartyNumber, accusedPartyNumber);

            // Act
            var response = await PostAsync($"api/case/claim/Create", claimListInput);

            //Assert
            UsingDbContext(dbContext =>
            {
                var createdClaim = dbContext.Claims.FirstOrDefault();
                createdClaim.ShouldNotBeNull();
                createdClaim.CaseNumber.ShouldBe(caseAgg.CaseNumber);

                var activatedCase = dbContext.Cases.FirstOrDefault(c => c.CaseNumber == caseAgg.CaseNumber);
                activatedCase.CaseStatus.ShouldBe(CaseStatusEnum.Active);

                var caseHistoryList = dbContext.CaseHistory.Where(h => h.CaseNumber == caseAgg.CaseNumber).ToList();
                caseHistoryList.ShouldNotBeNull();
                caseHistoryList.Count.ShouldBe(2);
                caseHistoryList.First().Operation.ShouldBe(CaseOperationEnum.AddClaim);
                caseHistoryList.Last().Operation.ShouldBe(CaseOperationEnum.ActivateCase);
            });
        }

        #region Methods

        private static CloseClaimDto CreateCloseClaimDto(string claimNumber, string referenceNumber = "123")
        {
            return new CloseClaimDto
            {
                ClaimNumber = claimNumber,
                ReferenceNumber = referenceNumber,
                CloseDate = CLock.Now
            };
        }

        private async Task<Domain.Aggregates.Case.Case> CreateCaseAsync(List<CaseParty> caseParties, bool isClosed = false)
        {
            var caseAgg = await ObjectFactory.CreateCaseAsync(caseParties: caseParties);
            if (isClosed)
                caseAgg.Close();

            UsingDbContext(dbContext =>
            {
                dbContext.Cases.Add(caseAgg);
                dbContext.SaveChanges();
            });
            return caseAgg;
        }

        private async Task<CaseParty> AddPartyAsync(PartyRoleEnum partyRole, string partyNumber)
        {
            var party = await ObjectFactory.CreatePartyAsync(partyNumber);
            UsingDbContext(dbContext =>
            {
                dbContext.Parties.Add(party);
                dbContext.SaveChanges();
            });

            return CaseParty.Create(new CasePartyCreationParam
            {
                PartyNumber = party.PartyNumber,
                PromissoryNumber = "1",
                PartyRoleTypeId = partyRole,
                IsApplicant = false,
                PartyClassificationId = PartyClassificationEnum.Requester
            }
            );
        }

        private async Task<Domain.Aggregates.Claim.Claim> CreateClaimAsync()
        {
            var complaintParty = await AddPartyAsync(PartyRoleEnum.OriginalCreditor, "1");
            var accusedParty = await AddPartyAsync(PartyRoleEnum.OriginalDebtor, "2");
            var caseAgg = await CreateCaseAsync(new List<CaseParty> { complaintParty, accusedParty });
            var claim = await ObjectFactory.CreateClaimAsync(caseAgg.CaseNumber);
            UsingDbContext(dbContext =>
            {
                dbContext.Claims.Add(claim);
                dbContext.SaveChanges();
            });
            return claim;
        }

        private static List<CreateClaimDto> CreateClaimDtoList(string caseNumber, string complaintPartyNumber, string accusedPartyNumber)
        {
            return new List<CreateClaimDto>
            {
               new CreateClaimDto
                {
                   CaseNumber = caseNumber,
                   Claim = new ClaimDto
                   {
                       ClaimDateTime = CLock.Now,
                       ComplaintPartyNumber = complaintPartyNumber,
                       DebtTypeId = DebtTypeEnum.Expense,
                       IsJoint=true,
                       RequiredAmount = new MoneyDto
                       {
                           Value = 1000,
                           CurrencyIso = "SAR",
                       },
                       RemainingAmount = new MoneyDto
                       {
                           Value = 500,
                           CurrencyIso = "SAR",
                       },
                       BasicAmount = new MoneyDto
                       {
                           Value = 1000,
                           CurrencyIso = "SAR",
                       },
                       ClaimDetails = new List<ClaimDetailsDto>
                       {
                           new ClaimDetailsDto
                           {
                               AccusedPartyNumber = accusedPartyNumber,
                               RequiredAmount = new MoneyDto
                               {
                                   Value = 1000,
                                   CurrencyIso = "SAR",
                               },
                               BillingAmount = new MoneyDto
                               {
                                   Value = 1000,
                                   CurrencyIso = "SAR",
                               }
                           },
                       }
                   },
                   PromissoryNumber = "1"
                }
            };
        }

        private static ClaimEffectInputDto GetAddClaimEffectInputDto(string claimNumber, FinancialEffectTypeEnum financialEffectTypeEnum, MoneyDto moneyDto)
        {
            return new ClaimEffectInputDto
            {
                ClaimNumber = claimNumber,
                EffectAmount = moneyDto,
                EffectType = financialEffectTypeEnum
            };
        }
        #endregion
    }
}