using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Moj.CMS.Application.AppServices.Case.Queries.GetCaseEvents;
using Moj.CMS.Application.AppServices.CaseHistory.Queries;
using Moj.CMS.Shared.Models;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moj.CMS.Web.Pages.Cases.CaseProfile.Tabs
{
    public partial class CaseEventsDetails
    {
        [Parameter] public IEnumerable<CaseEventsDto> CaseEvents { get; set; }

        private int? OperationId { get; set; }
        private string UserName { get; set; }
        private IEnumerable<CaseEventsDto> FilteredCaseEvents { get; set; }
        private MudTable<CaseEventsDto> table = new MudTable<CaseEventsDto>();
        private IEnumerable<SelectListItem> _caseOperationList = new List<SelectListItem>();
        private MudDateRangePicker _picker;
        private DateRange DateRange { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _caseOperationList = await _lookupsService.GetCaseOperationListItemsAsync();
            FilteredCaseEvents = CaseEvents.ToList();
            await base.OnInitializedAsync();
        }

        private void OnFilter()
        {
            FilteredCaseEvents = CaseEvents.Where(caseEventItem =>
                (DateRange == null || caseEventItem.Date.Date >= DateRange.Start &&
                     (DateRange.End == null || caseEventItem.Date.Date <= DateRange.End))
                && (OperationId == null || caseEventItem.OperationId == OperationId.Value)
                && (string.IsNullOrEmpty(UserName) || caseEventItem.UserName.Contains(UserName))).ToList();
        }

        private void Clear()
        {
            OperationId = null;
            UserName = string.Empty;
            _picker.Clear();
            FilteredCaseEvents = CaseEvents;
        }

        private async Task ExportToExcel()
        {
            var result = await CmsModule.ExecuteQueryAsync(new ExportCaseEventToExcel { CaseEvents = FilteredCaseEvents });
            var fileInfo = result.Data;
            await _jsRuntime.InvokeAsync<List<CaseEventsDto>>("saveAsExcel", $"{Globallocalizer[fileInfo.FileName]}.xlsx", Convert.ToBase64String(fileInfo.FileData));
        }

        private async Task GenerateExcel()
        {
            await ExportToExcel();
        }
    }
}
