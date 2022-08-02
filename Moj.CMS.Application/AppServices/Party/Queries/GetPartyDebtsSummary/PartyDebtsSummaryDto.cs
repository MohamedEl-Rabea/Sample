using Moj.CMS.Shared.DTO;

namespace Moj.CMS.Application.AppServices.Party.Queries
{
    public class PartyDebtsSummaryDto
    {
        public int PartyId { get; set; }
        public int CasesCount { get; set; }
        public MoneyDto TotalDebtsAmount { get; set; }
        public MoneyDto TotalPaidAmount { get; set; }
        public MoneyDto TotalTransferedAmount { get; set; }
        public MoneyDto TotalRemainingAmount { get; set; }

    }
}
