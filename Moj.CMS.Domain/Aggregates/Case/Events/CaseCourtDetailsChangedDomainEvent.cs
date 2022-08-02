using Moj.CMS.Domain.Aggregates.Case.ValueObjects;
using Moj.CMS.Domain.DomainEvents;
using Moj.CMS.Domain.Shared.Enums;

namespace Moj.CMS.Domain.Aggregates.Case.Events
{
    public class CaseCourtDetailsChangedDomainEvent : CaseChangedDomainEventBase
    {
        public CaseDetails PreviousCaseDetails { get; set; }

        public CaseCourtDetailsChangedDomainEvent()
        {
            Operation = CaseOperationEnum.ChangeCourtDetails;
        }
    }
}
