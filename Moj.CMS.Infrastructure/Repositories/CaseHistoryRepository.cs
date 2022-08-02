using Moj.CMS.Domain.Aggregates.CaseHistory;
using Moj.CMS.Domain.Shared.Repositories;
using Moj.CMS.Shared.Infrastructure.Repositories;

namespace Moj.CMS.Infrastructure.Repositories
{
    public class CaseHistoryRepository : BaseDomainRepository<CaseHistory>, ICaseHistoryRepository
    {
        public CaseHistoryRepository(IRepository<CaseHistory> genericRepository) : base(genericRepository)
        {
        }
    }
}
