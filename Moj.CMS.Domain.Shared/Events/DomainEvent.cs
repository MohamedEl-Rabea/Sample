using Moj.CMS.Domain.Shared.Values;
using System;

namespace Moj.CMS.Domain.Shared.Events
{
    /// <summary>
    ///A Domain Event is an event that is spawned from the aggreagte root that is a result of a decision within the domain.
    ///</summary>
    public abstract class DomainEvent : IDomainEvent
    {
        public DateTime EventTime { get; set; }

        public object EventSource { get; set; }

        protected DomainEvent()
        {
            EventTime = CLock.Now;
        }
    }
}
