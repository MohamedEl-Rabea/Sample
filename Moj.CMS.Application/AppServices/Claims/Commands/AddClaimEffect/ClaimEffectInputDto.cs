using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Shared.DTO;

namespace Moj.CMS.Application.AppServices.Claims.Commands.ClaimIncreaseDicrease
{
    public class ClaimEffectInputDto
    {
        public string ClaimNumber { get; set; }
        public FinancialEffectTypeEnum EffectType { get; set; }
        public MoneyDto EffectAmount { get; set; }
    }
}
