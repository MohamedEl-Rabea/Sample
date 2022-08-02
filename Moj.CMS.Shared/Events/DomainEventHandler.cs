using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Domain.Shared.Events
{
    public abstract class DomainEventHandler<TDomainEvent> : INotificationHandler<TDomainEvent>
        where TDomainEvent : INotification
    {
        public abstract Task Handle(TDomainEvent notification, CancellationToken cancellationToken);
    }
}
