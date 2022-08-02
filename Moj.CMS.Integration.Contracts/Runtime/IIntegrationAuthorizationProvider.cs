using Moj.CMS.Integration.Contracts.Constants;
using System.Threading.Tasks;

namespace Moj.CMS.Integration.Contracts.Runtime
{
    public interface IIntegrationAuthorizationProvider
    {
        Task<string> TryGetTokenAsync(string clientId, ClientTypeEnum clientType);
    }
}
