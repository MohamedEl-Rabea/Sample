using Moj.CMS.Domain.Aggregates.Promissory;
using Moj.CMS.Domain.Shared.Repositories;
using Moj.CMS.Shared.Infrastructure.Repositories;

namespace Moj.CMS.Infrastructure.Repositories
{
    public class PromissoryRepository : BaseDomainRepository<Promissory>, IPromissoryRepository
    {
        public PromissoryRepository(IRepository<Promissory, int> genericRepository) : base(genericRepository)
        {
        }
    }
}
