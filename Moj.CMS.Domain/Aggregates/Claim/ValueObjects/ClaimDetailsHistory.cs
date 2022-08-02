using Moj.CMS.Domain.ParameterObjects.Claim;
using Moj.CMS.Domain.Shared.Audit;
using Moj.CMS.Domain.Shared.Guard;
using Moj.CMS.Domain.Shared.Values;
using System;
using System.Collections.Generic;

namespace Moj.CMS.Domain.Aggregates.Claim.ValueObjects
{
    public class ClaimDetailsHistory : ValueObject, ICreationAudited
    {
        public string PartyNumber { get; private set; }
        public Money NewRequiredAmount { get; private set; }
        public Money NewBillingAmount { get; private set; }
        public Money OldRequiredAmount { get; private set; }
        public Money OldBillingAmount { get; private set; }

        public static ClaimDetailsHistory Create(ClaimDetailsHistoryParam param)
        {
            Guard.AssertArgumentNotNullOrEmptyOrWhitespace(param.PartyNumber, nameof(param.PartyNumber));
            Guard.AssertArgumentNotNull(param.NewBillingAmount, nameof(param.NewBillingAmount));
            Guard.AssertArgumentNotNull(param.OldBillingAmount, nameof(param.OldBillingAmount));
            Guard.AssertArgumentNotNull(param.NewRequiredAmount, nameof(param.NewRequiredAmount));
            Guard.AssertArgumentNotNull(param.OldRequiredAmount, nameof(param.OldRequiredAmount));

            var claimDetailsAdjustmentHistory = new ClaimDetailsHistory
            {
                PartyNumber = param.PartyNumber,
                NewBillingAmount = param.NewBillingAmount,
                OldBillingAmount = param.OldBillingAmount,
                NewRequiredAmount = param.NewRequiredAmount,
                OldRequiredAmount = param.OldRequiredAmount,
            };
            return claimDetailsAdjustmentHistory;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return PartyNumber;
            yield return NewBillingAmount;
            yield return OldBillingAmount;
            yield return NewRequiredAmount;
            yield return OldRequiredAmount;
        }

        public DateTime CreationTime { get; set; }
        public string CreatorUserId { get; set; }
    }
}
