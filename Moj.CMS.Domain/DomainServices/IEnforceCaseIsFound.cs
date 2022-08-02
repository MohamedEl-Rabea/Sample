using System.Threading.Tasks;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Domain.DomainServices
{
    [TransientService]
    public interface IEnforceCaseIsFound
    {
        Task<bool> IsFound(string caseNumber);
    }
}
