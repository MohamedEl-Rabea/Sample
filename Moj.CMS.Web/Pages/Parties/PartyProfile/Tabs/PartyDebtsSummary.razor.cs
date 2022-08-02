using Microsoft.AspNetCore.Components;
using Moj.CMS.Application.AppServices.Party.Queries;
using MudBlazor;
using System.Threading.Tasks;

namespace Moj.CMS.Web.Pages.Parties.PartyProfile.Tabs
{
    public partial class PartyDebtsSummary
    {
        [Parameter] public int PartyId { get; set; }
        public PartyDebtsSummaryDto  partyDebtsSummary { get; set; } = new PartyDebtsSummaryDto();
        private bool _loading = true;

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
            await base.OnInitializedAsync();
        }

        private async Task LoadData()
        {
            var partyDebts = await CmsModule.ExecuteQueryAsync(new GetPartyDebtsSummaryQuery { PartyId = PartyId });
            if (partyDebts.Succeeded)
            {
                partyDebtsSummary = partyDebts.Data ?? partyDebtsSummary;
            }
            else
            {
                foreach (var error in partyDebts.Errors)
                {
                    _snackBar.Add(localizer[error], Severity.Error);
                }
            }
            _loading = false;
        }
    }
}
