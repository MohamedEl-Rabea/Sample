using Microsoft.AspNetCore.Components;
using Moj.CMS.Application.AppServices.Case.Queries.GetCaseEvents;
using MudBlazor;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moj.CMS.Web.Pages.Cases.CaseProfile.Tabs
{
    public partial class CaseEvents
    {
        [Parameter] public int CaseId { get; set; }
        public IEnumerable<CaseEventsDto> CaseEventsList { get; set; } = new List<CaseEventsDto>();

        private bool _showTimeline = true;

        protected override async Task OnInitializedAsync()
        {
            await GetCaseEvents();
            await base.OnInitializedAsync();
        }
        private async Task GetCaseEvents()
        {
            var caseEventsResult = await CmsModule.ExecuteQueryAsync(new GetCaseEventsQuery { CaseId = CaseId });
            if (caseEventsResult.Succeeded)
            {
                CaseEventsList = caseEventsResult.Data.ToList();
            }
            else
            {
                foreach (var error in caseEventsResult.Errors)
                {
                    _snackBar.Add(localizer[error], Severity.Error);
                }
            }
        }

    }
}
