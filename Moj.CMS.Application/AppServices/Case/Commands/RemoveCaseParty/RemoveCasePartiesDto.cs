using System.Collections.Generic;

namespace Moj.CMS.Application.AppServices.Case.Commands.RemoveCaseParty
{
    public class RemoveCasePartiesDto
    {
        public string CaseNumber { get; set; }
        public IEnumerable<string> PartiesIdentityNumbers { get; set; }
    }
}
