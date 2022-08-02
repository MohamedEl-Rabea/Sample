using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moj.CMS.Domain.Shared.Entities;
using Moj.CMS.Domain.Shared.Repositories;

namespace Moj.CMS.Shared.Infrastructure.Repositories
{
    public class BaseDomainRepository<TAggregate> : BaseDomainRepository<TAggregate, int>,
        IDomainRepository<TAggregate> where TAggregate : class, IAggregateRoot<int>
    {
        public BaseDomainRepository(IRepository<TAggregate, int> genericRepository) : base(genericRepository)
        {
        }
    }

    public class BaseDomainRepository<TAggregate, TPrimaryKey> :
        IDomainRepository<TAggregate, TPrimaryKey> where TAggregate : class, IAggregateRoot<TPrimaryKey>
    {
        private readonly IRepository<TAggregate, TPrimaryKey> _genericRepository;

        public BaseDomainRepository(IRepository<TAggregate, TPrimaryKey> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public virtual async Task<TAggregate> AddAsync(TAggregate aggregate)
        {
            return await _genericRepository.InsertAsync(aggregate);
        }

        public virtual async Task AddRangeAsync(IEnumerable<TAggregate> aggregates)
        {
            await _genericRepository.InsertRangeAsync(aggregates);
        }
        public virtual async Task UpdateAsync(TAggregate aggregate)
        {
            await _genericRepository.UpdateAsync(aggregate);
        }

        public virtual async Task DeleteAsync(TAggregate aggregate)
        {
            await _genericRepository.DeleteAsync(aggregate);
        }

        public virtual async Task DeleteAsync(TPrimaryKey aggregateKey)
        {
            var aggregate = await GetAsync(aggregateKey);
            await DeleteAsync(aggregate);
        }

        public virtual async Task<TAggregate> GetAsync(TPrimaryKey aggregateKey)
        {
            return await _genericRepository.GetAsync(aggregateKey);
        }

        public virtual async Task<TAggregate> GetAsyncOrDefault(TPrimaryKey aggregateKey)
        {
            return await _genericRepository.FirstOrDefaultAsync(entity => entity.Id.Equals(aggregateKey));
        }

        public virtual async Task<IEnumerable<TAggregate>> GetAllAsync(Expression<Func<TAggregate, bool>> predicate)
        {
            return await _genericRepository.GetAll().Where(predicate).ToListAsync();
        }

        public virtual async Task<IEnumerable<TAggregate>> GetAllIncludingAsync(Expression<Func<TAggregate, bool>> predicate, Expression<Func<TAggregate, object>> includingPredicate)
        {
            return await _genericRepository.GetAllIncluding().Where(predicate).Include(includingPredicate).ToListAsync();
        }

        public async Task<TAggregate> GetOrDefaultAsync(Expression<Func<TAggregate, bool>> predicate)
        {
            return await _genericRepository.FirstOrDefaultAsync(predicate);

        }
    }
}
