using Moj.CMS.Domain.Shared.Repositories;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Domain.Aggregates.Promissory
{
    [ScopedService]
    public interface IPromissoryRepository : IDomainRepository<Promissory>
    {
        
    }
}
