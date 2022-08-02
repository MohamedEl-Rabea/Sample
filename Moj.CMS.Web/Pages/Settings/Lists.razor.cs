using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Moj.CMS.Domain.Shared.Helpers;
using Moj.CMS.Domain.Shared.LookupModels;
using Moj.CMS.Web.Constants;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
namespace Moj.CMS.Web.Pages.Settings
{
    public partial class Lists
    {
        [Inject]
        Microsoft.Extensions.Localization.IStringLocalizer<Lists> currentPageLocalizer { set; get; }

        private MudTable<LookupBase> table;
        private int lookupTypeId { get; set; }
        private IEnumerable<LookupBase> items = Enumerable.Empty<LookupBase>();
        private IEnumerable<string> CurrentLookupProperties = Enumerable.Empty<string>();
        private IEnumerable<LookupTypesEnum> lookupTypes = LookupTypesEnum.List();
        private IDictionary<string, LookupFilterValue> filterValues = new Dictionary<string, LookupFilterValue>();
        protected override async Task OnInitializedAsync()
        {
            InitializeBreadcrumps(new ObservableCollection<BreadcrumbItem>
            {
                new BreadcrumbItem(Globallocalizer[ Urls.Settings.Tilte], href: "",disabled:true),
                new BreadcrumbItem(Globallocalizer[Urls.Settings.Lists.Title], href: Urls.Settings.Lists.Href, icon: Urls.Settings.Lists.Icon),
            });
            await base.OnInitializedAsync();
        }

        private IEnumerable<LookupFilterInfo> GetFilterableProps()
        {
            if (lookupTypeId > 0)
                return LookupTypesEnum.Find(lookupTypeId).GetFilterableProperties();

            return Enumerable.Empty<LookupFilterInfo>();
        }

        private void SetFilterValue(object filterValue, LookupFilterInfo filterInfo)
        {
            if (string.IsNullOrEmpty(Convert.ToString(filterValue)))
                filterValues.Remove(filterInfo.Name);
            else
                filterValues[filterInfo.Name] = new LookupFilterValue { Value = filterValue, PropertyType = filterInfo.PropertyType };
        }

        private IEnumerable<string> GetLookupProps()
        {
            if (lookupTypeId > 0)
                return LookupTypesEnum.Find(lookupTypeId).GetProperties();

            return Enumerable.Empty<string>();
        }

        private async Task LoadData()
        {
            if (lookupTypeId > 0)
            {
                var response = await _lookupsService.GetLookupItemsAsync(lookupTypeId, filterValues);
                items = response.ToList();
                CurrentLookupProperties = GetLookupProps();
            }
        }

        private dynamic GetPropertyValue(LookupBase context, string propName)
        {
            var isBoolean = context.GetType().GetProperty(propName).PropertyType == typeof(bool);
            var value = context.GetType().GetProperty(propName).GetValue(context, null);
            value = isBoolean ? currentPageLocalizer[value != null ? value.ToString() : string.Empty] : value;
            return value ?? string.Empty;
        }

        private bool IsSystemLookup()
        {
            if (lookupTypeId > 0)
                return LookupTypesEnum.Find(lookupTypeId).IsSystem;

            return false;
        }

        private async Task RefreshLookupItems()
        {
            items = Enumerable.Empty<LookupBase>();
            await LoadData();
        }

        private string GetFilterValue(LookupFilterInfo filter)
        {
            LookupFilterValue filterValue;
            if (filterValues.TryGetValue(filter.Name, out filterValue))
                return filterValue.Value.ToString();

            return string.Empty;
        }

        private HashSet<string> GetFilterValues(LookupFilterInfo filter)
        {
            LookupFilterValue filterValue;
            if (filterValues.TryGetValue(filter.Name, out filterValue))
                return new HashSet<string> { filterValue.Value.ToString() };

            return new HashSet<string>();
        }

        private async Task Clear(int selectedType)
        {
            lookupTypeId = selectedType;
            items = Enumerable.Empty<LookupBase>();
            filterValues.Clear();
            await LoadData();
        }
        private async Task ClearFilter()
        {
            items = Enumerable.Empty<LookupBase>();
            filterValues.Clear();
            await LoadData();
        }
        private async Task ExportToExcel()
        {
            var fileInfo = await _lookupsService.ExportLookupItemsToExcel(lookupTypeId, filterValues);
            await _jsRuntime.InvokeAsync<List<LookupBase>>("saveAsExcel", $"{localizer[fileInfo.FileName]}.xlsx", Convert.ToBase64String(fileInfo.FileData));
        }

        public async Task GenerateExcel()
        {
            await ExportToExcel();
        }
        private async Task InvokeModal(int id = 0)
        {
            await Task.FromResult(id);
            //USE TabletType to load the right data in the modal from the target table 

            //var parameters = new DialogParameters();
            //if (id != 0)
            //{
            //    var _case = pagedData.FirstOrDefault(c => c.Id == id);
            //    if (_case != null)
            //    {
            //        parameters.Add("Data", new
            //        {
            //            Id = _case.Id,
            //        });
            //    }

            //    var options = new DialogOptions() { /*CloseButton = true,*/ /*FullScreen = true,*/ MaxWidth = MaxWidth.ExtraLarge,/* FullWidth = true,*/ DisableBackdropClick = true };
            //    var dialog = await _dialogService.Show<CaseDetailsModal>("Modal", parameters, options).Result;
            //    //var result = await dialog.Result; 
            //    if (!dialog.Cancelled)
            //    {
            //        OnFilter();
            //    }
            //}
        }
    }
}