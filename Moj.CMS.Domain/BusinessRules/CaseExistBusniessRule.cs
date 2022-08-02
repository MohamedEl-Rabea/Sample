using System.Threading.Tasks;
using Moj.CMS.Domain.DomainServices;
using Moj.CMS.Domain.Shared.Entities;

namespace Moj.CMS.Domain.BusinessRules
{
    public class CaseExistBusniessRule : IAsyncBusinessRule
    {
        private readonly IEnforceCaseIsFound _enforceCaseIsFound;
        private readonly string _caseNumber;

        public CaseExistBusniessRule(IEnforceCaseIsFound enforceCaseIsFound, string caseNumber)
        {
            _enforceCaseIsFound = enforceCaseIsFound;
            _caseNumber = caseNumber;
        }


        public string Message => $"Case with Number {_caseNumber} not found";

        public async Task<bool> IsBrokenAsync()
        {
            var IsFound = await _enforceCaseIsFound.IsFound(_caseNumber);
            return !IsFound;
        }
    }
}
