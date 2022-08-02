using Moj.CMS.Domain.DomainEvents;
using Moj.CMS.Domain.Shared.Enums;

namespace Moj.CMS.Domain.Aggregates.Case.Events
{
    public class CaseActivatedDomainEvent : CaseChangedDomainEventBase
    {
        public CaseStatusEnum OldStatus  { get; set; }

        public CaseActivatedDomainEvent()
        {
            Operation = CaseOperationEnum.ActivateCase;
        }
    }
}
