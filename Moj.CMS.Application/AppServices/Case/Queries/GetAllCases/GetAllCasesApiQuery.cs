using MediatR;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Case.Queries.GetAllCases
{

    public class GetAllCasesApiQuery : Query<IResult<PagedResult<CaseListItemDto>>>
    {
        public CasesPagedApiRequest Request { get; set; }
    }

    public class GetAllCasesApiQueryHandler : IRequestHandler<GetAllCasesApiQuery, IResult<PagedResult<CaseListItemDto>>>
    {
        private readonly ICaseQueries _caseQueries;
        
        public GetAllCasesApiQueryHandler(ICaseQueries caseQueries)
        {
            _caseQueries = caseQueries;
        }

        public async Task<IResult<PagedResult<CaseListItemDto>>> Handle(GetAllCasesApiQuery query, CancellationToken cancellationToken)
        {
            var result = await _caseQueries.GetAllAsync(query.Request);
            return Result<PagedResult<CaseListItemDto>>.Success(result);
        }

    }
}
