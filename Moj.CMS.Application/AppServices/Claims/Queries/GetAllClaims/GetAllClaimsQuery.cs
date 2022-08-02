using MediatR;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Claims.Queries
{
    public class GetAllClaimsQuery : PagedQuery<GetAllClaimsDto>
    {
    }

    public class GetAllClaimsQueryHandler : IRequestHandler<GetAllClaimsQuery, PagedResult<GetAllClaimsDto>>
    {
        private readonly IClaimQueries _claimQueries;

        public GetAllClaimsQueryHandler(IClaimQueries ClaimQueries)
        {
            _claimQueries = ClaimQueries;
        }

        public async Task<PagedResult<GetAllClaimsDto>> Handle(GetAllClaimsQuery request, CancellationToken cancellationToken)
        {
            var claims = await _claimQueries.GetAllAsync(request.PagedRequest);
            return claims;
        }
    }

}
