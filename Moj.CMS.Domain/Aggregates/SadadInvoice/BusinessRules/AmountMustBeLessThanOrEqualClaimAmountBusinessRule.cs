using Moj.CMS.Domain.DomainServices.SadadInvoice;
using Moj.CMS.Domain.Shared.Entities;
using Moj.CMS.Domain.Shared.Values;
using System.Threading.Tasks;

namespace Moj.CMS.Domain.Aggregates.SadadInvoice.BusinessRules
{
    public class AmountMustBeLessThanOrEqualClaimAmountBusinessRule : IAsyncBusinessRule
    {
        private readonly string _claimNumber;
        private readonly Money _amount;
        private readonly IClaimInfoProvider _claimInfoProvider;

        public AmountMustBeLessThanOrEqualClaimAmountBusinessRule(string claimNumber, Money amount, IClaimInfoProvider claimInfoProvider)
        {
            _claimNumber = claimNumber;
            _amount = amount;
            _claimInfoProvider = claimInfoProvider;
        }

        public string Message => $"Claim with number {_claimNumber} has amount less than {_amount}";

        public async Task<bool> IsBrokenAsync()
        {
            var claimRemainingAmount = await _claimInfoProvider.GetClaimTotalRemainingAmountAsync(_claimNumber);
            return claimRemainingAmount < _amount;
        }
    }
}