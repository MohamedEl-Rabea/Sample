using Moj.CMS.Domain.Aggregates.Case;
using Moj.CMS.Domain.Shared.Repositories;
using Moj.CMS.Shared.Infrastructure.Repositories;
using System.Linq;
using System.Threading.Tasks;
namespace Moj.CMS.Infrastructure.Repositories
{
    public class CaseRepository : BaseDomainRepository<Case>, ICaseRepository
    {
        public CaseRepository(IRepository<Case> genericRepository) : base(genericRepository)
        {
        }

        public async Task<Case> GetCaseByNumberAsync(string caseNumber)
        {
            var result = (await GetAllAsync(c => c.CaseNumber == caseNumber)).SingleOrDefault();
            return result;
        }
    }
}
