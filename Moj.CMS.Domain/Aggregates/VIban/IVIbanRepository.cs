using Moj.CMS.Domain.ParameterObjects.VIban;
using Moj.CMS.Domain.Shared.Repositories;
using System.Threading.Tasks;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Domain.Aggregates.VIban
{
    [ScopedService]
    public interface IVIbanRepository : IDomainRepository<VIban>
    {
    }
}
