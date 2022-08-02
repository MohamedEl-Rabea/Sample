using Moj.CMS.Domain.Aggregates.Party;
using Moj.CMS.Domain.Aggregates.Party.BusinessRules;
using Moj.CMS.Domain.DomainServices.Party;
using Moj.CMS.Domain.ParameterObjects.Party;
using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Domain.Shared.Values;
using Moq;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Moj.CMS.Domain.Tests.Party
{
    public class PartyTests : UnitTestBase
    {
        public PartyTests()
        {

        }

        [Fact]
        public async Task Should_Create_Party_Successfully()
        {
            //Arrange
            var param = GetPartyCreationParameter();

            //Act
            var partyAgg = await Aggregates.Party.Party.CreateAsync(param);

            //Assert
            partyAgg.ShouldNotBeNull();
            partyAgg.FullName.ShouldBe(param.FullName);
            partyAgg.Gender.ShouldBe(param.Gender);
            partyAgg.NationalityCode.ShouldBe(param.NationalityCode);
            partyAgg.PartyLocationId.ShouldBe(param.PartyLocationId);
            partyAgg.PartyNumber.ShouldBe(param.PartyNumber);
            partyAgg.PartyTypeId.ShouldBe(param.PartyTypeId);
            partyAgg.DateOfBirth.ShouldBe(param.DateOfBirth);
            partyAgg.CurrentIdentityNumber.ShouldBe(param.PartyIdentity.PartyIdentityNumber);
        }

        [Fact]
        public async Task Should_Statisfy_Business_Rule_No_Duplicate_Party_Number_Allowed()
        {
            //Arrange
            var param = GetPartyCreationParameter(partyNumberIsUnique: false);

            //Act
            Func<Task> createParty = async () =>
            {
                await Aggregates.Party.Party.CreateAsync(param);
            };

            //Assert
            await AssertBrokenRuleAsync<PartyNumberShouldNotBeDuplicatedBusinessRule>(createParty());

        }

        private PartyInfoParam GetPartyCreationParameter(bool partyNumberIsUnique = true)
        {
            var enforcePartyNumberIsUniqueMock = new Mock<IEnforcePartyNumberIsUnique>();
            enforcePartyNumberIsUniqueMock.Setup(enforcePartyNumber => enforcePartyNumber.IsUniqueAsync(It.IsAny<string>(), It.IsAny<int>()))
                                         .ReturnsAsync(partyNumberIsUnique);


            return new PartyInfoParam
            {
                PartyIdentity = PartyIdentity.NewPartyIdentity(partyIdentityNumber: "1", PartyIdentityTypeEnum.GulfNational),
                FullName = "FullNameTest",
                NationalityCode = "Nationality",
                PartyNumber = "3",
                DateOfBirth = CLock.Now,
                Gender = Gender.Male,
                PartyLocationId = PartyLocationEnum.InsideSaudi,
                PartyTypeId = PartyTypeEnum.Individual,
                EnforcePartyNumberIsUnique = enforcePartyNumberIsUniqueMock.Object

            };

        }
    }

}
