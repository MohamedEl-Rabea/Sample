using Moj.CMS.Domain.Aggregates.Case.Events;
using Moj.CMS.Domain.Aggregates.Claim.BusinessRules;
using Moj.CMS.Domain.Aggregates.Claim.Entities;
using Moj.CMS.Domain.Aggregates.Claim.Events;
using Moj.CMS.Domain.Aggregates.Claim.ValueObjects;
using Moj.CMS.Domain.Aggregates.Party.BusinessRules;
using Moj.CMS.Domain.BusinessRules;
using Moj.CMS.Domain.DomainServices.Party;
using Moj.CMS.Domain.ParameterObjects.Claim;
using Moj.CMS.Domain.Shared.Audit;
using Moj.CMS.Domain.Shared.Entities;
using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Domain.Shared.Guard;
using Moj.CMS.Domain.Shared.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moj.CMS.Domain.Aggregates.Claim
{
    public class Claim : FullAuditedAggregateRoot, IAggregateRoot
    {
        private Claim()
        {
            _claimDetailsList = new List<ClaimDetails>();
            _claimHistoryList = new List<ClaimHistory>();
            _extras = new List<ClaimExtra>();
        }

        public DateTime ClaimDateTime { get; private set; }
        public string CaseNumber { get; private set; }
        public string PromissoryNumber { get; private set; }
        public string ComplaintPartyNumber { get; private set; }
        public bool IsJoint { get; private set; }
        public Money BasicAmount { get; private set; }
        public Money RequiredAmount { get; private set; }
        public Money RemainingAmount { get; private set; }
        public Money TotalRequiredAmount { get; private set; }
        public Money TotalRemainingAmount { get; private set; }
        public ClaimStatus ClaimStatus { get; private set; }
        public ClaimCloseDetails CloseDetails { get; private set; }
        public DebtTypeEnum DebtTypeId { get; private set; }


        public IReadOnlyCollection<ClaimDetails> ClaimDetailsList => _claimDetailsList;
        private readonly List<ClaimDetails> _claimDetailsList;

        public IReadOnlyCollection<ClaimHistory> ClaimHistoryList => _claimHistoryList;
        private readonly List<ClaimHistory> _claimHistoryList;

        public IReadOnlyCollection<ClaimExtra> Extras => _extras;
        private readonly List<ClaimExtra> _extras;

        public static async Task<Claim> Create(CreateClaimParam param)
        {
            var claim = new Claim();
            await claim.SetClaimBasicInfoAsync(param);

            if (param.ShouldRegisterDomainEvent)
                claim.RegisterDomainEvent(new ClaimCreatedDomainEvent { CaseNumber = claim.CaseNumber, ClaimDetailsList = claim.ClaimDetailsList });

            return claim;
        }

        public void Close(string referenceNumber, DateTime closeDate)
        {
            if (ClaimStatus.Status != ClaimStatusEnum.Closed)
            {
                ClaimStatus = ClaimStatus.Close();
                CloseDetails = ClaimCloseDetails.Create(referenceNumber, closeDate);
                RegisterDomainEvent(new ClaimClosedDomainEvent { CaseNumber = CaseNumber });
            }
        }

        public async Task SetClaimBasicInfoAsync(CreateClaimParam param)
        {
            //Argument validations
            Guard.AssertArgumentNotNull(param.ClaimDate, nameof(param.ClaimDate));
            Guard.AssertArgumentNotNullOrEmptyOrWhitespace(param.CaseNumber, nameof(param.CaseNumber));
            Guard.AssertArgumentNotNullOrEmptyOrWhitespace(param.PromissoryNumber, nameof(param.PromissoryNumber));
            Guard.AssertArgumentNotNullOrEmptyOrWhitespace(param.ComplaintPartyNumber, nameof(param.ComplaintPartyNumber));
            Guard.AssertArgumentNotNull(param.BasicAmount, nameof(param.BasicAmount));
            Guard.AssertArgumentNotNull(param.RequiredAmount, nameof(param.RequiredAmount));
            Guard.AssertArgumentNotNull(param.RemainingAmount, nameof(param.RemainingAmount));

            //Busniess rules validations
            await CheckRuleAsync(new CaseExistBusniessRule(param.EnforceCaseIsFound, param.CaseNumber));

            ClaimDateTime = param.ClaimDate;
            CaseNumber = param.CaseNumber;
            DebtTypeId = param.DebtTypeId;
            PromissoryNumber = param.PromissoryNumber;
            IsJoint = param.IsJoint;
            ComplaintPartyNumber = param.ComplaintPartyNumber;
            ClaimStatus = ClaimStatus.Create(ClaimStatusEnum.Active, ClaimFinancialStatusEnum.NotPaid);
            BasicAmount = param.BasicAmount;
            SetAmounts(param.RequiredAmount, param.RemainingAmount);
            SetTotalAmounts(param.RequiredAmount.Clone(), param.RemainingAmount.Clone());
            await SetClaimDetailsAsync(param.ClaimDetailsList, param.EnforcePartiesAreAssignedToCase);
        }

        public async Task AddClaimDetailsAsync(IEnumerable<ClaimDetails> claimDetailsList, IGetCasePartiesNumbers getCasePartiesNumbers)
        {
            await SetClaimDetailsAsync(claimDetailsList, getCasePartiesNumbers);
            RegisterDomainEvent(new AccusedPartiesAddedToClaimDomainEvent { CaseNumber = CaseNumber, ClaimDetailsList = claimDetailsList });
        }

        private async Task SetClaimDetailsAsync(IEnumerable<ClaimDetails> claimDetailsList, IGetCasePartiesNumbers getCasePartiesNumbers)
        {
            var requestParties = claimDetailsList.Select(x => x.PartyNumber);
            var existingParties = _claimDetailsList.Select(x => x.PartyNumber);
            CheckRule(new ClaimDetailsHasNoDuplicatePartiesBusinessRule(requestParties, existingParties));
            await CheckRuleAsync(new PartiesMustBeAssignedToClaimCaseBusniessRule(CaseNumber, ComplaintPartyNumber, requestParties, getCasePartiesNumbers));

            _claimDetailsList.AddRange(claimDetailsList);
        }

        public void AddExtra(ClaimExtra extra)
        {
            Guard.AssertArgumentNotNull(extra, nameof(extra));

            var newTotalRequiredAmount = TotalRequiredAmount.Add(extra.Amount);
            var newTotalRemainingAmount = TotalRemainingAmount.Add(extra.Amount);


            _claimHistoryList.Add(ClaimHistory.Create(new ClaimHistoryParam
            {
                EffectType = extra.Type,
                TotalAmountBefore = TotalRequiredAmount,
                TotalAmountAfter = newTotalRequiredAmount,
            }));

            _extras.Add(extra);

            RegisterDomainEvent(new ClaimUpdatedDomainEvent
            {
                CaseNumber = CaseNumber,
                OldTotalRequiredAmount = TotalRequiredAmount,
                OldTotalRemainingAmount = TotalRemainingAmount
            });
            SetTotalAmounts(newTotalRequiredAmount, newTotalRemainingAmount);
            RefreshFinancialStatus();
        }

        public void Discount(Money effectAmount, FinancialEffectTypeEnum effectType)
        {
            Guard.AssertArgumentNotNull(effectType, nameof(effectType));
            Guard.AssertArgumentNotNull(effectAmount, nameof(effectAmount));

            CheckRule(new DiscountTypeMustBeDecremental(effectType));

            var newRequiredAmount = RequiredAmount.Subtract(effectAmount);
            var newRemainingAmount = RemainingAmount.Subtract(effectAmount);
            var newTotalRequiredAmount = TotalRequiredAmount.Subtract(effectAmount);
            var newTotalRemainingAmount = TotalRemainingAmount.Subtract(effectAmount);

            _claimHistoryList.Add(ClaimHistory.Create(new ClaimHistoryParam
            {
                EffectType = effectType,
                TotalAmountBefore = TotalRequiredAmount,
                TotalAmountAfter = newTotalRequiredAmount,
            }));

            RegisterDomainEvent(new ClaimUpdatedDomainEvent
            {
                CaseNumber = CaseNumber,
                OldTotalRequiredAmount = TotalRequiredAmount,
                OldTotalRemainingAmount = TotalRemainingAmount
            });
            SetAmounts(newRequiredAmount, newRemainingAmount);
            SetTotalAmounts(newTotalRequiredAmount, newTotalRemainingAmount);
            RefreshFinancialStatus();
        }

        private void SetAmounts(Money requiredAmount, Money remainingAmount)
        {
            RequiredAmount = requiredAmount;
            RemainingAmount = remainingAmount;
        }

        private void SetTotalAmounts(Money totalRequiredAmount, Money totalRemainingAmount)
        {
            TotalRequiredAmount ??= Money.Empty;
            TotalRemainingAmount ??= Money.Empty;
            var totalRequiredChanged = !TotalRequiredAmount.ValueEquals(totalRequiredAmount);
            var totalRemainingChanged = !TotalRemainingAmount.ValueEquals(totalRemainingAmount);
            if (totalRequiredChanged || totalRemainingChanged)
                RegisterDomainEvent(new ClaimAmountUpdatedDomainEvent { OldTotalRequiredAmount = TotalRequiredAmount, OldTotalRemainingAmount = TotalRemainingAmount });

            TotalRequiredAmount = totalRequiredAmount;
            TotalRemainingAmount = totalRemainingAmount;
        }

        private void RefreshFinancialStatus()
        {
            if (RemainingAmount.Value == 0)
            {
                ClaimStatus = ClaimStatus.Paid();
            }
            else if (RequiredAmount.Value == RemainingAmount.Value)
            {
                ClaimStatus = ClaimStatus.NotPaid();
            }
            else
            {
                ClaimStatus = ClaimStatus.PartiallyPaid();
            }

            RegisterDomainEvent(new ClaimStatusChangedDomainEvent());
        }
    }
}
