using System.Threading.Tasks;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Domain.DomainServices.Party
{
    [TransientService]
    public interface IEnforcePartyNumberIsUnique
    {
        Task<bool> IsUniqueAsync(string partyNumber, int _partyId);
    }
}
