using MediatR;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Case.Queries.GetCaseParties
{
    public class GetCasePartiesQuery : Query<Result<CasePartiesDto>>
    {
        public int CaseId { get; set; }
    }

    public class GetCasePartiesQueryHandler : IRequestHandler<GetCasePartiesQuery, Result<CasePartiesDto>>
    {
        private readonly ICaseQueries _caseQueries;

        public GetCasePartiesQueryHandler(ICaseQueries caseQueries)
        {
            _caseQueries = caseQueries;
        }

        public async Task<Result<CasePartiesDto>> Handle(GetCasePartiesQuery request, CancellationToken cancellationToken)
        {
            var data = await _caseQueries.GetCasePartiesAsync(request.CaseId);
            return Result<CasePartiesDto>.Success(data);
        }
    }
}
