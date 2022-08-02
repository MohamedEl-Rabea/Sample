using Moj.CMS.Application.AppServices.Claims.Commands.CreateFinancialClaim;
using Moj.CMS.Domain.Aggregates.Claim;
using Moj.CMS.Domain.Aggregates.Claim.ValueObjects;
using Moj.CMS.Domain.DomainServices;
using Moj.CMS.Domain.DomainServices.Party;
using Moj.CMS.Domain.ParameterObjects.Claim;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Claims.Services
{
    public class ClaimService : IClaimService
    {
        private readonly IClaimRepository _claimRepository;
        private readonly IEnforceCaseIsFound _enforceCaseIsFound;
        private readonly IGetCasePartiesNumbers _enforcePartiesAreAssignedToCase;

        public ClaimService(IClaimRepository claimRepository, IEnforceCaseIsFound enforceCaseIsFound, IGetCasePartiesNumbers enforcePartiesAreAssignedToCase)
        {
            _claimRepository = claimRepository;
            _enforceCaseIsFound = enforceCaseIsFound;
            _enforcePartiesAreAssignedToCase = enforcePartiesAreAssignedToCase;
        }

        public async Task<List<Claim>> AddCaseClaimsAsync(IEnumerable<CreateClaimDto> claimDtos)
        {
            return await AddClaims(claimDtos, false);
        }

        public async Task<List<Claim>> AddClaimsAsync(IEnumerable<CreateClaimDto> claimDtos)
        {
            return await AddClaims(claimDtos, true);
        }

        private async Task<List<Claim>> AddClaims(IEnumerable<CreateClaimDto> claims, bool shouldRegisterDomainEvent)
        {
            var createdClaims = new List<Claim>();
            foreach (var claimDto in claims)
            {
                var createClaimParam = BuildClaimCreationParamAsync(claimDto, shouldRegisterDomainEvent);
                var createClaim = await Claim.Create(createClaimParam);
                await _claimRepository.AddAsync(createClaim);
                createdClaims.Add(createClaim);
            }
            return createdClaims;
        }

        private CreateClaimParam BuildClaimCreationParamAsync(CreateClaimDto claimDto, bool shouldRegisterDomainEvent)
        {
            var claimDetailsParam = new CreateClaimParam
            {
                ClaimDetailsList = claimDto.Claim.ClaimDetails.Select(x => ClaimDetails.Create(x.AccusedPartyNumber, x.RequiredAmount.ToValueObject(), x.BillingAmount.ToValueObject())),
                CaseNumber = claimDto.CaseNumber,
                ComplaintPartyNumber = claimDto.Claim.ComplaintPartyNumber,
                ClaimDate = claimDto.Claim.ClaimDateTime,
                DebtTypeId = claimDto.Claim.DebtTypeId,
                PromissoryNumber = claimDto.PromissoryNumber,
                IsJoint = claimDto.Claim.IsJoint,
                BasicAmount = claimDto.Claim.BasicAmount.ToValueObject(),
                RemainingAmount = claimDto.Claim.RemainingAmount.ToValueObject(),
                RequiredAmount = claimDto.Claim.RequiredAmount.ToValueObject(),
                EnforceCaseIsFound = _enforceCaseIsFound,
                ShouldRegisterDomainEvent = shouldRegisterDomainEvent,
                EnforcePartiesAreAssignedToCase = _enforcePartiesAreAssignedToCase
            };
            return claimDetailsParam;
        }
    }
}
