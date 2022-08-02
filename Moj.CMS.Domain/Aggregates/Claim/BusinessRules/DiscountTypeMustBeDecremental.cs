using Moj.CMS.Domain.Lookups;
using Moj.CMS.Domain.Shared.Entities;
using Moj.CMS.Domain.Shared.Enums;

namespace Moj.CMS.Domain.Aggregates.Claim.BusinessRules
{
    public class DiscountTypeMustBeDecremental : IBusinessRule
    {
        private readonly FinancialEffectTypeEnum _extraType;

        public DiscountTypeMustBeDecremental(FinancialEffectTypeEnum extraType)
        {
            _extraType = extraType;
        }

        public string Message => $"This discount Type={_extraType} is not decremental type";
        public bool IsBroken()
        {
            var isIncremntal = FinancialEffectTypeLookUp.Find(_extraType).IsIncremental;
            return isIncremntal;
        }
    }
}
