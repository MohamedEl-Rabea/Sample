using System.Collections.Generic;

namespace Moj.CMS.Integration.Contracts.ThirdParties.Tahseel
{
    public class TahseelApiOptions
    {
        public const string SectionName = "TahseelApiOptions";

        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string BaseAddress { get; set; }
        public Dictionary<string, string> DefaultRequestHeaders { get; set; }
        public string GetRootDataLengthEndPointUri { get; set; }
    }
}
