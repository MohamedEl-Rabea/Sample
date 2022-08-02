using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using Moj.CMS.Application.AppServices.SadadInvoice.Queries;
using Moj.CMS.Application.AppServices.SadadInvoice.Queries.GetAllSadadInvoice;
using Moj.CMS.Shared.Requests;
using Moj.CMS.Web.Constants;
using Moj.CMS.Web.Extensions;
using MudBlazor;
using SSS.Components.NumberRange;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Moj.CMS.Web.Pages.SadadInvoices
{
    public partial class SadadInvoices
    {
        private IEnumerable<SadadInvoiceDto> pagedData;
        private SadadInvoiceFilter SadadInvoiceFilter = new();
        private MudTable<SadadInvoiceDto> table;
        private int totalItems;
        private PagedRequest<SadadInvoiceDto> request = new();
        public bool FilterManagerOpen;
        public bool _loading = true;
        private MudDateRangePicker ExpiryDateDatePicker;
        private MudDateRangePicker IssueDateDatePicker;
        private NumberRange MinBillableAmountNumberRange;
        private NumberRange AmountNumberRange;
        private NumberRange PaidAmountNumberRange;
        private NumberRange RemainingAmountNumberRange;
        private DateRange IssueDateRange { get; set; } = new DateRange();
        private DateRange ExpiryDateRange { get; set; } = new DateRange();
        private DecimalRange MinBillableAmount { get; set; } = new DecimalRange();
        private DecimalRange Amount { get; set; } = new DecimalRange();
        private DecimalRange PaidAmount { get; set; } = new DecimalRange();
        private DecimalRange RemainingAmount { get; set; } = new DecimalRange();
        [Inject] public IStringLocalizer<SadadInvoices> Localizer { get; set; }

        void OpenFilterManager(bool value)
        {
            FilterManagerOpen = value;
        }
        protected override async Task OnInitializedAsync()
        {
            InitializeBreadcrumps(new ObservableCollection<BreadcrumbItem>
            {
                new BreadcrumbItem(Globallocalizer[ Urls.General.Tilte], href: "",disabled:true),
                new BreadcrumbItem(Globallocalizer[Urls.General.SadadInvoices.Title], href: Urls.General.SadadInvoices.Href, icon: Urls.General.Claims.Icon),
            });
            await base.OnInitializedAsync();
        }


        private async Task<TableData<SadadInvoiceDto>> ServerReload(TableState state)
        {
            await LoadData(state);
            return new TableData<SadadInvoiceDto>() { TotalItems = totalItems, Items = pagedData };
        }

        private async Task LoadData(TableState state)
        {
            SadadInvoiceFilter.MinimumPayment = MinBillableAmount.MapToNumberRangeDto();
            SadadInvoiceFilter.Amount = Amount.MapToNumberRangeDto();
            SadadInvoiceFilter.PaidAmount = PaidAmount.MapToNumberRangeDto();
            SadadInvoiceFilter.RemainingAmount = RemainingAmount.MapToNumberRangeDto();
            SadadInvoiceFilter.IssueDateRange = IssueDateRange.MapToDateRangeDto();
            SadadInvoiceFilter.ExpiryDateRange = ExpiryDateRange.MapToDateRangeDto();
            SetRequestDetails(request, state, SadadInvoiceFilter);
            if (request.StateHasChanged())
            {
                _loading = true;
                var response = await CmsModule.ExecuteQueryAsync(new GetSadadInvoiceQuery { PagedRequest = request });
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
            SadadInvoiceFilter.Clear();
            MinBillableAmountNumberRange.Clear();
            AmountNumberRange.Clear();
            PaidAmountNumberRange.Clear();
            RemainingAmountNumberRange.Clear();
            ExpiryDateDatePicker.Clear();
            IssueDateDatePicker.Clear();
            table.ReloadServerData();
        }

        private async Task ExportToExcel()
        {
            PagedRequest<SadadInvoiceDto> excelRequest = new();
            excelRequest.PageSize = totalItems;
            excelRequest.Filter = request.GetFilter();
            excelRequest.Sort = request.Sort;
            var result = await CmsModule.ExecuteQueryAsync(new ExportSadadInvoiceToExcel { PagedRequest = excelRequest });
            var fileInfo = result.Data;
            await _jsRuntime.InvokeAsync<List<SadadInvoiceDto>>("saveAsExcel", $"{Globallocalizer[fileInfo.FileName]}.xlsx", Convert.ToBase64String(fileInfo.FileData));
        }

        public async Task GenerateExcel()
        {
            await ExportToExcel();
        }
    }
}
