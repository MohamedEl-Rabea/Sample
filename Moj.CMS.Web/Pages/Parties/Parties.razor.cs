using Microsoft.JSInterop;
using Moj.CMS.Application.AppServices.Party.Queries;
using Moj.CMS.Shared.Models;
using Moj.CMS.Shared.Requests;
using Moj.CMS.Web.Constants;
using Moj.CMS.Web.Extensions;
using MudBlazor;
using SSS.Components.NumberRange;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Moj.CMS.Web.Pages.Parties
{
    public partial class Parties
    {
        private IEnumerable<PartyListItemDto> pagedData;
        private IEnumerable<SelectListItem> partyTypeList = new List<SelectListItem>();
        private IEnumerable<SelectListItem> partyIdentitytypeList = new List<SelectListItem>();
        private IEnumerable<SelectListItem> NationalityList = new List<SelectListItem>();
        private PartyFilter PartyFilter = new();
        private MudTable<PartyListItemDto> table;
        private int totalItems;
        private PagedRequest<PartyListItemDto> request = new();
        public bool _loading = true;
        public bool FilterManagerOpen;
        private DecimalRange TotalCreditRange { get; set; } = new DecimalRange();
        private DecimalRange TotalDebtRange { get; set; } = new DecimalRange();
        protected override async Task OnInitializedAsync()
        {
            await InitializeLookups();
            await base.OnInitializedAsync();
        }

        private async Task InitializeLookups()
        {
            InitializeBreadcrumps(new ObservableCollection<BreadcrumbItem>
            {
                new BreadcrumbItem(Globallocalizer[ Urls.General.Tilte], href: "",disabled:true),
                new BreadcrumbItem(Globallocalizer[Urls.General.Parties.Title], href: Urls.General.Parties.Href, icon: Urls.General.Parties.Icon),
            });
            partyTypeList = await _lookupsService.GetPartyTypeListItemsAsync();
            partyIdentitytypeList = await _lookupsService.GetPartyIdentityTypeListItemsAsync();
            NationalityList = await _lookupsService.GetNationalityList();
        }

        void OpenFilterManager(bool value)
        {
            FilterManagerOpen = value;
        }

        private async Task<TableData<PartyListItemDto>> ServerReload(TableState state)
        {
            await LoadData(state);
            return new TableData<PartyListItemDto>() { TotalItems = totalItems, Items = pagedData };
        }
        private async Task LoadData(TableState state)
        {
            PartyFilter.TotalCreditAmount = TotalCreditRange.MapToNumberRangeDto();
            PartyFilter.TotalDebtAmount = TotalDebtRange.MapToNumberRangeDto();
            SetRequestDetails(request, state, PartyFilter);
            if (request.StateHasChanged())
            {
                _loading = true;
                var response = await CmsModule.ExecuteQueryAsync(new GetPagedPartiesQuery { PagedRequest = request });
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

        private void OnFilter()
        {
            table.ReloadServerData();
        }

        private void Clear()
        {
            PartyFilter.Clear();
            TotalCreditRange = null;
            TotalDebtRange = null;
            table.ReloadServerData();
        }

        private async Task ExportToExcel()
        {
            PagedRequest<PartyListItemDto> excelRequest = new();
            excelRequest.PageSize = totalItems;
            excelRequest.Filter = request.GetFilter();
            excelRequest.Sort = request.Sort;
            excelRequest.Filter.TotalCreditAmount = TotalCreditRange.MapToNumberRangeDto();
            excelRequest.Filter.TotalDebtAmount = TotalDebtRange.MapToNumberRangeDto();
            var fileInfo = await _mediator.Send(new ExportPartyToExcel() { PagedRequest = excelRequest });
            await _jsRuntime.InvokeAsync<List<PartyListItemDto>>("saveAsExcel", $"{localizer[fileInfo.FileName]}.xlsx", Convert.ToBase64String(fileInfo.FileData));
        }
    }
}