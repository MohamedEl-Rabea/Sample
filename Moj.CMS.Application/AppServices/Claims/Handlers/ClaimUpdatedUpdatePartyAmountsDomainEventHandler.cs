using Moj.CMS.Domain.Aggregates.Claim;
using Moj.CMS.Domain.Aggregates.Claim.Events;
using Moj.CMS.Domain.Aggregates.Party;
using Moj.CMS.Domain.Shared.Events;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Claims.Handlers
{
    public class ClaimUpdatedUpdatePartyAmountsDomainEventHandler : DomainEventHandler<ClaimUpdatedDomainEvent>
    {
        private readonly IPartyRepository _partyRepository;
        public ClaimUpdatedUpdatePartyAmountsDomainEventHandler(IPartyRepository partyRepository)
        {
            _partyRepository = partyRepository;
        }

        public override async Task Handle(ClaimUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var claim = notification.EventSource as Claim;
            var complaintPartyAgg = await _partyRepository.GetOrDefaultAsync(c => claim.ComplaintPartyNumber == c.PartyNumber);
            var updatedTotalRemainingAmount = claim.RemainingAmount.Subtract(notification.OldTotalRemainingAmount);
            complaintPartyAgg.AddCredit(updatedTotalRemainingAmount);
        }
    }
}
