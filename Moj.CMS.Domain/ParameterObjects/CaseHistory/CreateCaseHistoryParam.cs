using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Domain.Shared.Values;
using System;

namespace Moj.CMS.Domain.ParameterObjects.CaseHistory
{
    public class CreateCaseHistoryParam
    {
        public string CaseNumber { get; set; }
        public CaseOperationEnum Operation { get; set; }
        public DateTime OperationDateTime { get; set; }
        public LocalizedText Details { get; set; }
    }
}
