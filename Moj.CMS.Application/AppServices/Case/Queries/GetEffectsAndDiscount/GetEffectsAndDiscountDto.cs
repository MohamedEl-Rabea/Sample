using Moj.CMS.Shared.DTO;
using System;

namespace Moj.CMS.Application.AppServices.Case.Queries.GetEffectsAndDiscount
{
    public class GetEffectsAndDiscountDto
    {
        public string ClaimNumber { get; set; }
        public MoneyDto OldTotalAmount { get;set; }
        public MoneyDto NewTotalAmount { get; set; }
        public MoneyDto EffectAmount { get;  set; }
        public string EffectType { get; set; }
        public int EffectTypeId { get; set; }
        public string CreatorUser { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
