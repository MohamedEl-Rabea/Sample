using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Domain.DomainServices.Party;
using System.Threading.Tasks;

namespace Moj.CMS.Application.DomainServices.Party
{
    public class EnforcePartyNumberIsUnique : IEnforcePartyNumberIsUnique
    {
        private readonly IPartyQueries _partyQueries;
        public EnforcePartyNumberIsUnique(IPartyQueries partyQueries)
        {
            _partyQueries = partyQueries;
        }

        public async Task<bool> IsUniqueAsync(string partyNumber, int _partyId)
        {
            var partyId = await _partyQueries.GetPartyIdByPartyNumberAsync(partyNumber);
            return partyId == 0 || partyId == _partyId;
        }
    }
}
