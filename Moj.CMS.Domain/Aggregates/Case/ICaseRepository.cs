using Moj.CMS.Domain.Shared.Repositories;
using System.Threading.Tasks;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Domain.Aggregates.Case
{
    [ScopedService]
    public interface ICaseRepository: IDomainRepository<Case>
    {
        Task<Case> GetCaseByNumberAsync(string caseNumber);
    }
}
