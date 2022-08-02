using Moj.CMS.Domain.DomainEvents;
using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Domain.Shared.Values;

namespace Moj.CMS.Domain.Aggregates.Claim.Events
{
    public class ClaimUpdatedDomainEvent : CaseChangedDomainEventBase
    {
        public Money OldTotalRequiredAmount { get; set; }
        public Money OldTotalRemainingAmount { get; set; }
        public ClaimUpdatedDomainEvent()
        {
            Operation = CaseOperationEnum.EditClaim;
        }
    }
}
