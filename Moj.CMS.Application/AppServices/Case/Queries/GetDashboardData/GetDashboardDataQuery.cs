using MediatR;
using Moj.CMS.Application.Interfaces.Queries;
using System.Threading;
using System.Threading.Tasks;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;

namespace Moj.CMS.Application.AppServices.Case.Queries.GetDashboardData
{
    public class GetDashboardDataQuery : Query<Result<DashboardDto>>
    {
    }

    public class GetDashboardDataQueryHandler : IRequestHandler<GetDashboardDataQuery, Result<DashboardDto>>
    {
        private readonly ICaseQueries _caseQueries;

        public GetDashboardDataQueryHandler(ICaseQueries caseQueries)
        {
            _caseQueries = caseQueries;
        }

        public async Task<Result<DashboardDto>> Handle(GetDashboardDataQuery request, CancellationToken cancellationToken)
        {
            var dashboardDto = new DashboardDto();

            dashboardDto.Summary = await _caseQueries.GetDashboardSummaryAsync();

            dashboardDto.LastCases = await _caseQueries.GetLastCasesAsync();

            return Result<DashboardDto>.Success(dashboardDto);
        }
    }
}
