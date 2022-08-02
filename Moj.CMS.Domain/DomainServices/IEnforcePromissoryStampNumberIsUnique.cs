using System.Threading.Tasks;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Domain.DomainServices
{
    [TransientService]
    public interface IEnforcePromissoryNumberIsUnique
    {
        Task<bool> IsUniqueAsync(int promissoryId, string promissoryNumber);
    }
}
