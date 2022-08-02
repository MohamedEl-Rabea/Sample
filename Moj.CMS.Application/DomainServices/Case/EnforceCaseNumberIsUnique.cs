using Moj.CMS.Application.Interfaces.Queries;
using System.Threading.Tasks;
using Moj.CMS.Domain.DomainServices;

namespace Moj.CMS.Application.DomainServices.Case
{
    public class EnforceCaseNumberIsUnique : IEnforceCaseNumberIsUnique
    {
        private readonly ICaseQueries _caseQueries;

        public EnforceCaseNumberIsUnique(ICaseQueries caseQueries)
        {
            _caseQueries = caseQueries;
        }

        public async Task<bool> IsUniqueAsync(int caseId, string caseNumber)
        {
            var currentCaseId = await _caseQueries.GetCaseIdByCaseNumberAsync(caseNumber);
            return currentCaseId == 0 || currentCaseId == caseId;
        }
    }
}
