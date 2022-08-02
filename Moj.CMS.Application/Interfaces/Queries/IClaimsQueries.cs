using Moj.CMS.Application.AppServices.Case.Queries.GetEffectsAndDiscount;
using Moj.CMS.Application.AppServices.Claims.Queries;
using Moj.CMS.Application.AppServices.Claims.Queries.Dtos;
using Moj.CMS.Shared.DTO;
using Moj.CMS.Application.AppServices.Party.Queries.Dtos;
using Moj.CMS.Shared.Requests;
using Moj.CMS.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Application.Interfaces.Queries
{
    [ScopedService]
    public interface IClaimQueries
    {
        Task<IEnumerable<PartyAccountStatementDto>> GetPartyAccountStatement(int partyId);
        Task<PagedResult<GetAllClaimsDto>> GetAllAsync(PagedRequest<GetAllClaimsDto> request);
        Task<IEnumerable<ClaimBasicDetailsDto>> GetCaseClaimsAsync(string caseNumber);
        Task<int> GetClaimIdByNumberAsync(string claimNumber);
        Task<IEnumerable<string>> GetClaimAccusedPartyNumbersAsync(string claimNumber);
        Task<IEnumerable<CaseClaimStatusDto>> GetCaseClaimsStatusAsync(string caseNumber);
        Task<IEnumerable<GetEffectsAndDiscountDto>> GetClaimEffectsAsync(int caseId);
        Task<MoneyDto> GetClaimTotalRemainingAmountAsync(string claimNumber);
    }
}
