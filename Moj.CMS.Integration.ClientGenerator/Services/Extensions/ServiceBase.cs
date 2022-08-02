using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Integration.ClientGenerator.Services.Extensions
{
    public partial class ServiceBase
    {
        public string BearerToken { get; set; }

        protected Task PrepareRequestAsync(HttpClient client, HttpRequestMessage request, StringBuilder urlBuilder)
        {
            return Task.CompletedTask;
        }

        protected Task PrepareRequestAsync(HttpClient client, HttpRequestMessage request, string url)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", BearerToken);
            return Task.CompletedTask;
        }

        protected Task ProcessResponseAsync(HttpClient client, HttpResponseMessage response, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
