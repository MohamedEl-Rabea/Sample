using MediatR;
using Microsoft.EntityFrameworkCore;
using Moj.CMS.Domain.Shared.Entities;
using Moj.CMS.Shared.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Moj.CMS.Shared.Infrastructure
{
    public class DomainEventsDispatcher : IDomainEventsDispatcher
    {
        private readonly IMediator _mediator;

        public DomainEventsDispatcher(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task DispatchAsync(DbContext dbContext)
        {
            var domainEntityEntries = dbContext.ChangeTracker
                .Entries<IGeneratesDomainEvents>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();

            var domainEvents = domainEntityEntries.SelectMany(entry =>
            {
                foreach (var entityDomainEvent in entry.Entity.DomainEvents)
                    entityDomainEvent.EventSource = entry.Entity;

                return entry.Entity.DomainEvents;
            }).ToList();

            domainEntityEntries.ForEach(entry => entry.Entity.DomainEvents.Clear());
            if (domainEvents.Any())
            {
                foreach (var domainEvent in domainEvents)
                {
                    await _mediator.Publish(domainEvent);
                }
                await DispatchAsync(dbContext);
            }
        }
    }

}
