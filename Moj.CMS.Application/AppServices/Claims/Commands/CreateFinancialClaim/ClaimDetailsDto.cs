using Moj.CMS.Shared.DTO;

namespace Moj.CMS.Application.AppServices.Claims.Commands.CreateFinancialClaim
{
    public class ClaimDetailsDto
    {
        public string AccusedPartyNumber { get; set; }
        public bool CreateSadadBill { get; set; }
        public MoneyDto RequiredAmount { get; set; }
        public MoneyDto BillingAmount { get; set; }
    }
}
