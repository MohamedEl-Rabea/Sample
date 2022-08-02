using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using Moj.CMS.Domain.Shared.Events;

namespace Moj.CMS.Domain.Shared.Entities
{
    public class AggregateRoot : AggregateRoot<int>, IAggregateRoot
    {

    }

    /// <summary>
    /// This class can be used to simplify implementing <see cref="IAggregateRoot"/> for aggregate roots.
    /// Aggregate is a pattern in Domain-Driven Design. 
    /// A DDD aggregate is a cluster of domain objects that can be treated as a single unit.
    /// An example may be an order and its line-items, these will be separate objects,
    /// but it's useful to treat the order (together with its line items) as a single aggregate.
    /// </summary>
    /// <typeparam name="TKey">Type of the primary key of the entity</typeparam>
    public class AggregateRoot<TPrimaryKey> : Entity<TPrimaryKey>, IAggregateRoot<TPrimaryKey>
    {
        [NotMapped]
        public virtual ICollection<IDomainEvent> DomainEvents { get; }

        protected void RegisterDomainEvent(IDomainEvent domainEvent)
        {
            DomainEvents.Add(domainEvent);
        }

        public AggregateRoot()
        {
            DomainEvents = new Collection<IDomainEvent>();
        }
    }
}
