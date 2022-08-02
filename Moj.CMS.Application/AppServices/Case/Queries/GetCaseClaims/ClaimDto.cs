using Moj.CMS.Shared.DTO;
using System.Collections.Generic;

namespace Moj.CMS.Application.AppServices.Case.Queries.GetCaseClaims
{
    public class ClaimDto
    {
        public string ComplaintPartyName { get; set; }
        public string ClaimNumber { get; set; }
        public bool IsJoint { get; set; }
        public MoneyDto BasicAmount { get; set; }
        public MoneyDto RequiredAmount { get; set; }
        public MoneyDto RemainingAmount { get; set; }
        public bool ShowDetails { get; set; }
        public IEnumerable<ClaimDetailsDto> ClaimDetails { get; set; }
    }
}
