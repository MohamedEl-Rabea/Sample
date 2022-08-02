using Moj.CMS.Domain.Aggregates.Claim.BusinessRules;
using Moj.CMS.Domain.Shared.Audit;
using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Domain.Shared.Guard;
using Moj.CMS.Domain.Shared.Values;

namespace Moj.CMS.Domain.Aggregates.Claim.Entities
{
    public class ClaimExtra : AuditedEntity
    {
        public Money Amount { get; private set; }
        public Money Remaining { get; private set; }
        public FinancialEffectTypeEnum Type { get; private set; }

        public static ClaimExtra Create(Money amount, FinancialEffectTypeEnum type)
        {
            Guard.AssertArgumentNotNull(amount, nameof(amount));
            Guard.AssertArgumentNotNull(type, nameof(type));

            var claimExtra = new ClaimExtra
            {
                Amount = amount,
                Remaining = amount.Clone(),
                Type = type
            };

            claimExtra.CheckRule(new ExtraTypeMustBeIncremental(type));

            return claimExtra;
        }
    }
}
