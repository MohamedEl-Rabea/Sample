using Moj.CMS.Domain.Aggregates.Case.ValueObjects;
using Moj.CMS.Domain.Shared.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Moj.CMS.Domain.Aggregates.Case.BusinessRules
{
    public class CasePartiesHasNoDupplicatesBusinessRule : IBusinessRule
    {
        private readonly IEnumerable<CaseParty> _caseParties;
        public CasePartiesHasNoDupplicatesBusinessRule(IEnumerable<CaseParty> caseParties)
        {
            _caseParties = caseParties;
        }
        // note that it display db id not partynumber or name
        public string Message => $"party with {string.Join(",", _caseParties.GroupBy(cp => cp.PartyNumber).Where(g => g.Count() > 1).Select(k => k.Key).ToList())}  already exist";

        public bool IsBroken()
        {
            var isUnique = _caseParties.Select(cp => cp.PartyNumber).Distinct().Count() == _caseParties.Count();
            return !isUnique;
        }
    }
}
