using Moj.CMS.Application.AppServices.VIban.Queries.GetAllVIbans;
using Moj.CMS.Shared.Requests;
using Moj.CMS.Shared.Wrapper;
using System.Threading.Tasks;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Application.Interfaces.Queries
{
    [ScopedService]
    public interface IVIbanQueries
    {
        Task<PagedResult<VIbanDto>> GetAllAsync(PagedRequest<VIbanDto> request);
    }
}
