using Moj.CMS.Domain.DomainEvents;
using Moj.CMS.Domain.Shared.Enums;

namespace Moj.CMS.Domain.Aggregates.Case.Events
{
    public class ClaimClosedDomainEvent : CaseChangedDomainEventBase
    {
        public ClaimClosedDomainEvent()
        {
            Operation = CaseOperationEnum.CloseClaim;
        }
    }
}