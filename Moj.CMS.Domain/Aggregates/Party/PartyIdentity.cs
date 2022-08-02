using System;
using System.Collections.Generic;
using Moj.CMS.Domain.Shared.Audit;
using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Domain.Shared.Guard;
using Moj.CMS.Domain.Shared.Values;

namespace Moj.CMS.Domain.Aggregates.Party
{
    public class PartyIdentity : ValueObject, ICreationAudited
    {
        public PartyIdentityTypeEnum PartyIdentityTypeId { get; private set; }
        public string PartyIdentityNumber { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreationTime { get; set; }
        public string CreatorUserId { get; set; }
        public static PartyIdentity NewPartyIdentity(string partyIdentityNumber, PartyIdentityTypeEnum partyIdentityTypeId)
        {
            Guard.AssertArgumentNotNullOrEmptyOrWhitespace(partyIdentityNumber, nameof(partyIdentityNumber));

            var partyIdentity = new PartyIdentity
            {
                IsActive = true,
                PartyIdentityNumber = partyIdentityNumber,
                PartyIdentityTypeId = partyIdentityTypeId
            };
            return partyIdentity;
        }
        public PartyIdentity Deactivate()
        {
            var partyIdentity = new PartyIdentity
            {
                IsActive = false,
                PartyIdentityNumber = PartyIdentityNumber,
                PartyIdentityTypeId = PartyIdentityTypeId
            };
            return partyIdentity;
        }
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return PartyIdentityTypeId;
            yield return PartyIdentityNumber;
        }
    }
}
