using MediatR;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Case.Queries.GetCaseClaims
{
    public class GetCaseClaimsQuery : Query<Result<GetCaseClaimsDto>>
    {
        public int CaseId { get; set; }
    }
    public class GetCaseClaimsQueryHandler : IRequestHandler<GetCaseClaimsQuery, Result<GetCaseClaimsDto>>
    {
        private readonly ICaseQueries _caseQueries;

        public GetCaseClaimsQueryHandler(ICaseQueries caseQueries)
        {
            _caseQueries = caseQueries;
        }

        public async Task<Result<GetCaseClaimsDto>> Handle(GetCaseClaimsQuery request, CancellationToken cancellationToken)
        {
            var data = await _caseQueries.GetCaseClaimsAsync(request.CaseId);
            return  Result<GetCaseClaimsDto>.Success(data);
        }
    }
}
