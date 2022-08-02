using Moj.CMS.Shared.CustomAttributes;
using Moj.CMS.Shared.DTO;
using System;
namespace Moj.CMS.Application.AppServices.Claims.Queries
{
    public class GetAllClaimsDto : AuditedDto
    {
        [Exportable(Order = 1)]
        public string ClaimNumber { get; set; }
        [Exportable(Order = 2)]
        public string CaseNumber { get; set; }
        [Exportable(Order = 3)]
        public string PartyIdentityNumber { get; set; }
        [Exportable(Order = 4)]
        public string CourtName { get; set; }
        [Exportable(Order = 5)]
        public string DivisionName { get; set; }
        [Exportable(Order = 6)]
        public string JudgeName { get; set; }
        
        [Exportable(Order = 8)]
        public MoneyDto RequiredAmount { get; set; }
        [Exportable(Order = 9)]
        public MoneyDto RemainingAmount { get; set; }
        [Exportable(Order = 10)]
        public string StatusName { get; set; }
        public int StatusId { get; set; }
        public int ClaimId { get; set; }
        
        [Exportable(Order = 7)]
        public DateTime ClaimDateTime { get; set; }
        public int CaseId { get; set; }
        public string JudgeCode { get; set; }
        public string CourtCode { get; set; }
        public string DivisionCode { get; set; }
    }
}
