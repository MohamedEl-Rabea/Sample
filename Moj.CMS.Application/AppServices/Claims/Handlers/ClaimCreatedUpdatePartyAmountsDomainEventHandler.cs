using Moj.CMS.Domain.Aggregates.Case.Events;
using Moj.CMS.Domain.Aggregates.Claim;
using Moj.CMS.Domain.Aggregates.Party;
using Moj.CMS.Domain.Shared.Events;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Claims.Handlers
{
    public class ClaimCreatedUpdatePartyAmountsDomainEventHandler : DomainEventHandler<ClaimCreatedDomainEvent>
    {
        private readonly IPartyRepository _partyRepository;
        public ClaimCreatedUpdatePartyAmountsDomainEventHandler(IPartyRepository partyRepository)
        {
            _partyRepository = partyRepository;
        }

        public override async Task Handle(ClaimCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var claim = notification.EventSource as Claim;
            var newAccusedParties = notification.ClaimDetailsList.Select(cd => cd.PartyNumber);
            var allPartiesNumber = newAccusedParties.Append(claim.ComplaintPartyNumber);
            var partyList = await _partyRepository.GetAllAsync(c => allPartiesNumber.Contains(c.PartyNumber));

            var complaintPartyAgg = partyList.First(p => p.PartyNumber == claim.ComplaintPartyNumber);
            complaintPartyAgg.AddCredit(claim.TotalRequiredAmount);

            foreach (var party in partyList)
            {
                var accusedPartyClaimDetails = claim.ClaimDetailsList.FirstOrDefault(c => c.PartyNumber == party.PartyNumber);
                if (accusedPartyClaimDetails != null)
                    party.AddDebt(accusedPartyClaimDetails.RequiredAmount);
            }
        }
    }
}
