using Moj.CMS.Domain.Aggregates.Case.ValueObjects;
using Moj.CMS.Domain.DomainEvents;
using System.Collections.Generic;

namespace Moj.CMS.Domain.Aggregates.Case.Events
{
    public class CasePartyCreatedDomainEvent : CaseChangedDomainEventBase
    {
        public CasePartyCreatedDomainEvent()
        {
            Operation = Shared.Enums.CaseOperationEnum.AddParty;
        }

        public IEnumerable<CaseParty> CaseParties { get; set; }
    }
}
