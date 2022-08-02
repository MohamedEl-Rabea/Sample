using Microsoft.JSInterop;
using Moj.CMS.Application.AppServices.Claims.Queries;
using Moj.CMS.Shared.Models;
using Moj.CMS.Shared.Requests;
using Moj.CMS.Web.Constants;
using Moj.CMS.Web.Extensions;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Moj.CMS.Web.Pages.Claims
{
    public partial class Claims
    {
        private IEnumerable<GetAllClaimsDto> pagedData;
        private IEnumerable<SelectListItem> courtList = new List<SelectListItem>();
        private IEnumerable<SelectListItem> divisionList = new List<SelectListItem>();
        private IEnumerable<SelectListItem> JudgetList = new List<SelectListItem>();
        private IEnumerable<SelectListItem> claimStatusList = new List<SelectListItem>();
        private ClaimFilter claimFilter = new();
        private MudTable<GetAllClaimsDto> table;
        private int totalItems;
        private PagedRequest<GetAllClaimsDto> request = new();
        public bool FilterManagerOpen;
        public bool _loading = true;
        private MudDateRangePicker claimDatePicker;
        private DateRange claimDateValue;


        void OpenFilterManager(bool value)
        {
            FilterManagerOpen = value;
        }
        protected override async Task OnInitializedAsync()
        {
            InitializeBreadcrumps(new ObservableCollection<BreadcrumbItem>
            {
                new BreadcrumbItem(Globallocalizer[ Urls.General.Tilte], href: "",disabled:true),
                new BreadcrumbItem(Globallocalizer[Urls.General.Claims.Title], href: Urls.General.Claims.Href, icon: Urls.General.Claims.Icon),
            });
            await InitializeLookups();
            await base.OnInitializedAsync();
        }

        private async Task InitializeLookups()
        {
            courtList = await _courtQueries.GetCourtListAsync();
            JudgetList = await _lookupsService.GetJudgeListItemsAsync();
            claimStatusList = await _lookupsService.GetFinancialClaimStatusesListItemsAsync();
        }

        private async Task<TableData<GetAllClaimsDto>> ServerReload(TableState state)
        {
            await LoadData(state);
            return new TableData<GetAllClaimsDto>() { TotalItems = totalItems, Items = pagedData };
        }

        private async Task LoadData(TableState state)
        {
            _loading = true;
            claimFilter.ClaimDateRange = claimDateValue.MapToDateRangeDto();
            SetRequestDetails(request, state, claimFilter);
            if (request.StateHasChanged())
            {
                var response = await CmsModule.ExecuteQueryAsync(new GetAllClaimsQuery { PagedRequest = request });
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
                }
            }
        }

        private async Task RefreshDivisionList(string courtCode)
        {
            claimFilter.CourtCode = courtCode;
            divisionList = await _courtQueries.GetDivisionListByCourtCodeAsync(courtCode);
        }

        private void OnFilter()
        {
            table.ReloadServerData();
        }

        private void Clear()
        {
            claimFilter.Clear();
            claimDatePicker.Clear();
            table.ReloadServerData();
        }

        private async Task ExportToExcel()
        {
            PagedRequest<GetAllClaimsDto> excelRequest = new();
            excelRequest.PageSize = totalItems;
            excelRequest.Filter = request.GetFilter();
            excelRequest.Sort = request.Sort;

            var result = await CmsModule.ExecuteQueryAsync(new ExportClaimsToExcel { PagedRequest = excelRequest });
            var fileInfo = result.Data;
            await _jsRuntime.InvokeAsync<List<GetAllClaimsDto>>("saveAsExcel", $"{Globallocalizer[fileInfo.FileName]}.xlsx", Convert.ToBase64String(fileInfo.FileData));
        }

        public async Task GenerateExcel()
        {
            await ExportToExcel();
        }
    }
}