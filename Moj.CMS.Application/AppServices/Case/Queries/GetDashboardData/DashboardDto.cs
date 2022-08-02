using System.Collections.Generic;

namespace Moj.CMS.Application.AppServices.Case.Queries.GetDashboardData
{
    public class DashboardDto
    {
        public DashboardDto()
        {
            LastCases = new List<CaseDto>();
        }

        public SummaryDto Summary { get; set; } = new SummaryDto();

        public IEnumerable<CaseDto> LastCases { get; set; }
    }
}