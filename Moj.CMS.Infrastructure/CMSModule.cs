using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moj.CMS.Application.Interfaces;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using System.Threading.Tasks;

namespace Moj.CMS.Infrastructure
{
    public class CmsModule : ICmsModule
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public CmsModule(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task<TResult> ExecuteCommandAsync<TResult>(Command<TResult> command) where TResult : IResult
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetService<IMediator>();
                TResult result = await mediator.Send(command);
                result.RequestId = command.RequestId;
                return result;
            }
        }

        public async Task ExecuteCommandAsync(Command command)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetService<IMediator>();
                await mediator.Send(command);
            }
        }

        public async Task<TResult> ExecuteQueryAsync<TResult>(Query<TResult> query) where TResult : IResult
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetService<IMediator>();
                var result = await mediator.Send(query);
                result.RequestId = query.RequestId;
                return result;
            }
        }
    }
}
