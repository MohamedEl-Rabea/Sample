using System.Collections.Generic;

namespace Moj.CMS.Application.AppServices.Case.Queries.GetCaseParties
{
    public class CasePartiesDto
    {
        public IEnumerable<PartyDto> Parties { get; set; }
        public PartyRoleSummary Summary { get; set; }
    }
}
