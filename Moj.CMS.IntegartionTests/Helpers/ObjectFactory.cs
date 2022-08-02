using Moj.CMS.Domain.Aggregates.Case.ValueObjects;
using Moj.CMS.Domain.Aggregates.Claim.ValueObjects;
using Moj.CMS.Domain.Aggregates.Party;
using Moj.CMS.Domain.Aggregates.Promissory;
using Moj.CMS.Domain.Aggregates.VIban;
using Moj.CMS.Domain.DomainServices;
using Moj.CMS.Domain.DomainServices.Party;
using Moj.CMS.Domain.ParameterObjects.Case;
using Moj.CMS.Domain.ParameterObjects.Claim;
using Moj.CMS.Domain.ParameterObjects.Party;
using Moj.CMS.Domain.ParameterObjects.Promissory;
using Moj.CMS.Domain.ParameterObjects.VIban;
using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Domain.Shared.Values;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moj.CMS.IntegartionTests.Helpers
{
    public static class ObjectFactory
    {
        public static async Task<Domain.Aggregates.Case.Case> CreateCaseAsync(string caseNumber = "1",
            string promissoryNumber = "1",
            List<CaseParty> caseParties = null,
            CaseStatusEnum caseStatus = CaseStatusEnum.Active,
            CaseTypeEnum caseType = CaseTypeEnum.Financial,
            DateTime? receiveDate = null,
            DateTime? acceptanceDate = null,
            string judgeCode = null,
            string courtCode = null,
            string divisionCode = null,
            Money totalRemainingAmount = null,
            Money totalRequiredAmount = null)
        {
            var enforceCaseNumberIsUniqueMock = new Mock<IEnforceCaseNumberIsUnique>();
            enforceCaseNumberIsUniqueMock.Setup(enforceCaseNumber => enforceCaseNumber.IsUniqueAsync(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            var enforceCourtIsExistsMock = new Mock<IEnforceCourtIsExists>();
            enforceCourtIsExistsMock.Setup(enforceCourtExists => enforceCourtExists.IsExistAsync(It.IsAny<string>()))
                .ReturnsAsync(true);

            var enforceJudgeIsExistsMock = new Mock<IEnforceJudgeIsExists>();
            enforceJudgeIsExistsMock.Setup(enforceJudgeExists => enforceJudgeExists.IsExistAsync(It.IsAny<string>()))
                .ReturnsAsync(true);

            var getDivisionCourtCodeMock = new Mock<IGetDivisionCourtCode>();
            getDivisionCourtCodeMock.Setup(getDivisionCourtCode =>
                    getDivisionCourtCode.GetDivisionCourtCodeAsync(It.IsAny<string>()))
                .ReturnsAsync(courtCode ?? "1");

            var caseParam = new AddNewCaseParameter
            {
                CaseNumber = caseNumber,
                CaseStatusId = caseStatus,
                CaseTypeId = caseType,
                DatesInfo = CaseDate.Create(receiveDate ?? DateTime.Now, acceptanceDate ?? DateTime.Now),
                CaseDetailsParam = new CaseDetailsParam
                {
                    CaseDetails = CaseDetails.Create(divisionCode: divisionCode ?? "1", courtCode: courtCode ?? "1", judgeCode: judgeCode ?? "1"),
                    EnforceCourtIsExists = enforceCourtIsExistsMock.Object,
                    EnforceJudgeIsExists = enforceJudgeIsExistsMock.Object,
                    GetDivisionCourtCode = getDivisionCourtCodeMock.Object
                },
                CasePromissories = new List<CasePromissory> { CasePromissory.Create(promissoryNumber) },
                CaseParties = caseParties ?? new List<CaseParty>
                {
                    CaseParty.Create(new CasePartyCreationParam
                        {
                            PartyNumber = "2",
                            PromissoryNumber = "1",
                            PartyRoleTypeId = PartyRoleEnum.OriginalCreditor,
                            IsApplicant = true,
                            PartyClassificationId = PartyClassificationEnum.Requester
                        }
                    ),
                    CaseParty.Create(new CasePartyCreationParam
                        {
                            PartyNumber = "3",
                            PromissoryNumber = "1",
                            PartyRoleTypeId = PartyRoleEnum.OriginalDebtor,
                            IsApplicant = false,
                            PartyClassificationId = PartyClassificationEnum.Respondent
                        }
                    )
                },
                EnforceCaseNumberIsUnique = enforceCaseNumberIsUniqueMock.Object,
            };

            var caseAgg = await Domain.Aggregates.Case.Case.CreateAsync(caseParam);
            caseAgg.UpdateAmounts(totalRequiredAmount ?? Money.Empty, totalRemainingAmount ?? Money.Empty);
            return caseAgg;
        }

        public static async Task<Promissory> CreatePromissoryAsync(string number = "TestPromissoryNumber1",
            PromissoryTypeEnum typeId = PromissoryTypeEnum.Contract,
            DateTime? issueDate = null)
        {
            var enforcePromissoryNumberIsUniqueMock = new Mock<IEnforcePromissoryNumberIsUnique>();
            enforcePromissoryNumberIsUniqueMock.Setup(enforcePromissoryNumber => enforcePromissoryNumber.IsUniqueAsync(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            var promissoryParam = new AddNewPromissoryParameter
            {
                Number = number,
                PromissoryTypeId = typeId,
                PromissoryIssueDate = issueDate ?? CLock.Now,
                EnforcePromissoryNumberIsUnique = enforcePromissoryNumberIsUniqueMock.Object
            };

            var promissoryAgg = await Promissory.CreateAsync(promissoryParam);

            return promissoryAgg;
        }

        public static async Task<Party> CreatePartyAsync(string number = "123456", PartyTypeEnum partyType = PartyTypeEnum.Individual, PartyIdentityTypeEnum identityTypeEnum = default, decimal credit = 0, decimal debt = 0)
        {
            var enforcePartyNumberIsUniqueMock = new Mock<IEnforcePartyNumberIsUnique>();
            enforcePartyNumberIsUniqueMock.Setup(enforcePartyNumber => enforcePartyNumber
            .IsUniqueAsync(It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(true);

            var partyParam = new PartyInfoParam
            {
                PartyNumber = number,
                PartyTypeId = partyType,
                NationalityCode = "SA",
                DateOfBirth = null,
                FullName = "test full name",
                Gender = Gender.Male,
                PartyLocationId = PartyLocationEnum.InsideSaudi,
                EnforcePartyNumberIsUnique = enforcePartyNumberIsUniqueMock.Object,
                PartyIdentity = PartyIdentity.NewPartyIdentity(number, identityTypeEnum)
            };

            var partyAgg = await Party.CreateAsync(partyParam);


            partyAgg.AddCredit(credit != default ? Money.Create("SAR", credit) : Money.Empty);
            partyAgg.AddDebt(debt != default ? Money.Create("SAR", debt) : Money.Empty);

            return partyAgg;
        }

        public static async Task<Domain.Aggregates.Claim.Claim> CreateClaimAsync(string caseNumber = "1",
            string complaintPartyNumber = "1",
            List<string> accusedParties = null,
            string claimNumber = "1",
            bool shouldRegisterDomainEvent = true)
        {
            accusedParties = accusedParties ?? new List<string> { "632548" };
            var enforceCaseIsFound = new Mock<IEnforceCaseIsFound>();
            enforceCaseIsFound.Setup(enforceCaseIsFound => enforceCaseIsFound.IsFound(It.IsAny<string>()))
                                    .ReturnsAsync(true);

            var getCasePartiesNumbersMock = new Mock<IGetCasePartiesNumbers>();
            var result = new Dictionary<string, PartyRoleEnum>
            {
                { complaintPartyNumber, PartyRoleEnum.OriginalCreditor }
            };
            accusedParties.ForEach(partyNumber =>
            {
                result.Add(partyNumber, PartyRoleEnum.OriginalDebtor);
            });

            getCasePartiesNumbersMock.Setup(enforcePartiesAreAssignedToCase => enforcePartiesAreAssignedToCase
                                            .GetAsync(It.IsAny<string>()))
                                            .ReturnsAsync(result);

            var creationParam = new CreateClaimParam
            {
                ClaimNumber = claimNumber,
                CaseNumber = caseNumber,
                ClaimDate = CLock.Now,
                ComplaintPartyNumber = complaintPartyNumber,
                EnforceCaseIsFound = enforceCaseIsFound.Object,
                EnforcePartiesAreAssignedToCase = getCasePartiesNumbersMock.Object,
                PromissoryNumber = "TestPromisoryNO1",
                IsJoint = true,
                BasicAmount = Money.Create("SAR", 2000),
                RemainingAmount = Money.Create("SAR", 2000),
                RequiredAmount = Money.Create("SAR", 1000),
                ShouldRegisterDomainEvent = shouldRegisterDomainEvent,
                ClaimDetailsList = accusedParties.Select(partyNumber => ClaimDetails.Create(partyNumber, Money.Create("SAR", 1000), Money.Create("SAR", 1000))),
            };
            return await Domain.Aggregates.Claim.Claim.Create(creationParam);
        }

        public static Task<VIban> CreateVIbanAsync(string alias = "Test Name", string vIbanNumber = "Test Number", string bankName = "Test Bank Name",
            string parentAccountNumber = "Test Parent Number",
            string referenceNumber = "Reference Number",
            VIbanReferenceTypeEnum referenceType = VIbanReferenceTypeEnum.Case,
            decimal CAP = 100000,
            DateTime? issueDate = null)
        {
            var param = new CreateVIbanParam
            {
                Alias = alias,
                CAP = CAP,
                IssueDate = issueDate ?? CLock.Now,
                VIbanNumber = vIbanNumber,
                BankName = bankName,
                ParentAccountNumber = parentAccountNumber,
                ReferenceDetails = VIbanReferenceDetails.Create(referenceNumber, referenceType)
            };

            return Task.FromResult(VIban.Create(param));
        }
    }
}
