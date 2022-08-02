using Moj.CMS.Domain.Aggregates.Case.ValueObjects;
using Moj.CMS.Domain.DomainServices;

namespace Moj.CMS.Domain.ParameterObjects.Case
{
    public class CaseDetailsParam
    {
        public CaseDetails CaseDetails { get; set; }
        public IEnforceJudgeIsExists EnforceJudgeIsExists { get; set; }
        public IEnforceCourtIsExists EnforceCourtIsExists { get; set; }
        public IGetDivisionCourtCode GetDivisionCourtCode { get; set; }
    }
}
