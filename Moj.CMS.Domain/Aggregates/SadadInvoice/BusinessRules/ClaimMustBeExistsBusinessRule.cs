using Moj.CMS.Domain.DomainServices.SadadInvoice;
using Moj.CMS.Domain.Shared.Entities;
using System.Threading.Tasks;

namespace Moj.CMS.Domain.Aggregates.SadadInvoice.BusinessRules
{
    public class ClaimMustBeExistsBusinessRule : IAsyncBusinessRule
    {
        private readonly string _claimNumber;
        private readonly IClaimInfoProvider _getClaimId;

        public ClaimMustBeExistsBusinessRule(string claimNumber, IClaimInfoProvider getClaimId)
        {
            _claimNumber = claimNumber;
            _getClaimId = getClaimId;
        }

        public string Message => $"Claim with number {_claimNumber} not found";

        public async Task<bool> IsBrokenAsync()
        {
            var claimId = await _getClaimId.GetClaimIdAsync(_claimNumber);
            return claimId == 0;
        }
    }
}
