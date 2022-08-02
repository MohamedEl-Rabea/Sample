using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Domain.Shared.Values;

namespace Moj.CMS.Domain.ParameterObjects.Claim
{
    public class ClaimHistoryParam
    {
        public Money TotalAmountBefore { get; set; }
        public Money TotalAmountAfter { get; set; }
        public FinancialEffectTypeEnum EffectType { get; set; }
    }
}
