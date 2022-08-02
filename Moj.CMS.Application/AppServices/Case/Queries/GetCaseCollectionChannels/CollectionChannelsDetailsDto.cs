using Moj.CMS.Shared.DTO;
using System;

namespace Moj.CMS.Application.AppServices.Case.Queries.GetCaseClaims
{
    public class CollectionChannelsDetailsDto
    {
        public string ClaimNumber { get; set; }
        public MoneyDto OldAmount { get; set; }
        public MoneyDto NewAmount { get; set; }
        public MoneyDto ReportAmount => OldAmount.Subtract(NewAmount);
        public DateTime ReportDate { get; set; }
    }
}
