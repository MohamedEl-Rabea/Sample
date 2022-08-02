using Moj.CMS.Domain.Aggregates.Claim.ValueObjects;
using Moj.CMS.Domain.DomainEvents;
using Moj.CMS.Domain.Shared.Enums;
using System.Collections.Generic;

namespace Moj.CMS.Domain.Aggregates.Claim.Events
{
    public class AccusedPartiesAddedToClaimDomainEvent : CaseChangedDomainEventBase
    {
        public IEnumerable<ClaimDetails> ClaimDetailsList { get; set; }
        public AccusedPartiesAddedToClaimDomainEvent()
        {
            Operation = CaseOperationEnum.AddAccusedPartiesToClaim;
        }
    }
}
