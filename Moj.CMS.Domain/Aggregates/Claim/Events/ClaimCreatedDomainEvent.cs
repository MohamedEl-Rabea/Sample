using Moj.CMS.Domain.Aggregates.Claim.ValueObjects;
using Moj.CMS.Domain.DomainEvents;
using Moj.CMS.Domain.Shared.Enums;
using System.Collections.Generic;

namespace Moj.CMS.Domain.Aggregates.Case.Events
{
    public class ClaimCreatedDomainEvent : CaseChangedDomainEventBase
    {
        public IEnumerable<ClaimDetails> ClaimDetailsList { get; set; }

        public ClaimCreatedDomainEvent()
        {
            Operation = CaseOperationEnum.AddClaim;
        }
    }
}
