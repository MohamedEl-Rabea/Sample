using Moj.CMS.Domain.Lookups;
using Moj.CMS.Domain.Shared.Entities;
using Moj.CMS.Domain.Shared.Enums;

namespace Moj.CMS.Domain.Aggregates.Claim.BusinessRules
{
    public class ExtraTypeMustBeIncremental : IBusinessRule
    {
        private readonly FinancialEffectTypeEnum _extraType;

        public ExtraTypeMustBeIncremental(FinancialEffectTypeEnum extraType)
        {
            _extraType = extraType;
        }

        public string Message => $"This extra Type={_extraType} is not incremental type";
        public bool IsBroken()
        {
            var isIncremntal = FinancialEffectTypeLookUp.Find(_extraType).IsIncremental;
            return !isIncremntal;
        }
    }
}
