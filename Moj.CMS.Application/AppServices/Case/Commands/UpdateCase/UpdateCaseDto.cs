using Moj.CMS.Application.AppServices.Case.Commands.AddCase;
using Moj.CMS.Domain.Shared.Enums;

namespace Moj.CMS.Application.AppServices.Case.Commands.UpdateCase
{
    public class UpdateCaseDto
    {
        public string CaseNumber { get; set; }
        public CaseTypeEnum CaseType { get; set; }
        public CaseStatusEnum CaseStatus { get; set; }
        public CaseDetailsDto CaseDetails { get; set; }
    }
}