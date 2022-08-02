using Moj.CMS.Application.AppServices.Case.Commands.AddCase;
using System.Collections.Generic;

namespace Moj.CMS.Application.AppServices.Case.Commands.AddCaseParty
{
    public class AddCasePartiesDto
    {
        public string CaseNumber { get; set; }
        public IEnumerable<CasePartyDto> Requesters { get; set; } = new List<CasePartyDto>();
        public IEnumerable<CasePartyDto> Respondents { get; set; } = new List<CasePartyDto>();
    }
}
