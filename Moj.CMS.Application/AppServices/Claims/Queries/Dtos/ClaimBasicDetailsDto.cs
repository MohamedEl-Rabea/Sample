using Moj.CMS.Shared.DTO;

namespace Moj.CMS.Application.AppServices.Claims.Queries.Dtos
{
    public class ClaimBasicDetailsDto
    {
        public string ClaimNumber { get; set; }
        public string ComplaintPartyNumber { get; set; }
        public string ComplaintPartyName { get; set; }
        public MoneyDto RequiredAmount { get; set; }
        public MoneyDto RemainingAmount { get; set; }
    }
}
