using MediatR;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Case.Queries.GetAllCases
{


    public class GetAllCasesQuery : PagedQuery<CaseListItemDto>
    {

    }

    public class GetAllCasesQueryHandler : IRequestHandler<GetAllCasesQuery, PagedResult<CaseListItemDto>>
    {
        private readonly ICaseQueries _caseQueries;

        public GetAllCasesQueryHandler(ICaseQueries caseQueries)
        {
            _caseQueries = caseQueries;
        }

        public async Task<PagedResult<CaseListItemDto>> Handle(GetAllCasesQuery query, CancellationToken cancellationToken)
        {
            var result = await _caseQueries.GetAllAsync(query.PagedRequest);
            return result;
        }
    }
}
