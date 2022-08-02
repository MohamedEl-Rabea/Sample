using Moj.CMS.Application.AppServices.Case.Queries.GetCaseClaims;
using Moj.CMS.Shared.DTO;
using System.Collections.Generic;
using System.Linq;

namespace Moj.CMS.Application.AppServices.Case.Queries.GetCaseCollectionChannels
{
    public class CollectionChannelsDto
    {
        public MoneyDto TotalCollected => new MoneyDto
        {
            Value = AdjustmentChannels?.Sum(x => x.TotalAmount.Value) ?? 0,
            CurrencyIso = AdjustmentChannels?.FirstOrDefault()?.TotalAmount.CurrencyIso ?? ""
        };

        public IEnumerable<CaseClaimAdjustmentChannelDto> AdjustmentChannels { get; set; }
    }
}
