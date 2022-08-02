using MediatR;
using Moj.CMS.Application.AppServices.Claims.Commands.ClaimIncreaseDicrease;
using Moj.CMS.Domain.Aggregates.Claim;
using Moj.CMS.Domain.Aggregates.Claim.Entities;
using Moj.CMS.Domain.Lookups;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Case.Commands.ClaimIncreaseDicrease
{
    public class AddClaimEffectCommand : Command<IResult>
    {
        public ClaimEffectInputDto ClaimEffectInput { get; set; }
    }

    public class AddClaimEffectCommandHandler : IRequestHandler<AddClaimEffectCommand, IResult>
    {
        private readonly IClaimRepository _claimRepository;

        public AddClaimEffectCommandHandler(IClaimRepository claimRepository)
        {
            _claimRepository = claimRepository;
        }

        public async Task<IResult> Handle(AddClaimEffectCommand request, CancellationToken cancellationToken)
        {
            var claimEffectInput = request.ClaimEffectInput;
            var claim = await _claimRepository.GetClaimByNumberAsync(claimEffectInput.ClaimNumber);
            if (claim == null)
                throw new Exception($"Claim with Number {claimEffectInput.ClaimNumber} Not found");

            var isIncrement = FinancialEffectTypeLookUp.Find(claimEffectInput.EffectType).IsIncremental;

            if (isIncrement)
                claim.AddExtra(ClaimExtra.Create(claimEffectInput.EffectAmount.ToValueObject(), claimEffectInput.EffectType));
            else
                claim.Discount(claimEffectInput.EffectAmount.ToValueObject(), claimEffectInput.EffectType);

            return Result.Success();
        }
    }
}
