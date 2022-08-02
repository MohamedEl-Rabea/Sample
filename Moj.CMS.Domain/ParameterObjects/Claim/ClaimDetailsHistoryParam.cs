using Moj.CMS.Domain.Shared.Values;

namespace Moj.CMS.Domain.ParameterObjects.Claim
{
    public class ClaimDetailsHistoryParam
    {
        public string PartyNumber { get; set; }
        public Money NewRequiredAmount { get; set; }
        public Money NewBillingAmount { get; set; }
        public Money OldRequiredAmount { get; set; }
        public Money OldBillingAmount { get; set; }
    }
}
