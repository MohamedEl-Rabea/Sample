using Moj.CMS.Shared.DTO;
using Moj.CMS.Shared.Requests;
using System;
using System.Linq.Expressions;

namespace Moj.CMS.Application.AppServices.Claims.Queries
{
    public class ClaimFilter : Filter<GetAllClaimsDto>
    {
        public string PartyIdentityNumber { get; set; }
        public string ClaimNumber { get; set; }
        public DateRangeDto ClaimDateRange { get; set; }
        public string CaseNumber { get; set; }
        public string JudgeCode { get; set; }
        public string CourtCode { get; set; }
        public string DivisionCode { get; set; }
        public int? StatusId { get; set; }
        public decimal? RequiredAmount { get; set; }

        public override Expression<Func<GetAllClaimsDto, bool>> ToExpression()
        {
            Expression<Func<GetAllClaimsDto, bool>> expression = claimsListItem => (ClaimNumber == null || claimsListItem.ClaimNumber == ClaimNumber)
             && (string.IsNullOrEmpty(CaseNumber) || claimsListItem.CaseNumber.Contains(CaseNumber))
             && (StatusId == null || claimsListItem.StatusId == StatusId)
             && (string.IsNullOrEmpty(PartyIdentityNumber) || claimsListItem.PartyIdentityNumber == PartyIdentityNumber)
             && (string.IsNullOrEmpty(JudgeCode) || claimsListItem.JudgeCode == JudgeCode)
             && (string.IsNullOrEmpty(CourtCode) || claimsListItem.CourtCode == CourtCode)
             && (string.IsNullOrEmpty(DivisionCode) || claimsListItem.DivisionCode == DivisionCode)
             && (!RequiredAmount.HasValue || claimsListItem.RequiredAmount.Value <= RequiredAmount)
             && (ClaimDateRange == null || ClaimDateRange.From == default || (claimsListItem.ClaimDateTime >= ClaimDateRange.From &&
                                                    (ClaimDateRange.To == null || claimsListItem.ClaimDateTime <= ClaimDateRange.To)));
            return expression;
        }
    }
}
