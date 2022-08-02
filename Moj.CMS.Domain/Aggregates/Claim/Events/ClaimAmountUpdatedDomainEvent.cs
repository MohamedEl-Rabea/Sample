using Moj.CMS.Domain.Shared.Events;
using Moj.CMS.Domain.Shared.Values;

namespace Moj.CMS.Domain.Aggregates.Claim.Events
{
    public class ClaimAmountUpdatedDomainEvent : DomainEvent
    {
        public Money OldTotalRequiredAmount { get; set; }
        public Money OldTotalRemainingAmount { get; set; }
    }
}
