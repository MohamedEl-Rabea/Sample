using Moj.CMS.Domain.Aggregates.Case.Events;
using Moj.CMS.Domain.Aggregates.Claim.BusinessRules;
using Moj.CMS.Domain.Aggregates.Claim.Entities;
using Moj.CMS.Domain.Aggregates.Claim.Events;
using Moj.CMS.Domain.Aggregates.Claim.ValueObjects;
using Moj.CMS.Domain.BusinessRules;
using Moj.CMS.Domain.DomainServices;
using Moj.CMS.Domain.DomainServices.Party;
using Moj.CMS.Domain.ParameterObjects.Claim;
using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Domain.Shared.Values;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Moj.CMS.Domain.Tests.Claim
{
    public class ClaimTests : UnitTestBase
    {
        public ClaimTests()
        {

        }

        [Fact]
        public async Task Should_Create_Claim_Successfully()
        {
            //Arrange
            var param = GetClaimCreationParameter();

            //Act
            var claimAgg = await Aggregates.Claim.Claim.Create(param);

            //Assert
            claimAgg.CaseNumber.ShouldBe(param.CaseNumber);
            claimAgg.ClaimDetailsList.SequenceEqual(param.ClaimDetailsList);
            claimAgg.ClaimDateTime.ShouldBe(param.ClaimDate);
            claimAgg.ComplaintPartyNumber.ShouldBe(param.ComplaintPartyNumber);
            claimAgg.PromissoryNumber.ShouldBe(param.PromissoryNumber);
            claimAgg.RemainingAmount.ShouldBe(param.RemainingAmount);
            claimAgg.RequiredAmount.ShouldBe(param.RequiredAmount);
            claimAgg.DebtTypeId.ShouldBe(param.DebtTypeId);
        }

        [Fact]
        public async Task Should_Close_Claim_Successfully()
        {
            //Arrange
            var createParam = GetClaimCreationParameter();
            var claimAgg = await Aggregates.Claim.Claim.Create(createParam);
            var closeReferenceNumber = "123";
            var closeDate = CLock.Now;

            //Act
            claimAgg.Close(closeReferenceNumber, closeDate);

            //Assert
            claimAgg.CloseDetails.ReferenceNumber.ShouldBe(closeReferenceNumber);
            claimAgg.CloseDetails.CloseDate.ShouldBe(closeDate);
            claimAgg.ClaimStatus.Status.ShouldBe(ClaimStatusEnum.Closed);
        }

        [Fact]
        public async Task Should_Not_Override_Close_Details_For_Closed_Claim()
        {
            //Arrange
            var createParam = GetClaimCreationParameter();
            var claimAgg = await Aggregates.Claim.Claim.Create(createParam);
            var oldCloseReferenceNumber = "123";
            var oldCloseDate = CLock.Now.AddDays(-2);
            claimAgg.Close(oldCloseReferenceNumber, oldCloseDate);

            //Act
            claimAgg.Close(oldCloseReferenceNumber + "12", oldCloseDate.AddDays(2));

            //Assert
            AssertRegisteredSingleDomainEvent<ClaimClosedDomainEvent>(claimAgg);
            claimAgg.CloseDetails.ReferenceNumber.ShouldBe(oldCloseReferenceNumber);
            claimAgg.CloseDetails.CloseDate.ShouldBe(oldCloseDate);
            claimAgg.ClaimStatus.Status.ShouldBe(ClaimStatusEnum.Closed);
        }

        [Fact]
        public async Task Should_Register_Domain_Event_Claim_Closed_Upon_Close_Claim()
        {
            //Arrange
            var createParam = GetClaimCreationParameter();
            var claimAgg = await Aggregates.Claim.Claim.Create(createParam);
            var closeReferenceNumber = "123";
            var closeDate = CLock.Now;

            //Act
            claimAgg.Close(closeReferenceNumber, closeDate);

            //Assert
            var registeredDomainEvent = AssertRegisteredSingleDomainEvent<ClaimClosedDomainEvent>(claimAgg);
            registeredDomainEvent.CaseNumber.ShouldBe(claimAgg.CaseNumber);
            registeredDomainEvent.Operation.ShouldBe(CaseOperationEnum.CloseClaim);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task Should_Register_Domain_Event_Claim_Created_Upon_Create_Claim(bool ShouldRegisterDomainEvent)
        {
            //Arrange
            var param = GetClaimCreationParameter(true, ShouldRegisterDomainEvent);

            //Act
            var claimAgg = await Aggregates.Claim.Claim.Create(param);

            //Assert
            if (ShouldRegisterDomainEvent)
            {
                var registeredDomainEvent = AssertRegisteredSingleDomainEvent<ClaimCreatedDomainEvent>(claimAgg);
                registeredDomainEvent.CaseNumber.ShouldBe(param.CaseNumber);
                registeredDomainEvent.Operation.ShouldBe(CaseOperationEnum.AddClaim);
                registeredDomainEvent.ClaimDetailsList.SequenceEqual(param.ClaimDetailsList);
            }
            else
                AssertNotRegisteredDomainEvent<ClaimCreatedDomainEvent>(claimAgg);
        }

        [Fact]
        public async Task Should_Statisfy_Business_Rule_CaseExistBusniessRule()
        {
            //Arrange
            var param = GetClaimCreationParameter(caseIsFound: false);

            //Act
            Func<Task> createClaim = async () =>
            {
                await Aggregates.Claim.Claim.Create(param);
            };

            //Assert
            await AssertBrokenRuleAsync<CaseExistBusniessRule>(createClaim());
        }

        [Fact]
        public async Task Should_Add_Claim_Extra_Successfully()
        {
            //Arrange
            var createParam = GetClaimCreationParameter(ShouldRegisterDomainEvent: false);
            var claim = await Aggregates.Claim.Claim.Create(createParam);
            var totalRequeridAmountBefore = claim.TotalRequiredAmount;
            var totalRemainingAmountBefore = claim.TotalRemainingAmount;
            var claimExtra = CreateClaimExtra();

            //Act
            claim.AddExtra(claimExtra);

            //Assert
            var addedClaimExtra = claim.Extras.FirstOrDefault();
            addedClaimExtra.ShouldNotBeNull();
            addedClaimExtra.Amount.ValueEquals(claimExtra.Remaining).ShouldBeTrue();
            addedClaimExtra.Remaining.ValueEquals(claimExtra.Remaining).ShouldBeTrue();
            addedClaimExtra.Type.ShouldBe(claimExtra.Type);
            claim.TotalRequiredAmount.ValueEquals(claimExtra.Amount.Add(totalRequeridAmountBefore)).ShouldBeTrue();
            claim.TotalRemainingAmount.ValueEquals(claimExtra.Remaining.Add(totalRemainingAmountBefore)).ShouldBeTrue();

            var addedClaimHistory = claim.ClaimHistoryList.FirstOrDefault();
            addedClaimHistory.ShouldNotBeNull();
            addedClaimHistory.EffectType.ShouldBe(claimExtra.Type);
            addedClaimHistory.TotalAmountBefore.ValueEquals(totalRequeridAmountBefore).ShouldBeTrue();
            addedClaimHistory.TotalAmountAfter.ValueEquals(claimExtra.Remaining.Add(totalRequeridAmountBefore)).ShouldBeTrue();
        }

        [Fact]
        public async Task Should_Register_Domain_Event_Claim_Updated_Upon_Add_Extra()
        {
            //Arrange
            var createParam = GetClaimCreationParameter(ShouldRegisterDomainEvent: false);
            var claimAgg = await Aggregates.Claim.Claim.Create(createParam);
            var totalRequeridAmountBefore = claimAgg.TotalRequiredAmount;
            var totalRemainingAmountBefore = claimAgg.TotalRemainingAmount;
            var claimExtra = CreateClaimExtra();

            //Act
            claimAgg.AddExtra(claimExtra);

            //Assert
            var registeredDomainEvent = AssertRegisteredSingleDomainEvent<ClaimUpdatedDomainEvent>(claimAgg);
            registeredDomainEvent.CaseNumber.ShouldBe(claimAgg.CaseNumber);
            registeredDomainEvent.OldTotalRequiredAmount.ValueEquals(totalRequeridAmountBefore).ShouldBeTrue();
            registeredDomainEvent.OldTotalRemainingAmount.ValueEquals(totalRemainingAmountBefore).ShouldBeTrue();
            registeredDomainEvent.Operation.ShouldBe(CaseOperationEnum.EditClaim);
        }

        [Fact]
        public void Should_Statisfy_Business_Rule_ExtraTypeMustBeIncrementalBusniessRule()
        {
            //Arrange
            //Act
            Action createClaimExtra = () =>
            {
                CreateClaimExtra(financialEffectTypeEnum: FinancialEffectTypeEnum.WaiverRecord);
            };

            //Assert
            AssertBrokenRule<ExtraTypeMustBeIncremental>(createClaimExtra);
        }

        [Fact]
        public async Task Should_Add_Claim_Discount_Successfully()
        {
            //Arrange
            var createParam = GetClaimCreationParameter(ShouldRegisterDomainEvent: false);
            var claim = await Aggregates.Claim.Claim.Create(createParam);
            var requiredAmountBefore = claim.RequiredAmount;
            var remainingAmountBefore = claim.RemainingAmount;
            var totalRequeridAmountBefore = claim.TotalRequiredAmount;
            var totalRemainingAmountBefore = claim.TotalRemainingAmount;
            var effectType = FinancialEffectTypeEnum.WaiverRecord;
            var discountAmount = Money.Create("SAR", 200);

            //Act
            claim.Discount(discountAmount, effectType);

            //Assert
            claim.Extras.ShouldBeEmpty();
            claim.RequiredAmount.ValueEquals(requiredAmountBefore.Subtract(discountAmount)).ShouldBeTrue();
            claim.RemainingAmount.ValueEquals(remainingAmountBefore.Subtract(discountAmount)).ShouldBeTrue();
            claim.TotalRequiredAmount.ValueEquals(totalRequeridAmountBefore.Subtract(discountAmount)).ShouldBeTrue();
            claim.TotalRemainingAmount.ValueEquals(totalRemainingAmountBefore.Subtract(discountAmount)).ShouldBeTrue();

            var addedClaimHistory = claim.ClaimHistoryList.FirstOrDefault();
            addedClaimHistory.ShouldNotBeNull();
            addedClaimHistory.EffectType.ShouldBe(effectType);
            addedClaimHistory.TotalAmountBefore.ValueEquals(totalRequeridAmountBefore).ShouldBeTrue();
            addedClaimHistory.TotalAmountAfter.ValueEquals(totalRequeridAmountBefore.Subtract(discountAmount)).ShouldBeTrue();
        }

        [Fact]
        public async Task Should_Register_Domain_Event_Claim_Updated_Upon_Discount()
        {
            //Arrange
            var createParam = GetClaimCreationParameter(ShouldRegisterDomainEvent: false);
            var claim = await Aggregates.Claim.Claim.Create(createParam);
            var totalRequeridAmountBefore = claim.TotalRequiredAmount;
            var totalRemainingAmountBefore = claim.TotalRemainingAmount;
            var effectType = FinancialEffectTypeEnum.WaiverRecord;
            var discountAmount = Money.Create("SAR", 200);

            //Act
            claim.Discount(discountAmount, effectType);

            //Assert
            var registeredDomainEvent = AssertRegisteredSingleDomainEvent<ClaimUpdatedDomainEvent>(claim);
            registeredDomainEvent.CaseNumber.ShouldBe(claim.CaseNumber);
            registeredDomainEvent.OldTotalRequiredAmount.ValueEquals(totalRequeridAmountBefore).ShouldBeTrue();
            registeredDomainEvent.OldTotalRemainingAmount.ValueEquals(totalRemainingAmountBefore).ShouldBeTrue();
            registeredDomainEvent.Operation.ShouldBe(CaseOperationEnum.EditClaim);
        }

        [Fact]
        public async Task Should_Statisfy_Business_Rule_DiscountTypeMustBeDecrementalBusniessRule()
        {
            //Arrange
            var createParam = GetClaimCreationParameter(ShouldRegisterDomainEvent: false);
            var claim = await Aggregates.Claim.Claim.Create(createParam);
            var discountAmount = Money.Create("SAR", 200);

            //Act
            Action addClaimDiscount = () =>
            {
                claim.Discount(discountAmount, FinancialEffectTypeEnum.NewspaperAdvertisement);
            };

            //Assert
            AssertBrokenRule<DiscountTypeMustBeDecremental>(addClaimDiscount);
        }

        [Fact]
        public async Task Should_Register_ClaimAmount_Updated_DomainEvent_Upon_Claim_Creation()
        {
            //Arrange
            var createParam = GetClaimCreationParameter();

            //Act
            var claimAgg = await Aggregates.Claim.Claim.Create(createParam);
            var oldTotalRequiredAmount = Money.Empty;
            var oldTotalRemainingAmount = Money.Empty;

            //Assert
            var registeredDomainEvent = AssertRegisteredSingleDomainEvent<ClaimAmountUpdatedDomainEvent>(claimAgg);
            registeredDomainEvent.OldTotalRequiredAmount.ValueEquals(oldTotalRequiredAmount).ShouldBeTrue();
            registeredDomainEvent.OldTotalRemainingAmount.ValueEquals(oldTotalRemainingAmount).ShouldBeTrue();
        }


        [Fact]
        public async Task Should_Register_ClaimAmount_Updated_Domain_Event_Upon_Add_Extra()
        {
            //Arrange
            var createParam = GetClaimCreationParameter(ShouldRegisterDomainEvent: false);
            var claimAgg = await Aggregates.Claim.Claim.Create(createParam);
            var oldTotalRequiredAmount = claimAgg.TotalRequiredAmount.Clone();
            var oldTotalRemainingAmount = claimAgg.TotalRemainingAmount.Clone();
            var claimExtra = CreateClaimExtra();

            //Act
            claimAgg.AddExtra(claimExtra);

            //Assert
            var registeredDomainEvent = GetRegisteredDomainEvents<ClaimAmountUpdatedDomainEvent>(claimAgg).First(x => x.OldTotalRemainingAmount.Value == 1000);
            registeredDomainEvent.OldTotalRequiredAmount.ValueEquals(oldTotalRequiredAmount).ShouldBeTrue();
            registeredDomainEvent.OldTotalRemainingAmount.ValueEquals(oldTotalRemainingAmount).ShouldBeTrue();
        }

        [Fact]
        public async Task Should_Register_ClaimAmount_Updated_Domain_Event_Upon_Discount()
        {
            //Arrange
            var createParam = GetClaimCreationParameter(ShouldRegisterDomainEvent: false);
            var claimAgg = await Aggregates.Claim.Claim.Create(createParam);
            var oldTotalRequiredAmount = claimAgg.TotalRequiredAmount.Clone();
            var oldTotalRemainingAmount = claimAgg.TotalRemainingAmount.Clone();
            var effectType = FinancialEffectTypeEnum.WaiverRecord;
            var discountAmount = Money.Create("SAR", 200);

            //Act
            claimAgg.Discount(discountAmount, effectType);

            //Assert
            var registeredDomainEvent = GetRegisteredDomainEvents<ClaimAmountUpdatedDomainEvent>(claimAgg).First(x => x.OldTotalRemainingAmount.Value == 1000);
            registeredDomainEvent.OldTotalRequiredAmount.ValueEquals(oldTotalRequiredAmount).ShouldBeTrue();
            registeredDomainEvent.OldTotalRemainingAmount.ValueEquals(oldTotalRemainingAmount).ShouldBeTrue();
        }



        #region Methods

        private CreateClaimParam GetClaimCreationParameter(bool caseIsFound = true, bool ShouldRegisterDomainEvent = true)
        {
            var enforceCaseIsFound = new Mock<IEnforceCaseIsFound>();
            enforceCaseIsFound.Setup(enforceCaseIsFound => enforceCaseIsFound.IsFound(It.IsAny<string>()))
                                    .ReturnsAsync(caseIsFound);

            var enforcePartiesAreAssignedToCase = new Mock<IGetCasePartiesNumbers>();
            enforcePartiesAreAssignedToCase.Setup(enforcePartiesAreAssignedToCase => enforcePartiesAreAssignedToCase.GetAsync(It.IsAny<string>()))
                                    .ReturnsAsync(new Dictionary<string, PartyRoleEnum> { { "56544", PartyRoleEnum.OriginalCreditor }, { "632548", PartyRoleEnum.OriginalDebtor } });

            return new CreateClaimParam
            {
                ClaimNumber = "TestClaim1",
                CaseNumber = "TestCaseNo1",
                ClaimDate = new DateTime(),
                DebtTypeId = DebtTypeEnum.Alimony,
                ComplaintPartyNumber = "56544",
                EnforceCaseIsFound = enforceCaseIsFound.Object,
                EnforcePartiesAreAssignedToCase = enforcePartiesAreAssignedToCase.Object,
                PromissoryNumber = "TestPromisoryNO1",
                IsJoint = true,
                BasicAmount = Money.Create("SAR", 1000),
                RemainingAmount = Money.Create("SAR", 1000),
                RequiredAmount = Money.Create("SAR", 1000),
                ShouldRegisterDomainEvent = ShouldRegisterDomainEvent,
                ClaimDetailsList = new List<ClaimDetails> {
                ClaimDetails.Create("632548",Money.Create("SAR", 1000),Money.Create("SAR", 1000)),
                },
            };
        }

        private ClaimExtra CreateClaimExtra(decimal amount = 100, FinancialEffectTypeEnum financialEffectTypeEnum = FinancialEffectTypeEnum.NewspaperAdvertisement)
        {
            return ClaimExtra.Create(Money.Create(CurrencyIso: "SAR", Value: amount), financialEffectTypeEnum);
        }
        #endregion
    }
}
