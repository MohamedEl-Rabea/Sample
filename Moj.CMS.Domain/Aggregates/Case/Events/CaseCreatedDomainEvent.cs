using Moj.CMS.Domain.Aggregates.Case.ValueObjects;
using Moj.CMS.Domain.DomainEvents;
using Moj.CMS.Domain.Shared.Enums;
using System.Collections.Generic;

namespace Moj.CMS.Domain.Aggregates.Case.Events
{
    public class CaseCreatedDomainEvent : CaseChangedDomainEventBase
    {
        public IEnumerable<CaseParty> CaseParties { get; set; }
        public IEnumerable<CasePromissory> CasePromissories { get; set; }

        public CaseCreatedDomainEvent()
        {
            Operation = CaseOperationEnum.CreateCase;
        }
    }
}
