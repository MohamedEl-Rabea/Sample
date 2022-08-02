using Moj.CMS.Shared.DTO;
using Moj.CMS.Shared.Requests;
using System;
using System.Linq.Expressions;

namespace Moj.CMS.Application.AppServices.VIban.Queries.GetAllVIbans
{
    public class VIbanFilter : Filter<VIbanDto>
    {
        public string AccountNumber { get; set; }
        public string Alias { get; set; }
        public string ReferenceNumber { get; set; }
        public int? ReferenceTypeId { get; set; }
        public DateRangeDto IssueDateRange { get; set; }
        public override Expression<Func<VIbanDto, bool>> ToExpression()
        {
            Expression<Func<VIbanDto, bool>> expression = item =>
                (string.IsNullOrEmpty(AccountNumber) || item.AccountNumber == AccountNumber)
                && (string.IsNullOrEmpty(Alias) || item.Alias == Alias)
                && (string.IsNullOrEmpty(ReferenceNumber) || item.ReferenceNumber == (ReferenceNumber))
                && (ReferenceTypeId == null || (int)item.ReferenceType == ReferenceTypeId.Value)
                && (IssueDateRange == null || ((IssueDateRange.From == default || item.IssueDate >= IssueDateRange.From) &&
                                                                                 (IssueDateRange.To == null || item.IssueDate <= IssueDateRange.To)));
            return expression;
        }
    }
}
