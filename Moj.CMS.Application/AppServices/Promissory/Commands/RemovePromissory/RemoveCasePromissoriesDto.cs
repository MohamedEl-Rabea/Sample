using System.Collections.Generic;

namespace Moj.CMS.Application.AppServices.Promissory.Commands.RemovePromissory
{
    public class RemoveCasePromissoriesDto
    {
        public string CaseNumber { get; set; }
        public IEnumerable<string> PromissoryNumbers { get; set; }
    }
}
