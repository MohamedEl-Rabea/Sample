using Moj.CMS.Shared.DTO;

namespace Moj.CMS.Application.AppServices.Party.Queries
{
    public class PartyAmountResult
    {
        public string CaseNumber { get; set; }
        public int CaseClaimCount { get; set; }
        public string PartyNumber { get; set; }
        public MoneyDto TotalPartyCaseAccuesedAmount { get; set; }
        public MoneyDto TotalRemainingPartyCaseAccuesedAmount { get; set; }
        public MoneyDto TotalPartyCaseComplaintAmount { get; set; }
        public MoneyDto TotalRemainingPartyCaseComplaintAmount { get; set; }
        public MoneyDto TotalPaidPartyCaseComplaintAmount => TotalPartyCaseComplaintAmount.Subtract(TotalRemainingPartyCaseComplaintAmount);
    }
}