using Moj.CMS.Domain.DomainServices.SadadInvoice;
using Moj.CMS.Domain.Shared.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Moj.CMS.Domain.Aggregates.SadadInvoice.BusinessRules
{
    public class PartyMustBeAccusedBusinessRule : IAsyncBusinessRule
    {
        private readonly string _claimNumber;
        private readonly string _partyNumber;
        private readonly IClaimInfoProvider  _claimInfoProvider;

        public PartyMustBeAccusedBusinessRule(string claimNumber, string partyNumber, IClaimInfoProvider claimInfoProvider)
        {
            _claimNumber = claimNumber;
            _partyNumber = partyNumber;
            _claimInfoProvider = claimInfoProvider;
        }

        public string Message => $"Party with number {_partyNumber} not accused party on claim number {_claimNumber}";

        public async Task<bool> IsBrokenAsync()
        {
            var accusedParties = await _claimInfoProvider.GetClaimAccusedPartyNumbersAsync(_claimNumber);
            return !accusedParties.Contains(_partyNumber);
        }
    }
}