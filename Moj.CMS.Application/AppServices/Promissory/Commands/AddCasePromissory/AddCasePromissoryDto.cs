using Moj.CMS.Application.AppServices.Promissory.Dtos;
using System.Collections.Generic;

namespace Moj.CMS.Application.AppServices.Promissory.Commands.AddCasePromissory
{
    public class AddCasePromissoryDto
    {
        public string CaseNumber { get; set; }
        public IEnumerable<PromissoryDto> PromissoryDtoList { get; set; }
    }
}
