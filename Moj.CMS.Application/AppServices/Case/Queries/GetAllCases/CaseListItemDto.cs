using Moj.CMS.Shared.CustomAttributes;
using Moj.CMS.Shared.DTO;
using System;

namespace Moj.CMS.Application.AppServices.Case.Queries.GetAllCases
{
    public class CaseListItemDto : AuditedDto
    {
        public int Id { get; set; }

        [Exportable(Order = 1)]
        public string CaseNumber { get; set; }

        [Exportable(Order = 4)]
        public string Court { get; set; }
        public string CourtCode { get; set; }

        [Exportable(Order = 5)]
        public string Division { get; set; }
        public string DivisionCode { get; set; }

        [Exportable(Order = 6)]
        public string Judge { get; set; }
        public string JudgeCode { get; set; }

        [Exportable(Order = 2)]
        public string CaseType { get; set; }
        public int CaseTypeId { get; set; }

        [Exportable(Order = 3)]
        public string CaseStatus { get; set; }
        public int CaseStatusId { get; set; }

        [Exportable(Order = 7)]
        public DateTime ReceiveDate { get; set; }

        [Exportable(Order = 8)]
        public DateTime JudgeAcceptanceDate { get; set; }

        [Exportable(Order = 9)]
        public int RequestersCount { get; set; }

        [Exportable(Order = 10)]
        public int RespondentsCount { get; set; }

        [Exportable(Order = 11)]
        public MoneyDto TotalRequiredAmount { get; set; }

        [Exportable(Order = 12)]
        public MoneyDto TotalRemainingAmount { get; set; }
    }
}