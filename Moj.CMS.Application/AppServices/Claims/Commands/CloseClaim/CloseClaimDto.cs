using System;

namespace Moj.CMS.Application.AppServices.Claims.Commands.CloseClaim
{
    public class CloseClaimDto
    {
        public string ClaimNumber { get; set; }
        public string ReferenceNumber { get; set; }
        public DateTime CloseDate { get; set; }
    }
}
