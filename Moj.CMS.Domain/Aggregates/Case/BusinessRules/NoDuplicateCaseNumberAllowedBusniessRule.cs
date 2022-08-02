using System.Threading.Tasks;
using Moj.CMS.Domain.DomainServices;
using Moj.CMS.Domain.Shared.Entities;

namespace Moj.CMS.Domain.Aggregates.Case.BusinessRules
{
    public class NoDuplicateCaseNumberAllowedBusniessRule : IAsyncBusinessRule
    {
        private readonly IEnforceCaseNumberIsUnique _enforceCaseNumberIsUnique;
        private readonly int _caseId;
        private readonly string _caseNumber;

        public NoDuplicateCaseNumberAllowedBusniessRule(IEnforceCaseNumberIsUnique enforceCaseNumberIsUnique, string caseNumber, int caseId)
        {
            _enforceCaseNumberIsUnique = enforceCaseNumberIsUnique;
            _caseNumber = caseNumber;
            _caseId = caseId;
        }

        public string Message => "Cannot add duplicate case number.";

        public async Task<bool> IsBrokenAsync()
        {
            var isUnique = await _enforceCaseNumberIsUnique.IsUniqueAsync(_caseId, _caseNumber);
            return !isUnique;
        }
    }
}
