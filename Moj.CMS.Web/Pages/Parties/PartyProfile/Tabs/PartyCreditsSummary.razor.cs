using Microsoft.AspNetCore.Components;
using Moj.CMS.Application.AppServices.Party.Queries;
using MudBlazor;
using System.Threading.Tasks;

namespace Moj.CMS.Web.Pages.Parties.PartyProfile.Tabs
{
    public partial class PartyCreditsSummary
    {
        [Parameter] public int PartyId { get; set; }
        public PartyCreditsSummaryDto partyCreditsSummary { get; set; } = new PartyCreditsSummaryDto();
        private bool _loading = true;

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
            await base.OnInitializedAsync();
        }

        private async Task LoadData()
        {
            var partyCredits = await CmsModule.ExecuteQueryAsync(new GetPartyCreditsSummaryQuery { PartyId = PartyId });
            if (partyCredits.Succeeded)
            {
                partyCreditsSummary = partyCredits.Data ?? partyCreditsSummary;
            }
            else
            {
                foreach (var error in partyCredits.Errors)
                {
                    _snackBar.Add(localizer[error], Severity.Error);
                }
            }
            _loading = false;
        }
    }
}
