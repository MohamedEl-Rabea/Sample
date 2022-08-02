using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Shared.DTO;
using System;

namespace Moj.CMS.Application.AppServices.Case.Commands.AddCase
{
    public class CaseDetailsDto
    {
        public string CaseNumber { get; set; }
        public CaseTypeEnum CaseType { get; set; }
        public CaseStatusEnum CaseStatus { get; set; }
        public string CourtCode { get; set; }
        public string DivisionCode { get; set; }
        public string JudgeCode { get; set; }
        public DateTime ReceiveDate { get; set; }
        public DateTime JudgeAcceptanceDate { get; set; }
        public MoneyDto CaseBasicAmount { get; set; }
        public MoneyDto RequiredAmount { get; set; }
        public bool CreateVIban { get; set; }
    }
}