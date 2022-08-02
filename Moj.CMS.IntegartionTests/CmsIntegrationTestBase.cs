using Microsoft.Extensions.DependencyInjection;
using Moj.CMS.Application.Interfaces;
using Moj.CMS.Infrastructure.Contexts;
using Moj.CMS.IntegartionTests.Helpers;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Moj.CMS.IntegartionTests
{
    public class CmsIntegrationTestBase<T> : IClassFixture<WebApplicationTestHostFactoryFixture<T>>, IDisposable
    {
        protected readonly WebApplicationTestHostFactoryFixture<T> _hostFactory;

        public CmsIntegrationTestBase(WebApplicationTestHostFactoryFixture<T> hostFactory)
        {
            _hostFactory = hostFactory;
        }

        protected ICmsModule CmsModule => _hostFactory.Host.Services.GetRequiredService<ICmsModule>();

        protected void UsingDbContext(Action<CMSDbContext> action)
        {
            using (var scope = _hostFactory.Host.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<CMSDbContext>();
                action.Invoke(dbContext);
            }
        }

        protected async Task<HttpResponseMessage> PostAsync(string requestUri, object payload)
        {
            var client = _hostFactory.GetHttpClient();
            var payloadSerialized = JsonConvert.SerializeObject(payload);
            var content = new StringContent(payloadSerialized, Encoding.UTF8, "application/json");
            return await client.PostAsync(requestUri, content);
        }

        protected async Task<IResult<TResult>> GetAsync<TResult>(string requestUri, object payload)
        {
            var client = _hostFactory.GetHttpClient();
            var payloadSerialized = JsonConvert.SerializeObject(payload);
            var content = new StringContent(payloadSerialized, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync(requestUri, content);
            return await DeerializeResponseContentAsync<TResult>(responseMessage);
        }
 
        protected async Task<TResult> ExecuteQueryAsync<TResult>(Query<TResult> query) where TResult : IResult
        {
            using (var scope = _hostFactory.Host.Services.CreateScope())
            {
                var cmsModule = scope.ServiceProvider.GetRequiredService<ICmsModule>();
                var result = await cmsModule.ExecuteQueryAsync(query);
                return result;
            }
        }

        //protected async Task<IResult<TResult>> GetAsync<TResult>(string requestUri, object payload)
        //{
        //    var client = _hostFactory.GetHttpClient();
        //    var payloadSerialized = JsonConvert.SerializeObject(payload);
        //    var content = new StringContent(payloadSerialized, Encoding.UTF8, "application/json");
        //    var request = new HttpRequestMessage
        //    {
        //        Method = HttpMethod.Get,
        //        RequestUri = new Uri(client.BaseAddress, requestUri),
        //        Content = content
        //    };
        //    var responseMessage = await client.SendAsync(request);
        //    return await DeerializeResponseContentAsync<TResult>(responseMessage);
        //}

        protected async Task<IResult<TResult>> DeerializeResponseContentAsync<TResult>(HttpResponseMessage responseMessage)
        {
            var contentString = await responseMessage.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result<TResult>>(contentString);
            return result;
        }

        public void Dispose()
        {
            Utilities.ReInializeDbForTestingAsync(_hostFactory.Host.Services).GetAwaiter().GetResult();
        }
    }
}
