using Moj.CMS.Domain.Aggregates.Case.BusinessRules;
using Moj.CMS.Domain.Aggregates.Case.Events;
using Moj.CMS.Domain.Aggregates.Case.ValueObjects;
using Moj.CMS.Domain.DomainServices;
using Moj.CMS.Domain.ParameterObjects.Case;
using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Domain.Shared.Values;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Moj.CMS.Domain.Tests.Case
{
    public class CaseTests : UnitTestBase
    {
        const string CourtCode = "1";
        const string DivisionCode = "1";
        const string JudgeCode = "1";

        public CaseTests()
        {

        }

        [Fact]
        public async Task Should_Create_Case_Successfully()
        {
            //Arrange
            var param = GetCaseCreationParameter();

            //Act
            var caseAgg = await Aggregates.Case.Case.CreateAsync(param);

            //Assert
            caseAgg.CaseNumber.ShouldBe(param.CaseNumber);
            caseAgg.CaseStatus.ShouldBe(param.CaseStatusId);
            caseAgg.CaseType.ShouldBe(param.CaseTypeId);
            caseAgg.CaseBasicAmount.ValueEquals(param.CaseBasicAmount).ShouldBe(true);
            caseAgg.ApprovedAmount.ValueEquals(param.RequiredAmount).ShouldBe(true);
            caseAgg.CaseDetails.ShouldNotBeNull();
            caseAgg.CaseDetails.FirstOrDefault().ShouldBe(param.CaseDetailsParam.CaseDetails);
            caseAgg.CaseParties.SequenceEqual(param.CaseParties);
            caseAgg.CasePromissories.SequenceEqual(param.CasePromissories);
            caseAgg.DatesInfo.ShouldBe(param.DatesInfo);
        }

        [Fact]
        public async Task Should_Close_Case_Successfully()
        {
            //Arrange
            var param = GetCaseCreationParameter();

            //Act
            var caseAgg = await Aggregates.Case.Case.CreateAsync(param);
            caseAgg.Close();

            //Assert
            caseAgg.CaseStatus.ShouldBe(CaseStatusEnum.Closed);
            caseAgg.CloseDate.ShouldNotBeNull();
        }

        [Fact]
        public async Task Should_Register_Domain_Event_Case_Created_Upon_Create_Case()
        {
            //Arrange
            var param = GetCaseCreationParameter();

            //Act
            var caseAgg = await Aggregates.Case.Case.CreateAsync(param);

            //Assert
            var registeredDomainEvent = AssertRegisteredSingleDomainEvent<CaseCreatedDomainEvent>(caseAgg);
            registeredDomainEvent.CaseNumber.ShouldBe(param.CaseNumber);
            registeredDomainEvent.Operation.ShouldBe(CaseOperationEnum.CreateCase);
            registeredDomainEvent.CaseParties.SequenceEqual(param.CaseParties);
            registeredDomainEvent.CasePromissories.SequenceEqual(param.CasePromissories);
        }

        [Fact]
        public async Task Should_Not_Register_Domain_Event_Case_Activated_Upon_Activate_Non_Closed_Case()
        {
            //Arrange
            var caseAgg = await CreateCaseAsync(isClosedCase: false);

            //Act
            caseAgg.Activate();

            //Assert
            AssertNotRegisteredDomainEvent<CaseActivatedDomainEvent>(caseAgg);
        }

        [Fact]
        public async Task Should_Register_Domain_Event_Case_Activated_Upon_Activate_Case()
        {
            //Arrange
            var caseAgg = await CreateCaseAsync(isClosedCase: true);

            //Act
            caseAgg.Activate();

            //Assert
            var registeredDomainEvent = AssertRegisteredSingleDomainEvent<CaseActivatedDomainEvent>(caseAgg);
            registeredDomainEvent.CaseNumber.ShouldBe(caseAgg.CaseNumber);
            registeredDomainEvent.Operation.ShouldBe(CaseOperationEnum.ActivateCase);
            registeredDomainEvent.OldStatus.ShouldBe(CaseStatusEnum.Closed);
        }

        [Fact]
        public async Task Should_Update_Case_Court_Details_Succeffully()
        {
            //Arrange
            string newCourt = "2", newDivision = "3", newJudge = "2";
            var caseAgg = await CreateCaseAsync();
            var updateCaseCourtDetailsParam = CreateCaseCourtDetailsChangeParam(newCourt, newDivision, newJudge);
            var oldCourtDetails = caseAgg.CaseDetails.First();

            //Act
            await caseAgg.UpdateCourtDetailsAsync(updateCaseCourtDetailsParam);

            //Assert
            var registeredDomainEvent = AssertRegisteredSingleDomainEvent<CaseCourtDetailsChangedDomainEvent>(caseAgg);
            registeredDomainEvent.CaseNumber.ShouldBe(caseAgg.CaseNumber);
            registeredDomainEvent.Operation.ShouldBe(CaseOperationEnum.ChangeCourtDetails);

            var changedCourtDetails = caseAgg.CaseDetails.SingleOrDefault(c => !c.IsCurrent);
            changedCourtDetails.ShouldNotBeNull();
            changedCourtDetails.ValueEquals(oldCourtDetails).ShouldBeTrue();

            var newCourtDetails = caseAgg.CaseDetails.SingleOrDefault(c => c.IsCurrent);
            newCourtDetails.ShouldNotBeNull();
            newCourtDetails.ValueEquals(updateCaseCourtDetailsParam.CaseDetails);
        }

        [Fact]
        public async Task Should_Not_Update_Case_Court_Details_InCase_Of_Equals_Current()
        {
            //Arrange
            var caseAgg = await CreateCaseAsync();
            var currentCourtDetails = caseAgg.CaseDetails.Single(d => d.IsCurrent);
            var updateCaseCourtDetailsParam = CreateCaseCourtDetailsChangeParam(currentCourtDetails.CourtCode
                , currentCourtDetails.DivisionCode
                , currentCourtDetails.JudgeCode);

            //Act
            await caseAgg.UpdateCourtDetailsAsync(updateCaseCourtDetailsParam);

            //Assert
            AssertNotRegisteredDomainEvent<CaseCourtDetailsChangedDomainEvent>(caseAgg);
            var courtDetails = caseAgg.CaseDetails.SingleOrDefault(c => c.IsCurrent);
            courtDetails.ShouldNotBeNull();
            courtDetails.ValueEquals(updateCaseCourtDetailsParam.CaseDetails);
        }

        [Fact]
        public async Task Should_Statisfy_Business_Rule_No_Duplicate_Case_Number_Allowed()
        {
            //Arrange
            var param = GetCaseCreationParameter(caseNumberIsUnique: false);

            //Act
            Func<Task> createCase = async () =>
            {
                await Aggregates.Case.Case.CreateAsync(param);
            };

            //Assert
            await AssertBrokenRuleAsync<NoDuplicateCaseNumberAllowedBusniessRule>(createCase());
        }

        [Fact]
        public async Task Should_Satisfy_Business_Rule_Judge_Must_Be_Exists()
        {
            //Arrange
            var param = GetCaseCreationParameter(judgeExists: false);

            //Act
            Func<Task> createCase = async () =>
            {
                await Aggregates.Case.Case.CreateAsync(param);
            };

            //Assert
            await AssertBrokenRuleAsync<JudgeMustBeExistsBusniessRule>(createCase());
        }

        [Fact]
        public async Task Should_Trigger_Business_Rule_Court_Details_Must_Be_Valid_When_Court_Not_Exists()
        {
            //Arrange
            var param = GetCaseCreationParameter(courtExists: false);

            //Act
            Func<Task> createCase = async () =>
            {
                await Aggregates.Case.Case.CreateAsync(param);
            };

            //Assert
            await AssertBrokenRuleAsync<CourtDetailsMustBeValidBusniessRule>(createCase());
        }

        [Fact]
        public async Task Should_Trigger_Business_Rule_Court_Details_Must_Be_Valid_When_Division_Not_Exists()
        {
            //Arrange
            var param = GetCaseCreationParameter(divisionCourtCode: null);

            //Act
            Func<Task> createCase = async () =>
            {
                await Aggregates.Case.Case.CreateAsync(param);
            };

            //Assert
            await AssertBrokenRuleAsync<CourtDetailsMustBeValidBusniessRule>(createCase());
        }

        [Fact]
        public async Task Should_Trigger_Business_Rule_Court_Details_Must_Be_Valid_When_Division_Court_Not_Match_With_Case_Court()
        {
            //Arrange
            var notMatchCourtCode = CourtCode + "2";
            var param = GetCaseCreationParameter(divisionCourtCode: notMatchCourtCode);

            //Act
            Func<Task> createCase = async () =>
            {
                await Aggregates.Case.Case.CreateAsync(param);
            };

            //Assert
            await AssertBrokenRuleAsync<CourtDetailsMustBeValidBusniessRule>(createCase());
        }


        private async Task<Aggregates.Case.Case> CreateCaseAsync(bool isClosedCase = false)
        {
            var param = GetCaseCreationParameter();
            var caseAgg = await Aggregates.Case.Case.CreateAsync(param);
            if (isClosedCase)
                caseAgg.Close();

            return caseAgg;
        }

        private AddNewCaseParameter GetCaseCreationParameter(bool caseNumberIsUnique = true, bool judgeExists = true, bool courtExists = true, string divisionCourtCode = CourtCode)
        {
            var enforceCaseNumberIsUniqueMock = new Mock<IEnforceCaseNumberIsUnique>();
            enforceCaseNumberIsUniqueMock.Setup(enforceCaseNumber => enforceCaseNumber.IsUniqueAsync(It.IsAny<int>(), It.IsAny<string>()))
                                         .ReturnsAsync(caseNumberIsUnique);

            var enforceCourtIsExistsMock = new Mock<IEnforceCourtIsExists>();
            enforceCourtIsExistsMock.Setup(enforceCourtExists => enforceCourtExists.IsExistAsync(It.IsAny<string>()))
                                    .ReturnsAsync(courtExists);

            var enforceJudgeIsExistsMock = new Mock<IEnforceJudgeIsExists>();
            enforceJudgeIsExistsMock.Setup(enforceJudgeExists => enforceJudgeExists.IsExistAsync(It.IsAny<string>()))
                                    .ReturnsAsync(judgeExists);


            var getDivisionCourtCodeMock = new Mock<IGetDivisionCourtCode>();
            getDivisionCourtCodeMock.Setup(getDivisionCourtCode => getDivisionCourtCode.GetDivisionCourtCodeAsync(It.IsAny<string>()))
                                    .ReturnsAsync(divisionCourtCode);

            return new AddNewCaseParameter
            {
                CaseNumber = "TestCase1",
                CaseStatusId = CaseStatusEnum.Active,
                CaseTypeId = CaseTypeEnum.Financial,
                DatesInfo = CaseDate.Create(DateTime.Now, DateTime.Now),
                RequiredAmount = Money.Create("SAR", 1000),
                CaseBasicAmount = Money.Create("SAR", 2000),
                CaseDetailsParam = new CaseDetailsParam
                {
                    CaseDetails = CaseDetails.Create(DivisionCode, CourtCode, JudgeCode),
                    EnforceCourtIsExists = enforceCourtIsExistsMock.Object,
                    EnforceJudgeIsExists = enforceJudgeIsExistsMock.Object,
                    GetDivisionCourtCode = getDivisionCourtCodeMock.Object
                },
                CaseParties = new List<CaseParty>
                {
                    CaseParty.Create(
                        new CasePartyCreationParam
                        {
                            PartyNumber = "1",
                            PromissoryNumber = "1",
                            PartyRoleTypeId = PartyRoleEnum.OriginalCreditor,
                            IsApplicant = false,
                            PartyClassificationId = PartyClassificationEnum.Requester
                        }),
                    CaseParty.Create(
                        new CasePartyCreationParam
                        {
                            PartyNumber = "2",
                            PromissoryNumber = "1",
                            PartyRoleTypeId = PartyRoleEnum.OriginalDebtor,
                            IsApplicant = false,
                            PartyClassificationId = PartyClassificationEnum.Respondent
                        })
                },
                CasePromissories = new List<CasePromissory> { CasePromissory.Create("TestPromissoryNumber1") },
                EnforceCaseNumberIsUnique = enforceCaseNumberIsUniqueMock.Object
            };
        }

        private CaseDetailsParam CreateCaseCourtDetailsChangeParam(string newCourt, string newDivision, string newJudge)
        {
            var param = GetCaseCreationParameter();
            var enforceCourtIsExistsMockObject = param.CaseDetailsParam.EnforceCourtIsExists;
            var enforceJudgeIsExistsMockObject = param.CaseDetailsParam.EnforceJudgeIsExists;
            var getDivisionCourtCodeMock = new Mock<IGetDivisionCourtCode>();
            getDivisionCourtCodeMock.Setup(getDivisionCourtCode => getDivisionCourtCode.GetDivisionCourtCodeAsync(It.IsAny<string>()))
                                    .ReturnsAsync(newCourt);

            var updateCaseCourtDetailsParam = new CaseDetailsParam
            {
                CaseDetails = CaseDetails.Create(newDivision, newCourt, newJudge),
                EnforceCourtIsExists = enforceCourtIsExistsMockObject,
                EnforceJudgeIsExists = enforceJudgeIsExistsMockObject,
                GetDivisionCourtCode = getDivisionCourtCodeMock.Object
            };

            return updateCaseCourtDetailsParam;
        }

    }
}
