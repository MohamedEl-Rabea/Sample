using Moj.CMS.Application.Integration.Models;
using Moj.CMS.Domain.Shared.Repositories;
using Moj.CMS.Domain.Shared.Values;
using Moj.CMS.Integration.Contracts.Constants;
using Moj.CMS.Integration.Contracts.Runtime;
using System.Threading.Tasks;

namespace Moj.CMS.Integration.Application.Services
{
    public class IntegrationAuthorizationProvider : IIntegrationAuthorizationProvider
    {
        private readonly IRepository<ClientIntegrationSettings> _integrationRepository;

        public IntegrationAuthorizationProvider(IRepository<ClientIntegrationSettings> integrationRepository)
        {
            _integrationRepository = integrationRepository;
        }

        public async Task<string> TryGetTokenAsync(string clientId, ClientTypeEnum clientType)
        {
            var authToken = string.Empty;

            var clientSettings = await _integrationRepository.FirstOrDefaultAsync(c =>
                c.ClientId == clientId && c.ClientType == clientType);

            if (clientSettings != null && clientSettings.TokenExpiresAt >= CLock.Now)
                authToken = clientSettings.AuthToken;

            return authToken;
        }
    }
}
