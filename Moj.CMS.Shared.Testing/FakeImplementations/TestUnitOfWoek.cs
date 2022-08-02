using Microsoft.EntityFrameworkCore.Storage;
using Moj.CMS.Infrastructure.Contexts;
using Moj.CMS.Shared.Interfaces;
using System.Threading.Tasks;

namespace Moj.CMS.Shared.Testing.FakeImplementations
{
    public class TestUnitOfWoek : IUnitOfwork
    {
        private readonly CMSDbContext _dbContext;
        private readonly IDomainEventsDispatcher _domainEventsDispatcher;
        private IDbContextTransaction _currentTransaction;

        public TestUnitOfWoek(CMSDbContext dbContext, IDomainEventsDispatcher domainEventsDispatcher)
        {
            _dbContext = dbContext;
            _domainEventsDispatcher = domainEventsDispatcher;
        }

        public async Task BeginTransactionAsync()
        {
            _currentTransaction ??= await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _domainEventsDispatcher.DispatchAsync(_dbContext);
            await _dbContext.SaveChangesAsync();
            await _currentTransaction.CommitAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _currentTransaction.RollbackAsync();
        }

        public async Task SaveCurrentChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            if (_currentTransaction != null)
            {
                Task.Run(async () =>
                {
                    await _currentTransaction.DisposeAsync();
                    _currentTransaction = null;
                }).GetAwaiter().GetResult();
            }
        }
    }
}
