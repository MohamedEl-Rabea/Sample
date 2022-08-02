using Moj.CMS.Integration.Contracts.Constants;
using Moj.CMS.Integration.Contracts.Runtime;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Integration.Contracts.Handlers
{
    public class HttpContextHandler : DelegatingHandler
    {
        private readonly IIntegrationExecutionContextAccessor _integrationExecutionContextAccessor;
        
        public HttpContextHandler(IIntegrationExecutionContextAccessor integrationExecutionContextAccessor)
        {
            _integrationExecutionContextAccessor = integrationExecutionContextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add(HttpHeadersConsts.CorrelationIdHeader, _integrationExecutionContextAccessor.ExecutionContext.RequestId.ToString());
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
