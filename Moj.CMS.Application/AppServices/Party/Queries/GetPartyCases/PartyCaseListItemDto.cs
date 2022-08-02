using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Shared.CustomAttributes;
using Moj.CMS.Shared.DTO;
using System;

namespace Moj.CMS.Application.AppServices.Party.Queries
{
    public class PartyCaseListItemDto : AuditedDto
    {
        public int CaseId { get; set; }
        public int ClaimId { get; set; }

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

        [Exportable(Order = 13)]
        public MoneyDto CaseBasicAmount { get; set; }

        [Exportable(Order = 14)]
        public MoneyDto ApprovedAmount { get; set; }

        [Exportable(Order = 15)]
        public MoneyDto TotalCollectedAmount => TotalRequiredAmount.Subtract(TotalRemainingAmount);

        [Exportable(Order = 16)]
        public string PartyNumber { get; set; }
        [Exportable(Order = 17)]
        public MoneyDto TotalPartyCaseComplaintAmount { get; set; }
        [Exportable(Order = 18)]
        public MoneyDto TotalPartyCaseAccuesedAmount { get; set; }
        [Exportable(Order = 19)]
        public MoneyDto TotalRemainingPartyCaseAccuesedAmount { get; set; }
        [Exportable(Order = 20)]
        public MoneyDto TotalRemainingPartyCaseComplaintAmount { get; set; }
        [Exportable(Order = 21)]
        public MoneyDto TotalPaidPartyCaseComplaintAmount { get; set; }
        [Exportable(Order = 22)]
        public PartyClassificationEnum PartyClassification { get; set; }
    }
}