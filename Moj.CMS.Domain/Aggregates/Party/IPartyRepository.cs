using Moj.CMS.Domain.Shared.Repositories;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Domain.Aggregates.Party
{
    [ScopedService]
    public interface IPartyRepository: IDomainRepository<Party>
    {
    }
}
