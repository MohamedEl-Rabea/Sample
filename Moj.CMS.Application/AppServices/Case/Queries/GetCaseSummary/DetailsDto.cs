using Moj.CMS.Domain.Shared.Enums;
using System;

namespace Moj.CMS.Application.AppServices.Case.Queries.GetCaseSummary
{
    public class DetailsDto
    {
        public string CaseNumber { get; set; }
        public int TypeId { get; set; }

        public string CaseType { get; set; }
        public int StatusId { get; set; }
        public string CaseStatus { get; set; }
        public DateTime ReceiveDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public TimeSpan CaseAge => StatusId == (int)CaseStatusEnum.Closed ? Convert.ToDateTime(CloseDate).Subtract(ReceiveDate) : DateTime.Now.Subtract(ReceiveDate);
        public int PartiesCount { get; set; }
        public int PromissoriesCount { get; set; }
        public int BankAccountsCount { get; set; }
        public int CaseEventsCount { get; set; }
    }
}