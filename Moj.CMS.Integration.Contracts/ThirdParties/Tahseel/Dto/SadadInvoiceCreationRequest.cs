using System;
using System.ComponentModel;

namespace Moj.CMS.Integration.Contracts.ThirdParties.Tahseel.Dto
{
    public class SadadInvoiceCreationRequest
    {
        public string InvoiceReferenceId { get; set; }
        public decimal Amount { get; set; }
        public string Category { get; set; }
        public decimal MinBillableAmount { get; set; }
        public string Description { get; set; }
        public string PartyName { get; set; }
        public string PartyIdentityNumber { get; set; }
        public BenefeciaryIdentityType PartyIdentityTypeId { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime DueDate { get; set; }
        public string DisplayLabelAr { get; set; }
        public string DisplayLabelEn { get; set; }
    }

    public enum BenefeciaryIdentityType
    {
        [Description("NAT")]
        NationalId,

        [Description("IQA")]
        Iqama,

        [Description("BIS")]
        BusinessID,

        [Description("UOI")]
        UnifiedOrganizationID,

        [Description("C700")]
        Code700,

        [Description("GCC")]
        GCCPassportNumber,

        [Description("PAS")]
        PassportNumber,

        [Description("BDN")]
        BorderNumber,

        [Description("FCN")]
        FamilyCardNumber
    }
}
