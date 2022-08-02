using System.Net.Http;

namespace Moj.CMS.Integration.ClientGenerator.Services.TahseelService
{
    public partial class TahseelService
    {
        public TahseelService(string baseUrl, HttpClient httpClient) : this(httpClient)
        {
            BaseUrl = baseUrl;
        }
    }
}
