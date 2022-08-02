using System.Threading.Tasks;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Domain.DomainServices
{
    [TransientService]
    public interface IEnforceIbanNumberIsUnique
    {
        Task<bool> IsUniqueIbanAsync(string ibanNumber);
    }
}
