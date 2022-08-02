using Moj.CMS.Infrastructure.Contexts;
using Moj.CMS.Shared.Infrastructure.Seed;
using Moj.CMS.Shared.Models.Currency;
using System.Linq;
using System.Threading.Tasks;

namespace Moj.CMS.Infrastructure.Seed
{
    public class CurrencySeeder : IDatabaseSeeder
    {
        private readonly CMSDbContext _db;
        public CurrencySeeder(CMSDbContext db)
        {
            _db = db;
        }

        public async Task SeedAsync()
        {
            var currenciesData = await CurrenciesSeedHelper.GetCurrenciesDataAsync();
            var currencies = currenciesData.Select(c => new Currency
            {
                Code = c.iso.code,
                ExRate = 1,
                Name = c.name,
                IsLocal = CurrencyConstants.LocalCurrencyIso == c.iso.code,
            });

            await _db.Currency.AddRangeAsync(currencies.Where(inputCurrency => !_db.Currency.Select(dbCurrency => dbCurrency.Code).Contains(inputCurrency.Code)));
            await _db.SaveChangesAsync();
        }
    }
}
