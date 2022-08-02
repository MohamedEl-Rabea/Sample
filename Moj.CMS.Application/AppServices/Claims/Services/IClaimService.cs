using Moj.CMS.Application.AppServices.Claims.Commands.CreateFinancialClaim;
using Moj.CMS.Domain.Aggregates.Claim;
using System.Collections.Generic;
using System.Threading.Tasks;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Application.AppServices.Claims.Services
{
    [ScopedService]
    public interface IClaimService
    {
        Task<List<Claim>> AddCaseClaimsAsync(IEnumerable<CreateClaimDto> ClaimDtos);
        Task<List<Claim>> AddClaimsAsync(IEnumerable<CreateClaimDto> ClaimDtos);
    }
}
