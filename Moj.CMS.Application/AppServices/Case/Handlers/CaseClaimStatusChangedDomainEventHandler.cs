using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Domain.Aggregates.Case;
using Moj.CMS.Domain.Aggregates.Claim;
using Moj.CMS.Domain.Aggregates.Claim.Events;
using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Domain.Shared.Events;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Case.Handlers
{
    public class CaseClaimStatusChangedDomainEventHandler : DomainEventHandler<ClaimStatusChangedDomainEvent>
    {
        private readonly IClaimQueries _claimsQueries;
        private readonly ICaseRepository _caseRepository;

        public CaseClaimStatusChangedDomainEventHandler(IClaimQueries claimsQueries, ICaseRepository caseRepository)
        {
            _claimsQueries = claimsQueries;
            _caseRepository = caseRepository;
        }
        public override async Task Handle(ClaimStatusChangedDomainEvent notification, CancellationToken cancellationToken)
        {
            var claim = notification.EventSource as Claim;
            if (claim != null)
            {
                var caseClaims = await _claimsQueries.GetCaseClaimsStatusAsync(claim.CaseNumber);
                var allOtherClaimsArePaid = caseClaims.Where(c => c.ClaimId != claim.Id).All(c => c.ClaimStatus == ClaimFinancialStatusEnum.FullyPaid);
                var allClaimsArePaid = claim.ClaimStatus.FinancialStatus == ClaimFinancialStatusEnum.FullyPaid && allOtherClaimsArePaid;
                if (allClaimsArePaid)
                {
                    var claimCase = await _caseRepository.GetCaseByNumberAsync(claim.CaseNumber);
                    claimCase.Close();
                }
            }
        }
    }
}
