using Moj.CMS.Domain.Aggregates.Case.ValueObjects;
using Moj.CMS.Domain.Shared.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Moj.CMS.Domain.Aggregates.Case.BusinessRules
{
    public class CasePromissoriesHasNoDupplicatesBusinessRule : IBusinessRule
    {
        private readonly string _caseNumber;

        private readonly IEnumerable<CasePromissory> _casePromissories;
        public CasePromissoriesHasNoDupplicatesBusinessRule(string caseNumber, IEnumerable<CasePromissory> casePromissories)
        {
            _caseNumber = caseNumber;
            _casePromissories = casePromissories;
        }

        public string Message => $"Promissories with numbers " +
            $"{string.Join(",", _casePromissories.GroupBy(cp => cp.PromissoryNumber).Where(g => g.Count() > 1).Select(k => k.Key).ToList())}" +
            $"  already assigned to this case= {_caseNumber}";

        public bool IsBroken()
        {
            var isUnique = _casePromissories.Select(cp => cp.PromissoryNumber).Distinct().Count() == _casePromissories.Count();
            return !isUnique;
        }
    }
}
