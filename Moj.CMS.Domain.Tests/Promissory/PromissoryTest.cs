
using Moj.CMS.Domain.Aggregates.Case.BusinessRules;
using Moj.CMS.Domain.DomainServices;
using Moj.CMS.Domain.ParameterObjects.Promissory;
using Moj.CMS.Domain.Shared.Enums;
using Moq;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Moj.CMS.Domain.Tests.Promissory
{
    public class PromissoryTest : UnitTestBase
    {
        public PromissoryTest()
        {

        }
        [Fact]
        public async Task Should_Create_Promissory_Successfully()
        {
            //Arrange
            var param = GetPromissoryCreationParameter();

            //Act
            var promissoryAgg = await Aggregates.Promissory.Promissory.CreateAsync(param);

            //Assert
            promissoryAgg.Number.ShouldBe(param.Number);
            promissoryAgg.TypeId.ShouldBe(param.PromissoryTypeId);
            promissoryAgg.PromissoryIssueDate.ShouldBe(param.PromissoryIssueDate);
        }


        [Fact]
        public async Task Should_Statisfy_Business_Rule_No_Duplicate_Promissory_Number_Allowed()
        {
            //Arrange
            var param = GetPromissoryCreationParameter(promissoryNumberIsUnique: false);

            //Act
            Func<Task> createPromissory = async () =>
            {
                await Aggregates.Promissory.Promissory.CreateAsync(param);
            };

            //Assert
            await AssertBrokenRuleAsync<NoDuplicatePromissoryNumberAllowedBusniessRule>(createPromissory());

        }

        private AddNewPromissoryParameter GetPromissoryCreationParameter(bool promissoryNumberIsUnique = true)
        {
            var enforcePromissoryNumberIsUniqueMock = new Mock<IEnforcePromissoryNumberIsUnique>();
            enforcePromissoryNumberIsUniqueMock.Setup(enforcePromissoryNumber => enforcePromissoryNumber.IsUniqueAsync(It.IsAny<int>(), It.IsAny<string>()))
                                         .ReturnsAsync(promissoryNumberIsUnique);


            return new AddNewPromissoryParameter
            {
                Number = "PromissoryNumber1",
                PromissoryTypeId = PromissoryTypeEnum.Contract,
                PromissoryIssueDate = DateTime.UtcNow,
                EnforcePromissoryNumberIsUnique = enforcePromissoryNumberIsUniqueMock.Object
            };

        }
    }
}
