using Moj.CMS.Domain.Shared.Enums;

namespace Moj.CMS.Application.AppServices.Claims.Queries.Dtos
{
    public class CaseClaimStatusDto
    {
        public int ClaimId { get; set; }
        public ClaimFinancialStatusEnum ClaimStatus { get; set; }
    }
}
