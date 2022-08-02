using MediatR;
using Microsoft.Extensions.Localization;
using Moj.CMS.Domain.Aggregates.Claim;
using Moj.CMS.Domain.Aggregates.Claim.ValueObjects;
using Moj.CMS.Domain.DomainServices.Party;
using Moj.CMS.Shared;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Claims.Commands.CreateNewDebt
{
    public class CreateNewDebtCommand : Command<IResult>
    {
        public IEnumerable<CreateNewDebtDto> CreateNewDebtDtoList { get; set; }
    }

    public class CreateNewDebtCommandHandler : IRequestHandler<CreateNewDebtCommand, IResult>
    {
        private readonly IClaimRepository _claimRepository;
        private readonly IGetCasePartiesNumbers _getCasePartiesNumbers;

        public CreateNewDebtCommandHandler(IClaimRepository claimRepository, IGetCasePartiesNumbers getCasePartiesNumbers)
        {
            _claimRepository = claimRepository;
            _getCasePartiesNumbers = getCasePartiesNumbers;
        }

        public async Task<IResult> Handle(CreateNewDebtCommand request, CancellationToken cancellationToken)
        {
            foreach (var DebtDto in request.CreateNewDebtDtoList)
            {
                var claim = await _claimRepository.GetClaimByNumberAsync(DebtDto.ClaimNumber);
                if (claim == null)
                {
                    throw new Exception($"Claim with Number {DebtDto.ClaimNumber} Not found");
                }
                var claimDetailsList = new List<ClaimDetails>();
                foreach (var party in DebtDto.ClaimDetailsList)
                {
                    claimDetailsList.Add(ClaimDetails.Create(party.AccusedPartyNumber, party.RequiredAmount.ToValueObject(), party.BillingAmount.ToValueObject()));
                }
                await claim.AddClaimDetailsAsync(claimDetailsList, _getCasePartiesNumbers);
            }
            return Result.Success();
        }
    }
}
