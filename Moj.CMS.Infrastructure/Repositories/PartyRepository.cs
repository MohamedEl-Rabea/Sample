using Moj.CMS.Domain.Aggregates.Party;
using Moj.CMS.Domain.Shared.Repositories;
using Moj.CMS.Shared.Infrastructure.Repositories;

namespace Moj.CMS.Infrastructure.Repositories
{
    public class PartyRepository : BaseDomainRepository<Party>, IPartyRepository
    {
        public PartyRepository(IRepository<Party, int> genericRepository) : base(genericRepository)
        {
        }
    }
}
