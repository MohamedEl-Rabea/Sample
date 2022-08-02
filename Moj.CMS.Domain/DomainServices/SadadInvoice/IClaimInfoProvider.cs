using Moj.CMS.Domain.Shared.Values;
using System.Collections.Generic;
using System.Threading.Tasks;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Domain.DomainServices.SadadInvoice
{
    [TransientService]
    public interface IClaimInfoProvider
    {
        Task<int> GetClaimIdAsync(string claimNumber);

        Task<IEnumerable<string>> GetClaimAccusedPartyNumbersAsync(string claimNumber);

        Task<Money> GetClaimTotalRemainingAmountAsync(string claimNumber);
    }
}
