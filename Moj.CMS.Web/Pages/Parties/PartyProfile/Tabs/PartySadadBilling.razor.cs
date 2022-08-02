using Microsoft.AspNetCore.Components;
using Moj.CMS.Application.AppServices.Party.Queries.GetPartySadadBilling;
using MudBlazor;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moj.CMS.Web.Pages.Parties.PartyProfile.Tabs
{
    public partial class PartySadadBilling
    {
        [Parameter] public int PartyId { get; set; }
        public IEnumerable<PartySadadBillingDto> partySadadBillingList { get; set; } = new List<PartySadadBillingDto>();
        private MudTable<PartySadadBillingDto> table;
        private bool _loading = true;
        protected override async Task OnInitializedAsync()
        {
            await LoadData();
            await base.OnInitializedAsync();
        }

        private async Task LoadData()
        {
            var partyBasicDetails = await CmsModule.ExecuteQueryAsync(new GetPartySadadBillingQuery { PartyId = PartyId });
            if (partyBasicDetails.Succeeded)
            {
                partySadadBillingList = partyBasicDetails.Data ?? partySadadBillingList;
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
