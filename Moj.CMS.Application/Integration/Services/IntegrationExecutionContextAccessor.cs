using Moj.CMS.Integration.Contracts.Runtime;
using Moj.CMS.Shared.Runtime;

namespace Moj.CMS.Integration.Application.Services
{
    public class IntegrationExecutionContextAccessor : IIntegrationExecutionContextAccessor
    {
        private readonly IApplicationSession _applicationSession;

        public IntegrationExecutionContextAccessor(IApplicationSession applicationSession)
        {
            _applicationSession = applicationSession;
        }

        public ExecutionContext ExecutionContext => new ExecutionContext
        {
            RequestId = _applicationSession.RequestId,
            UserId = _applicationSession.UserId
        };
    }
}
