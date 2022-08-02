using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Moj.CMS.Integration.Contracts.AlAhli_B2B
{
    public class AlahliClient : IAlahliClient
    {
        private readonly HttpClient _httpClient;
        private readonly AlahliApiOptions _apiOptions;

        public AlahliClient(HttpClient httpClient, AlahliApiOptions apiOptions)
        {
            _httpClient = httpClient;
            _apiOptions = apiOptions;
        }

        public async Task<string> CreateVIban(VIbanCreationRequest vIbanData)
        {
            var timeStamp = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            var payloadSerialized = "{\"name\":\"test_" + $"{ timeStamp }" 
                + "\",\"gender\":\"male\",\"email\":\"test_" + $"{ timeStamp }"
                + "@test.com\",\"status\":\"active\"}";
            var content = new StringContent(payloadSerialized, Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync(_apiOptions.CreateVIbanAPI, content);
            return result.Headers.Location.AbsoluteUri;
        }
    }
}
