using System.Linq;
using System.Threading.Tasks;
using Moj.CMS.Domain.Aggregates.Claim;
using Moj.CMS.Domain.Shared.Repositories;
using Moj.CMS.Shared.Infrastructure.Repositories;

namespace Moj.CMS.Infrastructure.Repositories
{
    public class ClaimRepository : BaseDomainRepository<Claim>, IClaimRepository
    {
        public ClaimRepository(IRepository<Claim> genericRepository) : base(genericRepository)
        {
        }

        public async Task<Claim> GetClaimByNumberAsync(string claimNumber)
        {
            var claim = (await GetAllAsync(fc => fc.Id.ToString() == claimNumber)).SingleOrDefault();
            return claim;
        }
    }
}
