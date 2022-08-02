using MediatR;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Promissory.Queries
{
    public class GetAllPromissoriesQuery : PagedQuery<GetAllPromissoriesDto>
    {
        
    }
    public class GetAllPromissoriesQueryHandler : IRequestHandler<GetAllPromissoriesQuery, PagedResult<GetAllPromissoriesDto>>
    {
        private readonly IPromissoryQueries _PromissoryQueries;

        public GetAllPromissoriesQueryHandler(IPromissoryQueries PromissoryQueries)
        {
            _PromissoryQueries = PromissoryQueries;
        }

        public async Task<PagedResult<GetAllPromissoriesDto>> Handle(GetAllPromissoriesQuery query, CancellationToken cancellationToken)
        {
            var result = await _PromissoryQueries.GetAllAsync(query.PagedRequest);
            return result;
        }
    }
}
