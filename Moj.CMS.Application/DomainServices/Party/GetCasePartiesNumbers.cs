using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Domain.DomainServices.Party;
using Moj.CMS.Domain.Shared.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moj.CMS.Application.DomainServices.Party
{
    public class GetCasePartiesNumbers : IGetCasePartiesNumbers
    {
        private readonly ICaseQueries _caseQueries;

        public GetCasePartiesNumbers(ICaseQueries caseQueries)
        {
            _caseQueries = caseQueries;
        }

        public async Task<Dictionary<string, PartyRoleEnum>> GetAsync(string _caseNumber)
        {
            return (await _caseQueries.GetPartiesInfoByCaseNumberAsync(_caseNumber)).ToDictionary(x => x.PartyNumber, x => x.PartyRole);
        }
    }
}
