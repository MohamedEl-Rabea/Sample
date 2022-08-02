using Microsoft.JSInterop;
using Moj.CMS.Application.AppServices.VIban.Queries.ExportToExcel;
using Moj.CMS.Application.AppServices.VIban.Queries.GetAllVIbans;
using Moj.CMS.Shared.Models;
using Moj.CMS.Shared.Requests;
using Moj.CMS.Web.Extensions;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moj.CMS.Web.Pages.VIbans
{
    public partial class VIbans
    {
        private IEnumerable<SelectListItem> _referenceTypesList = new List<SelectListItem>();

        private IEnumerable<VIbanDto> _pagedData;
        private PagedRequest<VIbanDto> request = new PagedRequest<VIbanDto>();
        private VIbanFilter vIbanFilter = new VIbanFilter();
        private DateRange IssueDateRange { get; set; }
        private MudDateRangePicker _issueDatePicker;
        private MudTable<VIbanDto> _table;

        public bool FilterManagerOpen;
        private int _totalItems;
        private int _currentPage;
        public bool Loading = true;

        protected override async Task OnInitializedAsync()
        {
            _referenceTypesList = await _lookupsService.GetVIbanReferenceTypeListItemsAsync();
            await base.OnInitializedAsync();
        }

        private async Task<TableData<VIbanDto>> ServerReload(TableState state)
        {
            await LoadData(state);
            return new TableData<VIbanDto> { TotalItems = _totalItems, Items = _pagedData };
        }

        private async Task LoadData(TableState state)
        {
            Loading = true;
            vIbanFilter.IssueDateRange = IssueDateRange.MapToDateRangeDto();
            SetRequestDetails(request, state, vIbanFilter);
            if (request.StateHasChanged())
            {
                var response = await CmsModule.ExecuteQueryAsync(new GetAllVIbansQuery { PagedRequest = request });
                if (response.Succeeded)
                {
                    _totalItems = response.TotalCount;
                    _currentPage = response.CurrentPage;
                    _pagedData = response.Data;
                    Loading = false;
                }
                else
                {
                    foreach (var error in response.Errors)
                    {
                        _snackBar.Add(localizer[error], Severity.Error);
                    }
                }
            }
        }

        void OpenFilterManager(bool value)
        {
            FilterManagerOpen = value;
        }

        private void OnFilter()
        {
            _table.ReloadServerData();
        }

        private void Clear()
        {
            vIbanFilter.Clear();
            _issueDatePicker.Clear();
            _table.ReloadServerData();
        }

        public async Task GenerateExcel()
        {
            await ExportToExcel();
        }

        private async Task ExportToExcel()
        {
            PagedRequest<VIbanDto> excelRequest = new PagedRequest<VIbanDto>();
            excelRequest.PageSize = _totalItems;
            excelRequest.Filter = request.GetFilter();
            excelRequest.Sort = request.Sort;

            var result = await CmsModule.ExecuteQueryAsync(new ExportVIbansToExcel { PagedRequest = excelRequest });
            var fileInfo = result.Data;
            await _jsRuntime.InvokeAsync<List<VIbanDto>>("saveAsExcel", $"{localizer[fileInfo.FileName]}.xlsx", Convert.ToBase64String(fileInfo.FileData));
        }
    }
}
