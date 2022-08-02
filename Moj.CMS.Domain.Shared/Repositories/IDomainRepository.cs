using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Moj.CMS.Domain.Shared.Entities;

namespace Moj.CMS.Domain.Shared.Repositories
{
    public interface IDomainRepository<TAggregate> : IDomainRepository<TAggregate, int>
        where TAggregate : IAggregateRoot<int>
    {

    }

    public interface IDomainRepository<TAggregate, in TPrimaryKey>  where TAggregate :
        IAggregateRoot<TPrimaryKey>
    {
        Task<TAggregate> GetOrDefaultAsync(Expression<Func<TAggregate, bool>> predicate);
        Task<TAggregate> AddAsync(TAggregate aggregate);
        Task AddRangeAsync(IEnumerable<TAggregate> aggregates);
        Task UpdateAsync(TAggregate aggregate);
        Task DeleteAsync(TAggregate aggregate);
        Task DeleteAsync(TPrimaryKey aggregateKey);
        Task<TAggregate> GetAsync(TPrimaryKey aggregateKey);
        Task<TAggregate> GetAsyncOrDefault(TPrimaryKey aggregateKey);
        Task<IEnumerable<TAggregate>> GetAllAsync(Expression<Func<TAggregate, bool>> predicate);
        Task<IEnumerable<TAggregate>> GetAllIncludingAsync(Expression<Func<TAggregate, bool>> predicate, Expression<Func<TAggregate, object>> includingPredicate);
    }
}
