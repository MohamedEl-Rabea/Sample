using MediatR;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Case.Queries.GetCaseSummary
{
    public class GetCaseSummaryQuery : Query<Result<CaseSummaryDto>>
    {
        public int CaseId { get; set; }
    }

    public class GetCaseSummaryQueryHandler : IRequestHandler<GetCaseSummaryQuery, Result<CaseSummaryDto>>
    {
        private readonly ICaseQueries _caseQueries;

        public GetCaseSummaryQueryHandler(ICaseQueries caseQueries)
        {
            _caseQueries = caseQueries;
        }

        public async Task<Result<CaseSummaryDto>> Handle(GetCaseSummaryQuery request, CancellationToken cancellationToken)
        {
            var data = await _caseQueries.GetCaseSummaryAsync(request.CaseId);
            return Result<CaseSummaryDto>.Success(data);
        }
    }
}
