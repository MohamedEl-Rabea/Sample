using Microsoft.JSInterop;
using Moj.CMS.Application.AppServices.Case.Queries;
using Moj.CMS.Application.AppServices.Case.Queries.ExportToExcel;
using Moj.CMS.Application.AppServices.Case.Queries.GetAllCases;
using Moj.CMS.Application.AppServices.Case.Queries.GetCaseNumbersbyPartyId;
using Moj.CMS.Application.AppServices.Case.Queries.GetCaseNumbersbyPromissoryNumber;
using Moj.CMS.Shared.Models;
using Moj.CMS.Shared.Requests;
using Moj.CMS.Web.Constants;
using Moj.CMS.Web.Extensions;
using MudBlazor;
using SSS.Components.NumberRange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moj.CMS.Web.Pages.Cases
{
    public partial class Cases
    {
        private IEnumerable<CaseListItemDto> pagedData;
        private IEnumerable<SelectListItem> caseStatusList = new List<SelectListItem>();
        private IEnumerable<SelectListItem> courtList = new List<SelectListItem>();
        private IEnumerable<SelectListItem> divisionList = new List<SelectListItem>();
        private IEnumerable<SelectListItem> judgeList = new List<SelectListItem>();
        private IEnumerable<SelectListItem> caseTypetList = new List<SelectListItem>();

        private CaseFilter caseFilter = new();
        private MudTable<CaseListItemDto> table;
        private int totalItems;
        private int currentPage;
        private PagedRequest<CaseListItemDto> request = new();
        private string _caseNumber;
        private string[] _caseNumbersFromDb;
        public bool FilterManagerOpen;
        public bool _loading = true;
        private MudDateRangePicker _acceptancePicker;
        private MudDateRangePicker _receivePicker;
        private DateRange JudgeAcceptanceDateRange { get; set; }
        private DateRange ReceiveDateRange { get; set; }
        private DecimalRange TotalRequiredRange { get; set; }

        private void OpenFilterManager(bool value)
        {
            FilterManagerOpen = value;
        }

        protected override async Task OnInitializedAsync()
        {
            InitializeBreadcrumps();
            await InitializeLookups();
            await base.OnInitializedAsync();
        }

        private void InitializeBreadcrumps()
        {
            string paramVal = "";
            var navigateToPartyCases = _navigationManager.TryGetQueryString("partyId", out paramVal);
            if (!navigateToPartyCases)
            {
                InitializeBreadcrumps(new List<BreadcrumbItem>
                {
                    new BreadcrumbItem(Globallocalizer[Urls.General.Tilte], href: "",disabled:true),
                    new BreadcrumbItem(Globallocalizer[Urls.General.Cases.Title], Urls.General.Cases.Href, icon: Urls.General.Cases.Icon),
                });
            }
        }

        private async Task GetPartyCaseNumberIfApplicable()
        {
            int partyId;
            var hasPartyId = _navigationManager.TryGetQueryString("partyId", out partyId);
            if (hasPartyId)
                _caseNumbersFromDb = (await CmsModule.ExecuteQueryAsync(new GetCaseNumbersbyPartyIdQuery
                {
                    PartyId = partyId
                })).Data.ToArray();
        }

        private async Task GetPromissoryCasesNumberIfApplicable()
        {
            if (string.IsNullOrWhiteSpace(_caseNumber))
            {
                var hasPromissoryNumber = _navigationManager.TryGetQueryString("promissoryNumber", out string promissoryNumber);
                if (hasPromissoryNumber)
                    _caseNumbersFromDb = (await CmsModule.ExecuteQueryAsync(new GetCaseNumbersbyPromissoryNumberQuery
                    {
                        PromissoryNumber = promissoryNumber
                    })).Data.ToArray();
            }

        }
        private async Task InitializeLookups()
        {
            caseStatusList = await _lookupsService.GetCaseStatusListItemsAsync();
            caseTypetList = await _lookupsService.GetCaseTypeListItemsAsync();
            divisionList = await _courtQueries.GetDivisionListAsync();
            courtList = await _courtQueries.GetCourtListAsync();
            judgeList = await _lookupsService.GetJudgeListItemsAsync();
        }

        private async Task<TableData<CaseListItemDto>> ServerReload(TableState state)
        {
            await LoadData(state);
            return new TableData<CaseListItemDto>() { TotalItems = totalItems, Items = pagedData };
        }

        private async Task LoadData(TableState state)
        {
            _loading = true;
            await GetPromissoryCasesNumberIfApplicable();
            await GetPartyCaseNumberIfApplicable();
            caseFilter.CaseNumbers = string.IsNullOrWhiteSpace(_caseNumber) ? _caseNumbersFromDb : _caseNumber.Split(',').ToArray();
            caseFilter.JudgeAcceptanceDateRange = JudgeAcceptanceDateRange.MapToDateRangeDto();
            caseFilter.ReceiveDateRange = ReceiveDateRange.MapToDateRangeDto();
            caseFilter.TotalRequiredAmountRange = TotalRequiredRange.MapToNumberRangeDto();
            SetRequestDetails(request, state, caseFilter);
            if (request.StateHasChanged())
            {
                var response = await CmsModule.ExecuteQueryAsync(new GetAllCasesQuery { PagedRequest = request });
                if (response.Succeeded)
                {
                    totalItems = response.TotalCount;
                    currentPage = response.CurrentPage;
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
            caseFilter.CourtCode = courtCode;
            divisionList = await _courtQueries.GetDivisionListByCourtCodeAsync(courtCode);
        }

        private void OnFilter()
        {
            table.ReloadServerData();
        }

        private void Clear()
        {
            caseFilter.Clear();
            _acceptancePicker.Clear();
            _receivePicker.Clear();
            _caseNumber = null;
            table.ReloadServerData();
        }

        private async Task ExportToExcel()
        {
            PagedRequest<CaseListItemDto> excelRequest = new();
            excelRequest.PageSize = totalItems;
            excelRequest.Filter = request.GetFilter();
            excelRequest.Sort = request.Sort;

            var result = await CmsModule.ExecuteQueryAsync(new ExportCasesToExcel { PagedRequest = excelRequest });
            var fileInfo = result.Data;
            await _jsRuntime.InvokeAsync<List<CaseListItemDto>>("saveAsExcel", $"{localizer[fileInfo.FileName]}.xlsx", Convert.ToBase64String(fileInfo.FileData));
        }

        public async Task GenerateExcel()
        {
            await ExportToExcel();
        }
    }
}