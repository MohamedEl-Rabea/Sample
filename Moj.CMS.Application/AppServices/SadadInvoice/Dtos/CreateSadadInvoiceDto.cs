using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Domain.Shared.Values;
using System;

namespace Moj.CMS.Application.AppServices.SadadInvoice.Dtos
{
    public class CreateSadadInvoiceDto
    {
        public string InvoiceReferenceId { get; set; }
        public string PartyIdentityNumber { get; set; }
        public string PartyName { get; set; }
        public PartyIdentityTypeEnum PartyIdentityTypeId { get; set; }
        public decimal Amount { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Description { get; set; }
        public decimal MinBillableAmount { get; set; }
        public string Category { get; set; }
        public LocalizedText DisplayLabel { get; set; }
    }
}
