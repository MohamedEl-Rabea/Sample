using Moj.CMS.Shared.DTO;

namespace Moj.CMS.Application.AppServices.Party.Queries
{
    public class PartyCreditsSummaryDto
    {
        public int PartyId { get; set; }
        public int CasesCount { get; set; }
        public MoneyDto TotalCreditsAmount { get; set; }
        public MoneyDto TotalCollectedAmount { get; set; }
        public MoneyDto TotalTransferedAmount { get; set; }
        public MoneyDto TotalRemainingAmount { get; set; }
    }
}
