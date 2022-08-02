using System.Threading.Tasks;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Domain.DomainServices
{
    [TransientService]
    public interface IEnforceCaseNumberIsUnique
    {
        Task<bool> IsUniqueAsync(int caseId, string caseNumber);
    }
}
