using Moj.CMS.Shared.Models;
using Moj.CMS.Shared.Requests;
using Moj.CMS.Shared.Wrapper;
using System.Threading.Tasks;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Shared.Queries
{
    [TransientService]
    public interface ILogsQueries
    {
        Task<PagedResult<Log>> GetAllAsync(PagedRequest<Log> request);
        Task<PagedResult<EntityHistoryDto>> GetEntitiesHistoryAsync(PagedRequest<EntityHistoryDto> request);
        Task<EntityHistoryDto> GetEntityHistoryByRequestId(string requestId);
    }
}
