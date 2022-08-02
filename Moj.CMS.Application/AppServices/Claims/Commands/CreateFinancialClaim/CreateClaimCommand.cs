using MediatR;
using Moj.CMS.Application.AppServices.Claims.Services;
using Moj.CMS.Application.Dtos;
using Moj.CMS.Application.Extensions;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Claims.Commands.CreateFinancialClaim
{
    public class CreateClaimCommand : Command<IResult<ResourceCreatedDto>>
    {
        public IEnumerable<CreateClaimDto> ClaimDtoList { get; set; }
    }

    public class CreateClaimCommandHandler : IRequestHandler<CreateClaimCommand, IResult<ResourceCreatedDto>>
    {
        private readonly IClaimService _claimService;
        public CreateClaimCommandHandler(IClaimService claimService)
        {
            _claimService = claimService;
        }

        public async Task<IResult<ResourceCreatedDto>> Handle(CreateClaimCommand request, CancellationToken cancellationToken)
        {
            var result = await _claimService.AddClaimsAsync(request.ClaimDtoList);
            return Result<ResourceCreatedDto>.Success(result.Select(c => c.Id).MapToResourceCreatedDto());
        }
    }
}
