using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Shared.CustomAttributes;
using System;

namespace Moj.CMS.Application.AppServices.VIban.Queries.GetAllVIbans
{
    public class VIbanDto
    {
        [Exportable(Order = 1)]
        public string AccountNumber { get; set; }
        [Exportable(Order = 2)]
        public string Alias { get; set; }
        [Exportable(Order = 3)]
        public string ReferenceNumber { get; set; }
        [Exportable(Order = 4)]
        public VIbanReferenceTypeEnum ReferenceType { get; set; }
        [Exportable(Order = 5)]
        public DateTime IssueDate { get; set; }
        public bool IsActive { get; set; }
        [LocalizeValue]
        [Exportable(Order = 6)]
        public string AccountStatus => IsActive ? "Active" : "InActive";
    }
}
