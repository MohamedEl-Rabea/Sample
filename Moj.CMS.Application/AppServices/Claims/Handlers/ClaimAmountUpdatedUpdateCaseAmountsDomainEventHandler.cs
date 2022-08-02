using Moj.CMS.Domain.Aggregates.Case;
using Moj.CMS.Domain.Aggregates.Claim;
using Moj.CMS.Domain.Aggregates.Claim.Events;
using Moj.CMS.Domain.Shared.Events;
using Moj.CMS.Domain.Shared.Values;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Claims.Handlers
{
    public class ClaimAmountUpdatedUpdateCaseAmountsDomainEventHandler : DomainEventHandler<ClaimAmountUpdatedDomainEvent>
    {
        private readonly ICaseRepository _caseRepository;

        public ClaimAmountUpdatedUpdateCaseAmountsDomainEventHandler(ICaseRepository caseRepository)
        {
            _caseRepository = caseRepository;
        }

        public override async Task Handle(ClaimAmountUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var claim = notification.EventSource as Claim;
            var caseAgg = await _caseRepository.GetCaseByNumberAsync(claim.CaseNumber);
            var updatedTotalRequiredAmount = claim.TotalRequiredAmount.Subtract(notification.OldTotalRequiredAmount);
            var updatedTotalRemainingAmount = claim.TotalRemainingAmount.Subtract(notification.OldTotalRemainingAmount);
            caseAgg.UpdateAmounts(updatedTotalRequiredAmount, updatedTotalRemainingAmount);
        }
    }
}
