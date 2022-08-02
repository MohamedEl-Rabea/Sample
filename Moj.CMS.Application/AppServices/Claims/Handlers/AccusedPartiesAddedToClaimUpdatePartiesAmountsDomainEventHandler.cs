using Moj.CMS.Domain.Aggregates.Claim;
using Moj.CMS.Domain.Aggregates.Claim.Events;
using Moj.CMS.Domain.Aggregates.Party;
using Moj.CMS.Domain.Shared.Events;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Claims.Handlers
{
    public class AccusedPartiesAddedToClaimUpdatePartiesAmountsDomainEventHandler : DomainEventHandler<AccusedPartiesAddedToClaimDomainEvent>
    {
        private readonly IPartyRepository _partyRepository;

        public AccusedPartiesAddedToClaimUpdatePartiesAmountsDomainEventHandler(IPartyRepository partyRepository)
        {
            _partyRepository = partyRepository;
        }

        public override async Task Handle(AccusedPartiesAddedToClaimDomainEvent notification, CancellationToken cancellationToken)
        {
            var claim = notification.EventSource as Claim;
            var accussedClaimDetailPartiesNumbers = claim.ClaimDetailsList.Select(cd => cd.PartyNumber);
            var allAccussedPartiesAgg = await _partyRepository.GetAllAsync(c => accussedClaimDetailPartiesNumbers.Contains(c.PartyNumber));

            foreach (var accussedParty in allAccussedPartiesAgg)
            {
                var requiredAmount = claim.ClaimDetailsList.First(c => c.PartyNumber == accussedParty.PartyNumber).RequiredAmount;
                accussedParty.AddDebt(requiredAmount);
            }
        }
    }
}
