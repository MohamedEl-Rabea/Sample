using System.Collections.Generic;

namespace Moj.CMS.Application.AppServices.Party.Queries
{
    public class PartyCaseListDto
    {
        public IEnumerable<PartyCaseListItemDto> AccusedCaseList { get; set; }
        public IEnumerable<PartyCaseListItemDto> ComplaintCaseList { get; set; }
    }
}