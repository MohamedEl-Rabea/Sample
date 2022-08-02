using MediatR;
using Moj.CMS.Domain.Aggregates.Claim;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Claims.Commands.CloseClaim
{
    public class CloseClaimCommand : Command<IResult>
    {
        public CloseClaimDto CloseClaimDto { get; set; }
    }

    public class CloseClaimCommandHandler : IRequestHandler<CloseClaimCommand, IResult>
    {
        private readonly IClaimRepository _claimRepository;

        public CloseClaimCommandHandler(IClaimRepository claimRepository)
        {
            _claimRepository = claimRepository;
        }

        public async Task<IResult> Handle(CloseClaimCommand request, CancellationToken cancellationToken)
        {
            var requestClaim = request.CloseClaimDto;
            var claim = await _claimRepository.GetClaimByNumberAsync(requestClaim.ClaimNumber);
            claim.Close(requestClaim.ReferenceNumber, requestClaim.CloseDate);
            return Result.Success();
        }
    }
}