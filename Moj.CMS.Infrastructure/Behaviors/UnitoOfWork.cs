using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using Moj.CMS.Infrastructure.Contexts;
using Moj.CMS.Shared.Interfaces;
using System.Threading.Tasks;

namespace Moj.CMS.Infrastructure.Behaviors
{
    public class UnitoOfWork : IUnitOfwork
    {
        //TODO: Should be refactored to allow multiple dbcontext dynamically
        //https://docs.microsoft.com/en-us/ef/core/saving/transactions#cross-context-transaction

        private readonly CMSDbContext _dbContext;
        private readonly IMediator _mediator;
        private IDbContextTransaction _currentTransaction;
        private readonly IDomainEventsDispatcher _domainEventsDispatcher;

        private bool _savePointCreated;
        private const string MostFirstSavePoint = "MostFirstSavePoint";

        public UnitoOfWork(CMSDbContext dbContext, IMediator mediator, IDomainEventsDispatcher domainEventsDispatcher)
        {
            _dbContext = dbContext;
            _mediator = mediator;
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
            if (_savePointCreated)
                await _currentTransaction.RollbackToSavepointAsync(MostFirstSavePoint);
            else
                await _currentTransaction.RollbackAsync();
        }

        public async Task SaveCurrentChangesAsync()
        {
            if (!_savePointCreated)
            {
                await _currentTransaction.CreateSavepointAsync(MostFirstSavePoint);
                _savePointCreated = true;
            }

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
