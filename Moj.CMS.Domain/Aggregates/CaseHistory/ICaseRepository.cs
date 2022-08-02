using Moj.CMS.Domain.Shared.Repositories;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Domain.Aggregates.CaseHistory
{
    [ScopedService]
    public interface ICaseHistoryRepository : IDomainRepository<CaseHistory>
    {
    }
}
