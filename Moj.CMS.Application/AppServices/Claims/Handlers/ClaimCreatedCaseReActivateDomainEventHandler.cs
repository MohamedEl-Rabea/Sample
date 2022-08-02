using Moj.CMS.Domain.Aggregates.Case;
using Moj.CMS.Domain.Aggregates.Case.Events;
using Moj.CMS.Domain.Aggregates.Claim;
using Moj.CMS.Domain.Shared.Events;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Claims.Handlers
{
    public class ClaimCreatedCaseReActivateDomainEventHandler : DomainEventHandler<ClaimCreatedDomainEvent>
    {
        private readonly ICaseRepository _caseRepository;
        public ClaimCreatedCaseReActivateDomainEventHandler(ICaseRepository caseRepository)
        {
            _caseRepository = caseRepository;
        }

        public override async Task Handle(ClaimCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var claim = notification.EventSource as Claim;
            var caseAgg=await _caseRepository.GetCaseByNumberAsync(claim.CaseNumber);
            caseAgg.Activate();
        }
    }
}
