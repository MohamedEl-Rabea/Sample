using MediatR;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.VIban.Queries.GetAllVIbans
{
    public class GetAllVIbansQuery : PagedQuery<VIbanDto>
    {

    }

    public class GetAllVIbansQueryHandler : IRequestHandler<GetAllVIbansQuery, PagedResult<VIbanDto>>
    {
        private readonly IVIbanQueries _vIbanQueries;

        public GetAllVIbansQueryHandler(IVIbanQueries vIbanQueries)
        {
            _vIbanQueries = vIbanQueries;
        }

        public async Task<PagedResult<VIbanDto>> Handle(GetAllVIbansQuery query, CancellationToken cancellationToken)
        {
            var result = await _vIbanQueries.GetAllAsync(query.PagedRequest);
            return result;
        }
    }
}
