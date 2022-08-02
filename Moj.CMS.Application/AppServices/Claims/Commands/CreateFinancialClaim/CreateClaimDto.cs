namespace Moj.CMS.Application.AppServices.Claims.Commands.CreateFinancialClaim
{
    public class CreateClaimDto
    {
        public string CaseNumber { get; set; }
        public string PromissoryNumber { get; set; }

        public ClaimDto Claim { get; set; }
    }
}
