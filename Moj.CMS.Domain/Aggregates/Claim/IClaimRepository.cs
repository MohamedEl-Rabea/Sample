using System.Threading.Tasks;
using Moj.CMS.Domain.Shared.Repositories;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Domain.Aggregates.Claim
{
    [TransientService]
    public interface IClaimRepository : IDomainRepository<Claim>
    {
        Task<Claim> GetClaimByNumberAsync(string claimNumber);
    }
}
