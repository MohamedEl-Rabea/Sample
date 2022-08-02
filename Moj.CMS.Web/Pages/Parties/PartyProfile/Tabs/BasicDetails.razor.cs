using Microsoft.AspNetCore.Components;
using Moj.CMS.Application.AppServices.Party.Queries;
using MudBlazor;
using System.Threading.Tasks;

namespace Moj.CMS.Web.Pages.Parties.PartyProfile.Tabs
{
    public partial class BasicDetails
    {
        [Parameter] public int PartyId { get; set; }
        public PartyListItemDto partyDto { get; set; } = new PartyListItemDto();
        private bool _loading = true;
        protected override async Task OnInitializedAsync()
        {
            await LoadData();
            await base.OnInitializedAsync();
        }
        private async Task LoadData()
        {
            var partyBasicDetails = await CmsModule.ExecuteQueryAsync(new GetPartyBasicDetailsQuery { PartyId = PartyId });
            if (partyBasicDetails.Succeeded)
            {
                partyDto = partyBasicDetails.Data ?? partyDto;
            }
            else
            {
                foreach (var error in partyBasicDetails.Errors)
                {
                    _snackBar.Add(localizer[error], Severity.Error);
                }
            }
            _loading = false;
        }
    }
}
