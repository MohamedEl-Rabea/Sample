using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Moj.CMS.Shared.Models.Currency
{
    public static class CurrenciesSeedHelper
    {
        public static async Task<List<CurrencyJsonData>> GetCurrenciesDataAsync()
        {
            var currentExecutingAssembly = Assembly.GetExecutingAssembly();
            var resourcePath = currentExecutingAssembly.GetManifestResourceNames().FirstOrDefault(r => r.EndsWith("CurrenciesData.json"));
            var content = "";

            await using (Stream stream = currentExecutingAssembly.GetManifestResourceStream(resourcePath))
            {
                using StreamReader reader = new StreamReader(stream);
                content = await reader.ReadToEndAsync();
            }
            return JsonConvert.DeserializeObject<List<CurrencyJsonData>>(content);
        }
    }

    public class CurrencyJsonData
    {
        public string name { get; set; }
        public CurrencyISO iso { get; set; }
        public Unit units { get; set; }
    }

    public class CurrencyISO
    {
        public string code { get; set; }
        public string number { get; set; }
    }

    public class Unit
    {
        public Unit_Major major { get; set; }
        public Unit_Minor minor { get; set; }
    }

    public class Unit_Major
    {
        public string name { get; set; }
        public string symbol { get; set; }
    }

    public class Unit_Minor
    {
        public string name { get; set; }
        public string symbol { get; set; }
        public string majorValue { get; set; }
    }
}

