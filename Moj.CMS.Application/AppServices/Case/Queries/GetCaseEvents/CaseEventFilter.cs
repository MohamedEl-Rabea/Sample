using Moj.CMS.Shared.DTO;
using Moj.CMS.Shared.Requests;
using System;
using System.Linq.Expressions;

namespace Moj.CMS.Application.AppServices.Case.Queries.GetCaseEvents
{
    public class CaseEventFilter : IFilter<CaseEventsDto>
    {
        public DateRangeDto DateRange { get; set; }
        public int? OperationId { get; set; }
        public string UserName { get; set; }

        public Expression<Func<CaseEventsDto, bool>> ToExpression()
        {

            Expression<Func<CaseEventsDto, bool>> expression = caseEventItem =>
                (DateRange == null || (caseEventItem.Date.Date >= DateRange.From &&
                                              (DateRange.To == null || caseEventItem.Date.Date <= DateRange.To))) &&
                (OperationId == null || caseEventItem.OperationId == OperationId.Value) &&
                (string.IsNullOrEmpty(UserName) || caseEventItem.UserName.Contains(UserName));

            return expression;
        }
    }
}
