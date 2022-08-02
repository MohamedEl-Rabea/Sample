using Moj.CMS.Domain.ParameterObjects.Claim;
using Moj.CMS.Domain.Shared.Audit;
using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Domain.Shared.Guard;
using Moj.CMS.Domain.Shared.Values;
using System;
using System.Collections.Generic;

namespace Moj.CMS.Domain.Aggregates.Claim.ValueObjects
{
    public class ClaimHistory : ValueObject, ICreationAudited
    {
        public Money TotalAmountBefore { get; private set; }
        public Money TotalAmountAfter { get; private set; }
        public FinancialEffectTypeEnum EffectType { get; private set; }
        public string CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }

        public static ClaimHistory Create(ClaimHistoryParam param)
        {
            Guard.AssertArgumentNotNull(param.TotalAmountAfter, nameof(param.TotalAmountAfter));
            Guard.AssertArgumentNotNull(param.TotalAmountBefore, nameof(param.TotalAmountBefore));
            Guard.AssertArgumentNotNull(param.EffectType, nameof(param.EffectType));

            var ClaimAdjustmentHistory = new ClaimHistory
            {
                TotalAmountBefore = param.TotalAmountBefore,
                TotalAmountAfter = param.TotalAmountAfter,
                EffectType = param.EffectType
            };
            return ClaimAdjustmentHistory;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return TotalAmountBefore;
            yield return TotalAmountAfter;
            yield return EffectType;
        }
    }
}
