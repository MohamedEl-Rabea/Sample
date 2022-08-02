using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Domain.DomainServices.SadadInvoice;
using Moj.CMS.Domain.Shared.Values;
using Moj.CMS.Shared.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moj.CMS.Application.DomainServices.SadadInvoice
{
    public class ClaimInfoProvider : IClaimInfoProvider
    {
        private readonly IClaimQueries _claimQueries;

        public ClaimInfoProvider(IClaimQueries claimQueries)
        {
            _claimQueries = claimQueries;
        }

        public async Task<IEnumerable<string>> GetClaimAccusedPartyNumbersAsync(string claimNumber)
        {
            return await _claimQueries.GetClaimAccusedPartyNumbersAsync(claimNumber);
        }

        public async Task<int> GetClaimIdAsync(string claimNumber)
        {
            return await _claimQueries.GetClaimIdByNumberAsync(claimNumber);
        }

        public async Task<Money> GetClaimTotalRemainingAmountAsync(string claimNumber)
        {
            var result= await _claimQueries.GetClaimTotalRemainingAmountAsync(claimNumber);
            return Money.Create(result.CurrencyIso,result.Value);
        }
    }
}
