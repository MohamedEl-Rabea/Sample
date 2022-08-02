using Moj.CMS.Shared.DTO;

namespace Moj.CMS.Application.AppServices.Case.Queries.GetCaseSummary
{
    public class FinancialDto
    {
        public MoneyDto CurrentAmount { get; set; }
        public MoneyDto RemainingAmount { get; set; }
        public int ClaimsCount { get; set; }
    }
}