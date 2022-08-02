using Moj.CMS.Domain.DomainServices.Party;
using Moj.CMS.Domain.Shared.Entities;
using System.Threading.Tasks;

namespace Moj.CMS.Domain.Aggregates.Party.BusinessRules
{
    public class PartyNumberShouldNotBeDuplicatedBusinessRule : IAsyncBusinessRule
    {
        private readonly IEnforcePartyNumberIsUnique _enforcePartyNumberIsUnique;
        private readonly string _partyNumber;
        private readonly int _partyId;

        public PartyNumberShouldNotBeDuplicatedBusinessRule(IEnforcePartyNumberIsUnique enforcePartyNumberIsUnique, string partyNumber, int partyId)
        {
            _enforcePartyNumberIsUnique = enforcePartyNumberIsUnique;
            _partyNumber = partyNumber;
            _partyId = partyId;
        }
        public string Message => $"party with number= {_partyNumber} already exist";

        public async Task<bool> IsBrokenAsync()
        {
            var isUnique = await _enforcePartyNumberIsUnique.IsUniqueAsync(_partyNumber, _partyId);
            return !isUnique;
        }
    }
}
