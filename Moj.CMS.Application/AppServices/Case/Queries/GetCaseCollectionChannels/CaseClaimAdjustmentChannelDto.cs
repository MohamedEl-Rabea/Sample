using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Shared.DTO;
using System.Collections.Generic;
using System.Linq;

namespace Moj.CMS.Application.AppServices.Case.Queries.GetCaseClaims
{
    public class CaseClaimAdjustmentChannelDto
    {
        public MoneyDto TotalAmount => new MoneyDto
        {
            Value = AdjustmentDetails.Sum(x => x.OldAmount.Value - x.NewAmount.Value),
            CurrencyIso = AdjustmentDetails.FirstOrDefault()?.NewAmount.CurrencyIso ?? "",
        };
        public FinancialEffectTypeEnum AdjustmentReason { get; set; }
        public string AdjustmentReasonString { get; set; }
        public bool ShowDetails { get; set; }
        public IEnumerable<CollectionChannelsDetailsDto> AdjustmentDetails { get; set; }
    }
}