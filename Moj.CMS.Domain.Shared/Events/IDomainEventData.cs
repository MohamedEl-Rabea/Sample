using System;
using MediatR;

namespace Moj.CMS.Domain.Shared.Events
{
    public interface IDomainEvent : INotification
    {
        DateTime EventTime { get; set; }

        object EventSource { get; set; }
    }
}
