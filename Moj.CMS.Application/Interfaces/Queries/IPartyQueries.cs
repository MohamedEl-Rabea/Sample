using Moj.CMS.Application.AppServices.Party.Queries;
using Moj.CMS.Application.AppServices.Party.Queries.Dtos;
using Moj.CMS.Shared.Requests;
using Moj.CMS.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Application.Interfaces.Queries
{
    [TransientService]
    public interface IPartyQueries
    {
        Task<PartyDebtsSummaryDto> GetPartyDebtsSummary(int partyId);
        Task<PartyCreditsSummaryDto> GetPartyCreditsSummary(int partyId);
        Task<PartyCaseListDto> GetPartyCasesAsync(int partyId);
        Task<PartyListItemDto> GetPartyBasicDetailsAsync(int partyId);
        Task<IEnumerable<PartyListDto>> GetAllPartiesAsync();
        Task<IEnumerable<PartyBasicInfoDto>> GetPartiesBasicInfoByNumbersAsync(IEnumerable<string> partiesNumbers);
        Task<IEnumerable<PartyBasicInfoDto>> GetPartiesBasicInfoByIdsAsync(IEnumerable<int> partiesIds);
        Task<int> GetPartyIdByPartyNumberAsync(string partyNumber);
        Task<PartyBasicInfoDto> GetPartyIdByNumberAsync(string partyNumber);
        Task<PagedResult<PartyListItemDto>> GetAllPartiesAsync(PagedRequest<PartyListItemDto> request);
    }
}
