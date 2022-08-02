using Moj.CMS.Domain.Aggregates.Case.ValueObjects;
using Moj.CMS.Domain.DomainServices;
using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Domain.Shared.Values;

namespace Moj.CMS.Domain.ParameterObjects.Case
{
    public abstract class CaseParameterBasicInfo
    {
        public string CaseNumber { get; set; }
        public CaseTypeEnum CaseTypeId { get; set; }
        public CaseStatusEnum CaseStatusId { get; set; }
        public Money CaseBasicAmount { get; set; }
        public Money RequiredAmount { get; set; }
        public CaseDetailsParam CaseDetailsParam { get; set; }
        public CaseDate DatesInfo { get; set; }
        public IEnforceCaseNumberIsUnique EnforceCaseNumberIsUnique { get; set; }
    }
}