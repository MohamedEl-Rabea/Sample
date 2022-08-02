using Moj.CMS.Application.Interfaces.Queries;
using System.Threading.Tasks;
using Moj.CMS.Domain.DomainServices;

namespace Moj.CMS.Application.DomainServices.Case
{
    public class EnforceCaseIsFound : IEnforceCaseIsFound
    {
        private readonly ICaseQueries _caseQueries;

        public EnforceCaseIsFound(ICaseQueries caseQueries)
        {
            _caseQueries = caseQueries;
        }

        public async Task<bool> IsFound(string caseNumber)
        {
            var currentCaseId = await _caseQueries.GetCaseIdByCaseNumberAsync(caseNumber);
            return currentCaseId != 0;
        }
    }
}
