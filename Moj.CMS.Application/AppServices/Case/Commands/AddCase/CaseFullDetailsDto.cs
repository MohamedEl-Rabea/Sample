using Moj.CMS.Application.AppServices.Claims.Commands.CreateFinancialClaim;
using Moj.CMS.Application.AppServices.Promissory.Dtos;
using System.Collections.Generic;

namespace Moj.CMS.Application.AppServices.Case.Commands.AddCase
{
    public class CaseFullDetailsDto
    {
        public CaseDetailsDto BasicDetails { get; set; }
        public IEnumerable<PromissoryDto> PromissoryList { get; set; } = new List<PromissoryDto>();
        public IEnumerable<CasePartyDto> Requesters { get; set; } = new List<CasePartyDto>();
        public IEnumerable<CasePartyDto> Respondents { get; set; } = new List<CasePartyDto>();
        public IEnumerable<ClaimDto> Claims { get; set; } = new List<ClaimDto>();
    }
}
