using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Domain.Shared.Events;
using Moj.CMS.Domain.Shared.Values;
using System;

namespace Moj.CMS.Domain.DomainEvents
{
    public abstract class CaseChangedDomainEventBase : DomainEvent
    {
        public CaseChangedDomainEventBase()
        {
            OperationDateTime = CLock.Now;
        }

        public string CaseNumber { get; set; }
        public CaseOperationEnum Operation { get; set; }
        public DateTime OperationDateTime { get; set; }
    }
}
