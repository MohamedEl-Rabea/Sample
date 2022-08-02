using MediatR;
using Microsoft.EntityFrameworkCore;
using Moj.CMS.Infrastructure.Contexts;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Infrastructure.Behaviors
{
    public class UnitOfWorkBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
            where TRequest : Command<TResponse>
    {
        private readonly CMSDbContext _dbContext;
        private readonly IUnitOfwork _unitOfwork;

        public UnitOfWorkBehaviour(CMSDbContext dbContext, IUnitOfwork unitOfwork)
        {
            _dbContext = dbContext;
            _unitOfwork = unitOfwork;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            TResponse response = default;
            try
            {
                var strategy = _dbContext.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    await _unitOfwork.BeginTransactionAsync();
                    
                    response = await next();
                    
                    await _unitOfwork.CommitTransactionAsync();
                });

                return response;
            }
            catch (Exception ex)
            {
                await _unitOfwork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
