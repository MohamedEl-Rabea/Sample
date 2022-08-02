using Moj.CMS.Domain.Aggregates.VIban;
using Moj.CMS.Domain.ParameterObjects.VIban;
using Moj.CMS.Domain.Shared.Repositories;
using Moj.CMS.Shared.Infrastructure.Repositories;
using System.Threading.Tasks;

namespace Moj.CMS.Infrastructure.Repositories
{
    public class VIbanRepository : BaseDomainRepository<VIban>, IVIbanRepository
    {
        public VIbanRepository(IRepository<VIban> genericRepository) : base(genericRepository)
        {
        }
    }
}
