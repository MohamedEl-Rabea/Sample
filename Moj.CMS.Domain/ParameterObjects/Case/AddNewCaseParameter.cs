using Moj.CMS.Domain.Aggregates.Case.ValueObjects;
using System.Collections.Generic;

namespace Moj.CMS.Domain.ParameterObjects.Case
{
    public class AddNewCaseParameter : CaseParameterBasicInfo
    {
        public IEnumerable<CaseParty> CaseParties { get; set; }
        public IEnumerable<CasePromissory> CasePromissories { get; set; }
    }
}
