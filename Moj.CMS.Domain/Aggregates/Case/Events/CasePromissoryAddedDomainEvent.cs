using Moj.CMS.Domain.Aggregates.Case.ValueObjects;
using Moj.CMS.Domain.DomainEvents;
using Moj.CMS.Domain.Shared.Enums;
using System.Collections.Generic;

namespace Moj.CMS.Domain.Aggregates.Case.Events
{
    public class CasePromissoryAddedDomainEvent : CaseChangedDomainEventBase
    {
        public IEnumerable<CasePromissory> CasePromissories  { get; set; }
        public CasePromissoryAddedDomainEvent()
        {
            Operation = CaseOperationEnum.AddPromissory;
        }
    }
    
}
