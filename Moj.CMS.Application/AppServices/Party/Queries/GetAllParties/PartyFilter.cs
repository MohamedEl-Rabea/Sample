using Moj.CMS.Shared.DTO;
using Moj.CMS.Shared.Requests;
using System;
using System.Linq.Expressions;

namespace Moj.CMS.Application.AppServices.Party.Queries
{
    public class PartyFilter : Filter<PartyListItemDto>
    {
        public int? PartyTypeId { get; set; }
        public string IdentityNumber { get; set; }
        public int? IdentityTypeId { get; set; }
        public string NationalityCode { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public NumberRangeDto TotalCreditAmount { get; set; }
        public NumberRangeDto TotalDebtAmount { get; set; }

        public override Expression<Func<PartyListItemDto, bool>> ToExpression()
        {
            Expression<Func<PartyListItemDto, bool>> expression = PartyListItem =>
            (IdentityTypeId == null || IdentityTypeId == 0 || PartyListItem.IdentityTypeId == IdentityTypeId.Value)
            && (PartyTypeId == null || PartyTypeId == 0 || PartyListItem.PartyTypeId == PartyTypeId.Value)
            && (string.IsNullOrEmpty(NationalityCode) || PartyListItem.NationalityCode == NationalityCode)
            && (string.IsNullOrEmpty(Number) || PartyListItem.Number.Contains(Number))
            && (string.IsNullOrEmpty(IdentityNumber) || PartyListItem.IdentityNumber.Contains(IdentityNumber))
            && (string.IsNullOrEmpty(Name) || PartyListItem.Name.Contains(Name))
            && (TotalCreditAmount == null || ((TotalCreditAmount.Min == null || TotalCreditAmount.Min.Value == 0M || PartyListItem.TotalCreditAmountValue >= TotalCreditAmount.Min)
                                          && (TotalCreditAmount.Max == null || TotalCreditAmount.Max.Value == 0M || PartyListItem.TotalCreditAmountValue <= TotalCreditAmount.Max)))
            && (TotalDebtAmount == null || ((TotalDebtAmount.Min == null || TotalDebtAmount.Min.Value == 0M || PartyListItem.TotalDebtAmountValue >= TotalDebtAmount.Min)
                                        && (TotalDebtAmount.Max == null || TotalDebtAmount.Max.Value == 0M || PartyListItem.TotalDebtAmountValue <= TotalDebtAmount.Max))); ;

            return expression;
        }
    }
}
