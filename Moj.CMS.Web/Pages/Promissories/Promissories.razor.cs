using Microsoft.AspNetCore.WebUtilities;
using Microsoft.JSInterop;
using Moj.CMS.Application.AppServices.Promissory.Queries;
using Moj.CMS.Shared.Models;
using Moj.CMS.Shared.Requests;
using Moj.CMS.Web.Constants;
using Moj.CMS.Web.Extensions;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Moj.CMS.Web.Pages.Promissories
{
    public partial class Promissories
    {
        private IEnumerable<GetAllPromissoriesDto> pagedData;
        private IEnumerable<SelectListItem> promissoriesTypesList = new List<SelectListItem>();
        private IEnumerable<SelectListItem> deptTypesList = new List<SelectListItem>();
        private PromissoryFilter PromissoryFilter = new();
        private MudTable<GetAllPromissoriesDto> table;
        private MudDateRangePicker _promissoryPicker;
        private DateRange PromissoryDateRange { get; set; }
        private int totalItems;
        private bool _loading = true;
        private PagedRequest<GetAllPromissoriesDto> request = new();
        private bool FilterManagerOpen;

        protected override async Task OnInitializedAsync()
        {
            InitializeBreadcrumps(new ObservableCollection<BreadcrumbItem>
            {
                new BreadcrumbItem(Globallocalizer[ Urls.General.Tilte], href: "",disabled:true),
                new BreadcrumbItem(Globallocalizer[Urls.General.Promissories.Title], href: Urls.General.Promissories.Href, icon: Urls.General.Promissories.Icon),
            });
            promissoriesTypesList = await _lookupsService.GetPromissoriesTypesListItemsAsync();
            deptTypesList = await _lookupsService.GetDebtTypesListItemsAsync();
            await base.OnInitializedAsync();
        }

        private async Task<TableData<GetAllPromissoriesDto>> ServerReload(TableState state)
        {
            await LoadData(state);
            return new TableData<GetAllPromissoriesDto>() { TotalItems = totalItems, Items = pagedData };
        }
        private async Task LoadData(TableState state)
        {
            PromissoryFilter.PromissoryDateRange = PromissoryDateRange.MapToDateRangeDto();
            SetRequestDetails(request, state, PromissoryFilter);
            if (request.StateHasChanged())
            {
                _loading = true;
                var response = await CmsModule.ExecuteQueryAsync(new GetAllPromissoriesQuery { PagedRequest = request });
                if (response.Succeeded)
                {
                    totalItems = response.TotalCount;
                    pagedData = response.Data;
                    _loading = false;
                }
                else
                {
                    foreach (var error in response.Errors)
                    {
                        _snackBar.Add(localizer[error], Severity.Error);
                    }
                    _loading = false;
                }
            }
        }

        private void OnFilter()
        {
            table.ReloadServerData();
        }

        private void Clear()
        {
            PromissoryFilter.Clear();
            _promissoryPicker.Clear();
            table.ReloadServerData();
        }
        private async Task ExportToExcel()
        {
            PagedRequest<GetAllPromissoriesDto> excelRequest = new PagedRequest<GetAllPromissoriesDto>
            {
                PageSize = totalItems,
                Filter = request.GetFilter(),
                Sort = request.Sort
            };
            var result = await CmsModule.ExecuteQueryAsync(new ExportPromissoriesToExcel { PagedRequest = excelRequest });
            var fileInfo = result.Data;
            await _jsRuntime.InvokeAsync<List<GetAllPromissoriesDto>>("saveAsExcel", $"{localizer[fileInfo.FileName]}.xlsx", Convert.ToBase64String(fileInfo.FileData));
        }

        public async Task GenerateExcel()
        {
            await ExportToExcel();
        }

        void OpenFilterManager(bool value)
        {
            FilterManagerOpen = value;
        }
    }
}
