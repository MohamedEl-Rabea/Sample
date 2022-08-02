using Moj.CMS.Domain.DomainServices.SadadInvoice;
using Moj.CMS.Domain.Shared.Values;
using System;

namespace Moj.CMS.Domain.ParameterObjects.SadadInvoice
{
    public class CreateSadadInvoiceParam
    {
        public string Number { get; set; }
        public string ClaimNumber { get; set; }
        public string PartyNumber { get; set; }
        public Money Amount { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Description { get; set; }
        public Money MinBillableAmount { get; set; }
        public IClaimInfoProvider ClaimInfoProvider { get; set; }
    }
}
