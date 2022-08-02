using Moj.CMS.Domain.Aggregates.Case.BusinessRules;
using Moj.CMS.Domain.ParameterObjects.Promissory;
using Moj.CMS.Domain.Shared.Audit;
using Moj.CMS.Domain.Shared.Entities;
using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Domain.Shared.Guard;
using System;
using System.Threading.Tasks;

namespace Moj.CMS.Domain.Aggregates.Promissory
{
    public class Promissory : FullAuditedAggregateRoot, IAggregateRoot
    {
        private Promissory()
        {

        }
        public string Number { get; private set; }
        public PromissoryTypeEnum TypeId { get; private set; }
        public DateTime PromissoryIssueDate { get; private set; }

        public static async Task<Promissory> CreateAsync(AddNewPromissoryParameter input)
        {
            var newPromissory = new Promissory();
            await newPromissory.SetBasicInfoAsync(input);
            newPromissory.PromissoryIssueDate = input.PromissoryIssueDate;
            return newPromissory;
        }

        public async Task UpdatePromissoryInfoAsync(UpdatePromissoryParameter input)
        {
            await SetBasicInfoAsync(input);
        }

        private async Task SetBasicInfoAsync(PromissoryParameterBase input)
        {
            //Data validations
            Guard.AssertArgumentNotIncludeWhitespaces(input.Number, nameof(input.Number));

            //Busniess rules validations
            await CheckRuleAsync(new NoDuplicatePromissoryNumberAllowedBusniessRule(input.EnforcePromissoryNumberIsUnique, input.Number, Id));

            Number = input.Number;
            TypeId = input.PromissoryTypeId;
        }

        public static Promissory Empty()
        {
            return new Promissory();
        }
    }
}