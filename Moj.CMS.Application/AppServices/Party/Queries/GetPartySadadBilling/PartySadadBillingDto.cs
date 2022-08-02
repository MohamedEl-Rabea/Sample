using Moj.CMS.Shared.DTO;
using System;

namespace Moj.CMS.Application.AppServices.Party.Queries.GetPartySadadBilling
{
    public class PartySadadBillingDto
    {
        public string CollectionChannel { get; set; }
        public string CollectionChannelType { get; set; }
        public string ReferenceNumber { get; set; }
        public DateTime PaymentNoticeDate { get; set; }
        public MoneyDto PaymentNoticeAmount { get; set; }
        public DateTime CachNoticeDate { get; set; }
        public MoneyDto CachNoticeAmount { get; set; }
        public MoneyDto CollectedAmount { get; set; }
        public DateTime SettlementDate { get; set; }
        public string SettlementResult { get; set; }
    }
}
