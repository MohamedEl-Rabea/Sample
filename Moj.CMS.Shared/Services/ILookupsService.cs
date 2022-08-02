using Moj.CMS.Domain.Shared.Helpers;
using Moj.CMS.Domain.Shared.LookupModels;
using Moj.CMS.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Shared.Services
{
    [ScopedService]
    public interface ILookupsService
    {

        Task<IEnumerable<SelectListItem>> GetNationalityList();
        Task<IEnumerable<SelectListItem>> GetPartiesRolesItemsNamesAsync(IEnumerable<int> itemsIds);
        Task<string> GetLookupItemNameAsync(int itemId);
        Task<IEnumerable<SelectListItem>> GetCaseStatusListItemsAsync();
        Task<IEnumerable<SelectListItem>> GetVIbanReferenceTypeListItemsAsync();
        Task<IEnumerable<SelectListItem>> GetCaseOperationListItemsAsync();
        Task<IEnumerable<SelectListItem>> GetCaseTypeListItemsAsync();
        Task<IEnumerable<SelectListItem>> GetPartyIdentityTypeListItemsAsync();
        Task<IEnumerable<SelectListItem>> GetPartyLocationListItemsAsync();
        Task<IEnumerable<SelectListItem>> GetJudgeListItemsAsync();
        Task<IEnumerable<SelectListItem>> GetPromissoriesTypesListItemsAsync();
        Task<IEnumerable<SelectListItem>> GetDebtTypesListItemsAsync();
        Task<IEnumerable<SelectListItem>> GetFinancialClaimStatusesListItemsAsync();
        Task<IEnumerable<LookupBase>> GetLookupItemsAsync(int lookupTypeId, IDictionary<string, LookupFilterValue> filterValues);
        Task<ExportedFileInfo> ExportLookupItemsToExcel(int lookupTypeId, IDictionary<string, LookupFilterValue> filterValues);
        Task<IEnumerable<SelectListItem>> GetPartyTypeListItemsAsync();
    }
}
