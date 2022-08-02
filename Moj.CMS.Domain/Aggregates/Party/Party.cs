using Moj.CMS.Domain.Aggregates.Party.BusinessRules;
using Moj.CMS.Domain.ParameterObjects.Party;
using Moj.CMS.Domain.Shared.Audit;
using Moj.CMS.Domain.Shared.Entities;
using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Domain.Shared.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moj.CMS.Domain.Aggregates.Party
{
    public class Party : FullAuditedAggregateRoot, IAggregateRoot
    {
        private Party()
        {
            _partyIdentities = new List<PartyIdentity>();
        }
        public string FullName { get; private set; }
        public string NationalityCode { get; private set; }
        public string PartyNumber { get; private set; }
        public DateTime? DateOfBirth { get; private set; }
        public string CurrentIdentityNumber { get; private set; }

        public PartyTypeEnum PartyTypeId { get; private set; }
        public PartyLocationEnum PartyLocationId { get; private set; }
        public Gender Gender { get; private set; }
        public Money TotalCreditAmount { get; private set; }
        public Money TotalDebtAmount { get; private set; }

        public IReadOnlyCollection<PartyIdentity> PartyIdentities => _partyIdentities.ToList();
        private readonly List<PartyIdentity> _partyIdentities;


        public static async Task<Party> CreateAsync(PartyInfoParam creationParam)
        {
            var party = new Party();
            await party.SetPartyBasicInfoAsync(creationParam);
            return party;
        }

        public void AddCredit(Money creditAmount)
        {
            TotalCreditAmount ??= Money.Empty;
            TotalCreditAmount = TotalCreditAmount.Add(creditAmount);
        }

        public void AddDebt(Money requiredAmount)
        {
            TotalDebtAmount ??= Money.Empty;
            TotalDebtAmount = TotalDebtAmount.Add(requiredAmount);
        }

        public void SetPartyIdentity(PartyIdentity partyIdentity)
        {
            if (CurrentIdentityNumber != partyIdentity?.PartyIdentityNumber)
            {
                _partyIdentities.RemoveAll(pi => pi.PartyIdentityTypeId == partyIdentity.PartyIdentityTypeId);
                var currentActiveIdentity = _partyIdentities.FirstOrDefault(pi => pi.PartyIdentityNumber == CurrentIdentityNumber);
                currentActiveIdentity = currentActiveIdentity?.Deactivate();
                CurrentIdentityNumber = partyIdentity.PartyIdentityNumber;
                _partyIdentities.Add(partyIdentity);
            }
        }
        public async Task UpdateAsync(PartyInfoParam updateParam)
        {
            await SetPartyBasicInfoAsync(updateParam);
        }
        private async Task SetPartyBasicInfoAsync(PartyInfoParam input)
        {
            await CheckRuleAsync(new PartyNumberShouldNotBeDuplicatedBusinessRule(input.EnforcePartyNumberIsUnique, input.PartyNumber, Id));

            FullName = input.FullName;
            NationalityCode = input.NationalityCode;
            PartyNumber = input.PartyNumber;
            DateOfBirth = input.DateOfBirth;
            PartyTypeId = input.PartyTypeId;
            PartyLocationId = input.PartyLocationId;
            Gender = input.Gender;
            SetPartyIdentity(input.PartyIdentity);
        }
    }
}
