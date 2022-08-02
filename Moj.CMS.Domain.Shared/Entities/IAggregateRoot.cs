using System.Collections.Generic;
using Moj.CMS.Domain.Shared.Events;

namespace Moj.CMS.Domain.Shared.Entities
{
    public interface IAggregateRoot : IAggregateRoot<int>, IEntity
    {

    }

    public interface IAggregateRoot<TPrimaryKey> : IEntity<TPrimaryKey>, IGeneratesDomainEvents
    {

    }

    public interface IGeneratesDomainEvents
    {
        ICollection<IDomainEvent> DomainEvents { get; }
    }
}