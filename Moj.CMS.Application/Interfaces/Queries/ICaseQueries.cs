using Moj.CMS.Application.AppServices.Case.Queries;
using Moj.CMS.Application.AppServices.Case.Queries.Dtos;
using Moj.CMS.Application.AppServices.Case.Queries.GetAllCases;
using Moj.CMS.Application.AppServices.Case.Queries.GetCaseBasicDetails;
using Moj.CMS.Application.AppServices.Case.Queries.GetCaseClaims;
using Moj.CMS.Application.AppServices.Case.Queries.GetCaseEvents;
using Moj.CMS.Application.AppServices.Case.Queries.GetCaseParties;
using Moj.CMS.Application.AppServices.Case.Queries.GetCasePromissories;
using Moj.CMS.Application.AppServices.Case.Queries.GetCaseSummary;
using Moj.CMS.Application.AppServices.Case.Queries.GetDashboardData;
using Moj.CMS.Application.AppServices.Case.Queries.GetEffectsAndDiscount;
using Moj.CMS.Shared.Requests;
using Moj.CMS.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Application.Interfaces.Queries
{
    [ScopedService]
    public interface ICaseQueries
    {
        Task<IEnumerable<CasePartyRolesInfo>> GetPartiesInfoByCaseNumberAsync(string caseNumber);
        Task<PagedResult<CaseListItemDto>> GetAllAsync(PagedRequest<CaseListItemDto> request);
        Task<CaseBasicDetailsDto> GetCaseBasicDetailsAsync(int caseId);
        Task<CaseSummaryDto> GetCaseSummaryAsync(int caseId);
        Task<IEnumerable<GetCasePromissoriesDto>> GetCasePromissoriesAsync(int caseId);
        Task<GetCaseClaimsDto> GetCaseClaimsAsync(int caseId);
        Task<int> GetCaseIdByCaseNumberAsync(string caseNumber);
        Task<CasePartiesDto> GetCasePartiesAsync(int caseId);
        Task<IEnumerable<CaseEventsDto>> GetCaseEventsAsync(int caseId);
        Task<List<string>> GetCaseNumbersByPartyIdAsync(int partyId);
        Task<List<string>> GetCaseNumbersByPromissoryNumberAsync(string promissoryNumber);
        Task<SummaryDto> GetDashboardSummaryAsync();
        Task<IEnumerable<CaseDto>> GetLastCasesAsync();
        Task<IEnumerable<CaseClaimAdjustmentChannelDto>> GetCaseAdjustmentChannelsAsync(int caseId);
    }
}
