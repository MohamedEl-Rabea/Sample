using Moj.CMS.Shared.CustomAttributes;
using Moj.CMS.Shared.DTO;
using System;

namespace Moj.CMS.Application.AppServices.SadadInvoice.Queries
{
    public class SadadInvoiceDto: AuditedDto
    {
        [Exportable(Order = 1)]
        public string Number { get; set; }
        [Exportable(Order = 2)]
        public string Type { get; set; }
        [Exportable(Order = 3)]
        public string Description { get; set; }
        [Exportable(Order = 4)]
        public DateTime IssueDate { get; set; }
        [Exportable(Order = 5)]
        public DateTime ExpiryDate { get; set; }
        [Exportable(Order = 6)]
        public MoneyDto MinBillableAmount { get; set; }
        [Exportable(Order = 7)]
        public MoneyDto Amount { get; set; }
        [Exportable(Order = 8)]
        public MoneyDto PaidAmount { get; set; }
        [Exportable(Order = 9)]
        public MoneyDto RemainingAmount { get; set; }
        [Exportable(Order = 10)]
        public string PartyIdentityNumber { get; set; }
        [Exportable(Order = 11)]
        public string PartyName { get; set; }
        [Exportable(Order = 12)]
        public string VIban { get; set; }
        [Exportable(Order = 13)]
        public string ClaimNumber { get; set; }
        [Exportable(Order = 14)]
        public string CaseNumber { get; set; }
    }
}