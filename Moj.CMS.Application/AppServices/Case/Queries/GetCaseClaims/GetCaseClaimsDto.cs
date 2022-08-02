using Moj.CMS.Shared.DTO;
using System.Collections.Generic;
using System.Linq;

namespace Moj.CMS.Application.AppServices.Case.Queries.GetCaseClaims
{

    public class GetCaseClaimsDto
    {
        public MoneyDto TotalRequired => new MoneyDto
        {
            Value = ClaimList.Sum(x => x.RequiredAmount.Value),
            CurrencyIso = ClaimList.FirstOrDefault()?.RequiredAmount.CurrencyIso ?? ""
        };

        public MoneyDto TotalCollected => new MoneyDto
        {
            Value = TotalRequired.Value - TotalRemaining.Value,
            CurrencyIso = TotalRequired.CurrencyIso
        };

        public MoneyDto TotalRemaining => new MoneyDto
        {
            Value = ClaimList.Sum(x => x.RemainingAmount.Value),
            CurrencyIso = ClaimList.FirstOrDefault()?.RemainingAmount.CurrencyIso ?? ""
        };
        public List<ClaimDto> ClaimList { get; set; }
    }
}
