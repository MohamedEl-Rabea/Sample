using Moj.CMS.Shared.DTO;

namespace Moj.CMS.Application.AppServices.Case.Queries.GetCaseClaims
{
    public class ClaimDetailsDto
    {
        public string PartyName { get; set; }
        public MoneyDto RequiredAmount { get; set; } = new();
        public MoneyDto BillingAmount { get; set; } = new();
        public MoneyDto CollectedByReports { get; set; } = new();
        public MoneyDto Cashing { get; set; } = new();
        public MoneyDto Transfer { get; set; } = new();
        public MoneyDto Cheque { get; set; } = new();
        public MoneyDto Named { get; set; } = new();
        public MoneyDto Paid { get; set; } = new();
    }
}
