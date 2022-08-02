using Microsoft.Extensions.Localization;
using Moj.CMS.Domain.Shared.Helpers;
using Moj.CMS.Domain.Shared.LookupModels;
using Moj.CMS.Shared.Models;
using Moj.CMS.Shared.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Moj.CMS.Shared.Services
{
    public class LookupsService : ILookupsService
    {
        private readonly ILookupsQueries _lookupsQueries;
        private readonly IFileBuilder _fileBuilder;
        private readonly IStringLocalizer<CMSLocalizer> _localizer;

        public LookupsService(ILookupsQueries lookupsQueries, IFileBuilder fileBuilder, IStringLocalizer<CMSLocalizer> localizer)
        {
            _lookupsQueries = lookupsQueries;
            _fileBuilder = fileBuilder;
            _localizer = localizer;
        }

        public async Task<string> GetLookupItemNameAsync(int itemId)
        {
            return await _lookupsQueries.GetLookupItemNameAsync(itemId);
        }

        public async Task<IEnumerable<SelectListItem>> GetPartiesRolesItemsNamesAsync(IEnumerable<int> itemsIds)
        {
            return await _lookupsQueries.GetPartiesRolesItemsNamesAsync(itemsIds);
        }

        public async Task<int> GetJudgeIdByCodeAsync(string judgeCode)
        {
            return await _lookupsQueries.GetJudgeIdByCodeAsync(judgeCode);
        }

        public async Task<IEnumerable<SelectListItem>> GetCaseStatusListItemsAsync()
        {
            var caseStatusLookupItems = await _lookupsQueries.GetCaseStatusListAsync();
            var result = caseStatusLookupItems.Select(l => new SelectListItem { Key = l.Id, Text = l.Name });
            return result;
        }

        public async Task<IEnumerable<SelectListItem>> GetVIbanReferenceTypeListItemsAsync()
        {
            var referenceTypeLookupItems = await _lookupsQueries.GetVIbanReferenceTypeListAsync();
            var result = referenceTypeLookupItems.Select(l => new SelectListItem { Key = l.Id, Text = l.Name });
            return result;
        }

        public async Task<IEnumerable<SelectListItem>> GetCaseOperationListItemsAsync()
        {
            var caseOperationLookupItems = await _lookupsQueries.GetCaseOperationListAsync();
            var result = caseOperationLookupItems.Select(l => new SelectListItem { Key = l.Id, Text = l.Name });
            return result;
        }

        public async Task<IEnumerable<SelectListItem>> GetCaseTypeListItemsAsync()
        {
            var caseTypeLookupItems = await _lookupsQueries.GetCaseTypeListAsync();
            var result = caseTypeLookupItems.Select(l => new SelectListItem { Key = l.Id, Text = l.Name });
            return result;
        }

        public async Task<IEnumerable<SelectListItem>> GetPartyIdentityTypeListItemsAsync()
        {
            var partyIdentityTypeItems = await _lookupsQueries.GetPartyIdentityTypeListAsync();
            var result = partyIdentityTypeItems.Select(l => new SelectListItem { Key = l.Id, Text = l.Name });
            return result;
        }
        public async Task<IEnumerable<SelectListItem>> GetPartyTypeListItemsAsync()
        {
            var partyTypeItems = await _lookupsQueries.GetPartyTypeListAsync();
            var result = partyTypeItems.Select(l => new SelectListItem { Key = l.Id, Text = l.Name });
            return result;
        }

        public async Task<IEnumerable<SelectListItem>> GetPartyLocationListItemsAsync()
        {
            var partyLocationItems = await _lookupsQueries.GetPartyLocationListAsync();
            var result = partyLocationItems.Select(l => new SelectListItem { Key = l.Id, Text = l.Name });
            return result;
        }

        public async Task<IEnumerable<SelectListItem>> GetJudgeListItemsAsync()
        {
            var lookupItems = await _lookupsQueries.GetJudgeListAsync();
            var result = lookupItems.Select(l => new SelectListItem { Key = l.Id, Text = l.Name, Code = l.Code });
            return result;
        }

        public async Task<IEnumerable<SelectListItem>> GetPromissoriesTypesListItemsAsync()
        {
            var lookupItems = await _lookupsQueries.GetPromissoryTypeListAsync();
            var result = lookupItems.Select(l => new SelectListItem { Key = l.Id, Text = l.Name });
            return result;
        }

        public async Task<IEnumerable<SelectListItem>> GetDebtTypesListItemsAsync()
        {
            var lookupItems = await _lookupsQueries.GetDebtTypeListAsync();
            var result = lookupItems.Select(l => new SelectListItem { Key = l.Id, Text = l.Name });
            return result;
        }
        public async Task<IEnumerable<SelectListItem>> GetNationalityList()
        {
            var lookupItems = await _lookupsQueries.GetNationalityList();
            var result = lookupItems.Select(l => new SelectListItem { Code = l.Code, Text = l.Name });
            return result;
        }

        public async Task<IEnumerable<SelectListItem>> GetFinancialClaimStatusesListItemsAsync()
        {
            var lookupItems = await _lookupsQueries.GetClaimFinancialStatusListAsync();
            var result = lookupItems.Select(l => new SelectListItem { Key = l.Id, Text = l.Name });
            return result;
        }

        public async Task<IEnumerable<LookupBase>> GetLookupItemsAsync(int lookupTypeId, IDictionary<string, LookupFilterValue> filterValues)
        {
            var lookupType = LookupTypesEnum.Find(lookupTypeId);
            var task = (Task)typeof(ILookupsQueries).GetMethod(lookupType.QueryMethod).Invoke(_lookupsQueries, null);
            await task.ConfigureAwait(false);
            var resultProperty = task.GetType().GetProperty("Result");
            var result = (IEnumerable<LookupBase>)resultProperty.GetValue(task);
            result = ApplyFilters(result, filterValues);
            ApplyStatusTextLocalization(result);
            return result;
        }

        public async Task<ExportedFileInfo> ExportLookupItemsToExcel(int lookupTypeId, IDictionary<string, LookupFilterValue> filterValues)
        {
            var result = await GetLookupItemsAsync(lookupTypeId, filterValues);
            var lookupType = LookupTypesEnum.Find(lookupTypeId);
            var exportableProperties = lookupType.GetExportableProperties();
            var file = _fileBuilder.GenerateExcel(result, lookupType.Name, exportableProperties.ToArray());
            ExportedFileInfo exportedFileInfo = new ExportedFileInfo()
            {
                FileData = file,
                FileName = lookupType.Name
            };
            return exportedFileInfo;
        }

        private IEnumerable<LookupBase> ApplyFilters(IEnumerable<LookupBase> items, IDictionary<string, LookupFilterValue> filterValues)
        {
            return items.Where(item => item.GetType()
            .GetProperties()
            .All(prop => !filterValues.ContainsKey(prop.Name) || FilterPropertyValuesMatch(item, prop, filterValues[prop.Name])));
        }

        private bool FilterPropertyValuesMatch(LookupBase lookupItem, PropertyInfo property, LookupFilterValue filterValue)
        {
            if (filterValue.PropertyType == PropertyType.String)
                return Convert.ToString(property.GetValue(lookupItem, null)).Contains(Convert.ToString(filterValue.Value));
            else if (filterValue.PropertyType == PropertyType.Status || filterValue.PropertyType == PropertyType.Digits)
                return Convert.ToBoolean(property.GetValue(lookupItem, null)) == Convert.ToBoolean(filterValue.Value);
            else
                return true;
        }

        private void ApplyStatusTextLocalization(IEnumerable<LookupBase> lookupItems)
        {
            foreach (var item in lookupItems)
            {
                item.StatusText = _localizer[item.StatusText];
            }
        }
    }
}
