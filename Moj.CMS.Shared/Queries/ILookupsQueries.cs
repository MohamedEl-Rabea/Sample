using Moj.CMS.Domain.Shared.LookupModels;
using Moj.CMS.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Shared.Queries
{
    [ScopedService]
    public interface ILookupsQueries
    {
        Task<IEnumerable<Nationality>> GetNationalityList();
        Task<IEnumerable<SelectListItem>> GetPartiesRolesItemsNamesAsync(IEnumerable<int> itemsIds);
        Task<string> GetLookupItemNameAsync(int itemId);
        //Task<string> GetDivisionCourtCodeAsync(string divisionCode);
        //Task<int> GetDivisionIdByCodeAsync(string divisionCode);
        //Task<int> GetCourtIdByCodeAsync(string courtCode);
        Task<int> GetJudgeIdByCodeAsync(string judgeCode);
        Task<List<Judge>> GetJudgeNameByCodeAsync(string[] judgeCodes);
        Task<IEnumerable<CaseStatus>> GetCaseStatusListAsync();
        Task<IEnumerable<VIbanReferenceType>> GetVIbanReferenceTypeListAsync();
        Task<IEnumerable<CaseOperation>> GetCaseOperationListAsync();
        Task<IEnumerable<Area>> GetAreaListAsync();
        Task<IEnumerable<CaseType>> GetCaseTypeListAsync();
        Task<IEnumerable<Judge>> GetJudgeListAsync();
        Task<IEnumerable<PromissoryType>> GetPromissoryTypeListAsync();
        Task<IEnumerable<DebtType>> GetDebtTypeListAsync();
        Task<IEnumerable<ClaimFinancialStatus>> GetClaimFinancialStatusListAsync();
        Task<IEnumerable<ClaimStatus>> GetClaimStatusListAsync();
        Task<IEnumerable<PartyType>> GetPartyTypeListAsync();
        Task<IEnumerable<PartyRole>> GetPartyRoleListAsync();
        Task<IEnumerable<PartyIdentityType>> GetPartyIdentityTypeListAsync();
        Task<IEnumerable<PartyLocation>> GetPartyLocationListAsync();
    }
}
