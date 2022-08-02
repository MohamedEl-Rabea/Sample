using Moj.CMS.Domain.ParameterObjects.Claim;
using Moj.CMS.Domain.Shared.Audit;
using Moj.CMS.Domain.Shared.Guard;
using Moj.CMS.Domain.Shared.Values;
using System.Collections.Generic;

namespace Moj.CMS.Domain.Aggregates.Claim.ValueObjects
{
    public class ClaimDetails : FullAuditedEntity
    {
        public string PartyNumber { get; private set; }
        public Money RequiredAmount { get; private set; }
        public Money BillingAmount { get; private set; }

        public IReadOnlyCollection<ClaimDetailsHistory> ClaimDetailsHistoryList => _claimDetailsHistoryList;
        private readonly List<ClaimDetailsHistory> _claimDetailsHistoryList;

        private ClaimDetails()
        {
            _claimDetailsHistoryList = new List<ClaimDetailsHistory>();
        }

        private void SetRequiredAmount(Money NewAmount)
        {
            Guard.AssertArgumentNotNull(NewAmount, nameof(NewAmount));
            RequiredAmount = NewAmount;
        }

        private void SetBillingAmount(Money NewAmount)
        {
            Guard.AssertArgumentNotNull(NewAmount, nameof(NewAmount));
            BillingAmount = NewAmount;
        }
        public static ClaimDetails Create(string PartyNumber, Money RequiredAmount, Money BillingAmount)
        {
            var ClaimDetails = new ClaimDetails();
            ClaimDetails.SetClaimDetailsBasicInfo(PartyNumber, RequiredAmount, BillingAmount);
            return ClaimDetails;
        }

        public void SetClaimDetailsBasicInfo(string partyNumber, Money requiredAmount, Money billingAmount)
        {
            Guard.AssertArgumentNotNullOrEmptyOrWhitespace(partyNumber, nameof(partyNumber));

            PartyNumber = partyNumber;
            SetRequiredAmount(requiredAmount);
            SetBillingAmount(billingAmount);
        }

        public void AddHistory(ClaimDetails newValues)
        {
            _claimDetailsHistoryList.Add(ClaimDetailsHistory.Create(new ClaimDetailsHistoryParam
            {
                PartyNumber = newValues.PartyNumber,
                NewRequiredAmount = newValues.RequiredAmount,
                OldRequiredAmount = RequiredAmount,
                NewBillingAmount = newValues.BillingAmount,
                OldBillingAmount = BillingAmount,
            }));
        }

        public void AdjustClaimDetailsAmounts(ClaimDetails claimDetails)
        {
            AddHistory(claimDetails);
            SetRequiredAmount(claimDetails.RequiredAmount);
            SetBillingAmount(claimDetails.BillingAmount);
        }
    }
}
