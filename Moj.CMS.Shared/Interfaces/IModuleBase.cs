using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using System.Threading.Tasks;

namespace Moj.CMS.Shared.Interfaces
{
    public interface IModuleBase
    {
        Task<TResult> ExecuteCommandAsync<TResult>(Command<TResult> command) where TResult : IResult;

        Task ExecuteCommandAsync(Command command);

        Task<TResult> ExecuteQueryAsync<TResult>(Query<TResult> query) where TResult : IResult;
    }
}
