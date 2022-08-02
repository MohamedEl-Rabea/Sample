using Moj.CMS.Domain.Shared.Values;
using System;

namespace Moj.CMS.Domain.ParameterObjects.VIban
{
    public class CreateVIbanParam
    {
        public string VIbanNumber { get; set; }
        public decimal CAP { get; set; }
        public string Alias { get; set; }
        public string BankName { get; set; }
        public string ParentAccountNumber { get; set; }
        public DateTime IssueDate { get; set; }
        public VIbanReferenceDetails ReferenceDetails { get; set; }
    }
}