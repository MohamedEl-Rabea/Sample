using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Shared.DTO;
using Moj.CMS.Shared.Requests;
using System;
using System.Linq.Expressions;

namespace Moj.CMS.Application.AppServices.Promissory.Queries
{
    public class PromissoryFilter : Filter<GetAllPromissoriesDto>
    {
        public string Number { get; set; }
        public int? TypeId { get; set; }
        public DateRangeDto PromissoryDateRange { get; set; }


        public override Expression<Func<GetAllPromissoriesDto, bool>> ToExpression()
        {
            Expression<Func<GetAllPromissoriesDto, bool>> expression = promissoryListItem =>
            (TypeId == null || promissoryListItem.TypeId == (PromissoryTypeEnum)TypeId)
            && (PromissoryDateRange == null || PromissoryDateRange.From == default ||
            (promissoryListItem.PromissoryDate >= PromissoryDateRange.From && (PromissoryDateRange.To == null || promissoryListItem.PromissoryDate <= PromissoryDateRange.To)))
            && (string.IsNullOrEmpty(Number) || promissoryListItem.PromissoryNumber == Number);

            return expression;
        }
    }
}
