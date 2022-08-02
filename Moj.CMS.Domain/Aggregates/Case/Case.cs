using Moj.CMS.Domain.Aggregates.Case.BusinessRules;
using Moj.CMS.Domain.Aggregates.Case.Events;
using Moj.CMS.Domain.Aggregates.Case.ValueObjects;
using Moj.CMS.Domain.ParameterObjects.Case;
using Moj.CMS.Domain.Shared.Audit;
using Moj.CMS.Domain.Shared.Entities;
using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Domain.Shared.Guard;
using Moj.CMS.Domain.Shared.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moj.CMS.Domain.Aggregates.Case
{
    public class Case : FullAuditedAggregateRoot, IAggregateRoot
    {
        private Case()
        {
            _caseDetails = new List<CaseDetails>();
            _caseParties = new List<CaseParty>();
            _casePromissories = new List<CasePromissory>();
        }

        public string CaseNumber { get; private set; }
        public CaseTypeEnum CaseType { get; private set; }
        public CaseStatusEnum CaseStatus { get; private set; }
        public CaseDate DatesInfo { get; private set; }
        public DateTime? CloseDate { get; private set; }
        public Money CaseBasicAmount { get; private set; }
        public Money ApprovedAmount { get; private set; }
        public Money TotalRequiredAmount { get; private set; }
        public Money TotalRemainingAmount { get; private set; }

        public IReadOnlyCollection<CaseDetails> CaseDetails => _caseDetails.ToList();
        private readonly List<CaseDetails> _caseDetails;

        public IReadOnlyCollection<CaseParty> CaseParties => _caseParties.ToList();
        private readonly List<CaseParty> _caseParties;

        public IReadOnlyCollection<CasePromissory> CasePromissories => _casePromissories.ToList();
        private readonly List<CasePromissory> _casePromissories;

        public static async Task<Case> CreateAsync(AddNewCaseParameter input)
        {
            var newCase = new Case();
            await newCase.SetBasicInfoAsync(input);
            await newCase.AddCourtDetailsAsync(input.CaseDetailsParam);
            newCase.AddPromissories(input.CasePromissories);
            newCase.AddCaseParties(input.CaseParties);
            newCase.RegisterDomainEvent(new CaseCreatedDomainEvent
            {
                CaseNumber = newCase.CaseNumber,
                CaseParties = input.CaseParties,
                CasePromissories = input.CasePromissories
            });
            return newCase;
        }

        public async Task UpdateCaseInfoAsync(UpdateCaseParameter input)
        {
            await SetBasicInfoAsync(input);
            await AddCourtDetailsAsync(input.CaseDetailsParam);
        }

        private async Task SetBasicInfoAsync(CaseParameterBasicInfo input)
        {
            //Arguments validations
            Guard.AssertArgumentNotIncludeWhitespaces(input.CaseNumber, nameof(input.CaseNumber));

            //Busniess rules check
            await CheckRuleAsync(new NoDuplicateCaseNumberAllowedBusniessRule(input.EnforceCaseNumberIsUnique, input.CaseNumber, Id));

            CaseNumber = input.CaseNumber;
            CaseStatus = input.CaseStatusId;
            CaseType = input.CaseTypeId;
            DatesInfo = input.DatesInfo;
            CaseBasicAmount = input.CaseBasicAmount;
            ApprovedAmount = input.RequiredAmount;
        }

        public void Terminate(int reasonId)
        {
            Guard.AssertEnumValueIsValid(reasonId, typeof(RequestTerminationReasonsEnum), nameof(reasonId));
            CaseStatus = CaseStatusEnum.Closed;
        }

        public void Close()
        {
            CaseStatus = CaseStatusEnum.Closed;
            CloseDate = DateTime.Now;
        }

        private void AddCaseParties(IEnumerable<CaseParty> caseParties)
        {
            _caseParties.AddRange(caseParties);

            CheckRule(new CasePartiesHasNoDupplicatesBusinessRule(_caseParties));
        }

        public void AssignParties(IEnumerable<CaseParty> caseParties)
        {
            AddCaseParties(caseParties);
            if (caseParties.Any())
                RegisterDomainEvent(new CasePartyCreatedDomainEvent
                {
                    CaseNumber = CaseNumber,
                    CaseParties = caseParties
                });
        }

        private void AddPromissories(IEnumerable<CasePromissory> casePromissories)
        {
            _casePromissories.AddRange(casePromissories);
            CheckRule(new CasePromissoriesHasNoDupplicatesBusinessRule(CaseNumber, _casePromissories));
        }

        public void AssignPromissories(IEnumerable<CasePromissory> casePromissories)
        {
            AddPromissories(casePromissories);
            if (casePromissories.Any())
                RegisterDomainEvent(new CasePromissoryAddedDomainEvent
                {
                    CasePromissories = casePromissories,
                    CaseNumber = CaseNumber
                });
        }
        public void RemoveParties(IEnumerable<string> casePartiesNumbers)
        {
            _caseParties.RemoveAll(s => casePartiesNumbers.Contains(s.PartyNumber));
        }

        public async Task AddCourtDetailsAsync(CaseDetailsParam detialsParam)
        {
            var caseDetails = detialsParam.CaseDetails;

            if (!_caseDetails.Any(c => c == caseDetails))
            {
                await CheckRuleAsync(new JudgeMustBeExistsBusniessRule(caseDetails.JudgeCode, detialsParam.EnforceJudgeIsExists));
                await CheckRuleAsync(new CourtDetailsMustBeValidBusniessRule(caseDetails.CourtCode, caseDetails.DivisionCode, detialsParam.EnforceCourtIsExists, detialsParam.GetDivisionCourtCode));
                _caseDetails.Add(caseDetails);
            }
        }

        public async Task UpdateCourtDetailsAsync(CaseDetailsParam detialsParam)
        {
            var toBeDeactivatedCourt = _caseDetails.Single(d => d.IsCurrent);
            if (toBeDeactivatedCourt.Equals(detialsParam.CaseDetails))
            {
                return;
            }
            else
            {
                _caseDetails.Remove(toBeDeactivatedCourt);
                toBeDeactivatedCourt = toBeDeactivatedCourt.Moved();
                _caseDetails.Add(toBeDeactivatedCourt);

                await AddCourtDetailsAsync(detialsParam);

                RegisterDomainEvent(new CaseCourtDetailsChangedDomainEvent
                {
                    CaseNumber = CaseNumber,
                    PreviousCaseDetails = toBeDeactivatedCourt
                });
            }
        }

        public void UpdateAmounts(Money addedRequiredAmount, Money addedRemainingAmount)
        {
            TotalRequiredAmount = TotalRequiredAmount == null ? addedRequiredAmount : TotalRequiredAmount.Add(addedRequiredAmount);
            TotalRemainingAmount = TotalRemainingAmount == null ? addedRemainingAmount : TotalRemainingAmount.Add(addedRemainingAmount);
        }

        public void Activate()
        {
            if (CaseStatus == CaseStatusEnum.Closed)
            {
                CaseStatus = CaseStatusEnum.Active;
                RegisterDomainEvent(new CaseActivatedDomainEvent { CaseNumber = CaseNumber, OldStatus = CaseStatusEnum.Closed });
            }
        }
    }
}
