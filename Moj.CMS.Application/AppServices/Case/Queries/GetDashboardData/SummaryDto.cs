using Moj.CMS.Shared.DTO;

namespace Moj.CMS.Application.AppServices.Case.Queries.GetDashboardData
{
    public class SummaryDto
    {
        public int TodayCasesCount { get; set; }
        public MoneyDto TodayClaims { get; set; } = MoneyDto.Default;
        public int Reports { get; set; }
        public MoneyDto Paid { get; set; } = MoneyDto.Default;
        public MoneyDto UnPaid { get; set; } = MoneyDto.Default;
        public int TechnicalProblems { get; set; }
    }
}