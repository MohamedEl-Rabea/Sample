using Moj.CMS.Application.AppServices.Case.Queries.GetAllCases;
using Moj.CMS.Shared.DTO;
using Moj.CMS.Shared.Requests;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Moj.CMS.Application.AppServices.Case.Queries
{
    public class CaseFilter : Filter<CaseListItemDto>
    {
        public string[] CaseNumbers { get; set; }
        public string CourtCode { get; set; }
        public string DivisionCode { get; set; }
        public string JudgeCode { get; set; }
        public int? CaseStatusId { get; set; }
        public int? CaseTypeId { get; set; }
        public DateRangeDto ReceiveDateRange { get; set; }
        public DateRangeDto JudgeAcceptanceDateRange { get; set; }
        public NumberRangeDto TotalRequiredAmountRange { get; set; }

        public override Expression<Func<CaseListItemDto, bool>> ToExpression()
        {
            Expression<Func<CaseListItemDto, bool>> expression = item =>
            (CaseNumbers == null || CaseNumbers.Length == 0 || CaseNumbers.Contains(item.CaseNumber))
            && (string.IsNullOrEmpty(CourtCode) || item.CourtCode == CourtCode)
            && (string.IsNullOrEmpty(DivisionCode) || item.DivisionCode == DivisionCode)
            && (string.IsNullOrEmpty(JudgeCode) || item.JudgeCode == (JudgeCode))
            && (CaseStatusId == null || item.CaseStatusId == CaseStatusId.Value)
            && (CaseTypeId == null || item.CaseTypeId == CaseTypeId.Value)
            && (TotalRequiredAmountRange == null || ((!TotalRequiredAmountRange.Min.HasValue || item.TotalRequiredAmount.Value >= TotalRequiredAmountRange.Min)
                                                        && (!TotalRequiredAmountRange.Max.HasValue || item.TotalRequiredAmount.Value <= TotalRequiredAmountRange.Max)))
            && (ReceiveDateRange == null || ReceiveDateRange.From == default || (item.ReceiveDate >= ReceiveDateRange.From &&
                                                    (ReceiveDateRange.To == null || item.ReceiveDate <= ReceiveDateRange.To)))
            && (JudgeAcceptanceDateRange == null || JudgeAcceptanceDateRange.From == default ||
                (item.JudgeAcceptanceDate >= JudgeAcceptanceDateRange.From &&
                                                    (JudgeAcceptanceDateRange.To == null || item.JudgeAcceptanceDate <= JudgeAcceptanceDateRange.To)));
            return expression;
        }
    }
}
