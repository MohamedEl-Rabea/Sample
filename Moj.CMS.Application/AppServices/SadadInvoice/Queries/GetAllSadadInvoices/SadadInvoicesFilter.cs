using Moj.CMS.Shared.DTO;
using Moj.CMS.Shared.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.SadadInvoice.Queries.GetAllSadadInvoice
{
    public class SadadInvoiceFilter : Filter<SadadInvoiceDto>
    {
        public string Number { get; set; }
        public string PartyIdentityNumber { get; set; }
        public string PartyName { get; set; }
        public string PaymentChannelNumber { get; set; }
        public string ClaimNumber { get; set; }
        public string CaseNumber { get; set; }
        public DateRangeDto IssueDateRange { get; set; }
        public DateRangeDto ExpiryDateRange { get; set; }
        public NumberRangeDto MinimumPayment { get; set; }
        public NumberRangeDto Amount { get; set; }
        public NumberRangeDto PaidAmount { get; set; }
        public NumberRangeDto RemainingAmount { get; set; }
        public override Expression<Func<SadadInvoiceDto, bool>> ToExpression()
        {
            Expression<Func<SadadInvoiceDto, bool>> expression = SadadInvoiceListItem => (ClaimNumber == null || SadadInvoiceListItem.ClaimNumber == ClaimNumber)
             && (string.IsNullOrEmpty(Number) || SadadInvoiceListItem.Number == Number)
             && (string.IsNullOrEmpty(PartyIdentityNumber) || SadadInvoiceListItem.PartyIdentityNumber == PartyIdentityNumber)
             && (string.IsNullOrEmpty(PartyName) || SadadInvoiceListItem.PartyName == PartyName)
             && (string.IsNullOrEmpty(PaymentChannelNumber) || SadadInvoiceListItem.VIban == PaymentChannelNumber)
             && (string.IsNullOrEmpty(ClaimNumber) || SadadInvoiceListItem.ClaimNumber == ClaimNumber)
             && (string.IsNullOrEmpty(CaseNumber) || SadadInvoiceListItem.CaseNumber == CaseNumber)
             && (IssueDateRange == null || IssueDateRange.From == default || (SadadInvoiceListItem.IssueDate >= IssueDateRange.From
                                          && (IssueDateRange.To == null || SadadInvoiceListItem.IssueDate <= IssueDateRange.To)))
             && (ExpiryDateRange == null || ExpiryDateRange.From == default || (SadadInvoiceListItem.ExpiryDate >= ExpiryDateRange.From
                                           && (ExpiryDateRange.To == null || SadadInvoiceListItem.ExpiryDate <= ExpiryDateRange.To)))
             && (MinimumPayment == null || ((MinimumPayment.Min == default || SadadInvoiceListItem.MinBillableAmount.Value >= MinimumPayment.Min)
                                          && (MinimumPayment.Max == null || SadadInvoiceListItem.MinBillableAmount.Value <= MinimumPayment.Max)))
             && (Amount == null || ((Amount.Min == default || SadadInvoiceListItem.Amount.Value >= Amount.Min)
                                          && (Amount.Max == null || SadadInvoiceListItem.Amount.Value <= Amount.Max)))
             && (PaidAmount == null || ((PaidAmount.Min == default || SadadInvoiceListItem.PaidAmount.Value >= PaidAmount.Min)
                                          && (PaidAmount.Max == null || SadadInvoiceListItem.PaidAmount.Value <= PaidAmount.Max)))
             && (RemainingAmount == null || ((RemainingAmount.Min == default || SadadInvoiceListItem.RemainingAmount.Value >= RemainingAmount.Min)
                                          && (RemainingAmount.Max == null || SadadInvoiceListItem.RemainingAmount.Value <= RemainingAmount.Max)));
            return expression;
        }
    }
}
