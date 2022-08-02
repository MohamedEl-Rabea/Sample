using Microsoft.AspNetCore.Components;
using Moj.CMS.Application.AppServices.Party.Queries.GetPartyBankAccounts;
using MudBlazor;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moj.CMS.Web.Pages.Parties.PartyProfile.Tabs
{
    public partial class PartyBankAccounts
    {
        [Parameter] public int PartyId { get; set; }
        public IEnumerable<PartyBankAccountDto> partyBankAccountList { get; set; } = new List<PartyBankAccountDto>();
        private MudTable<PartyBankAccountDto> table;
        private bool _loading = true;
        protected override async Task OnInitializedAsync()
        {
            await LoadData();
            await base.OnInitializedAsync();
        }

        private async Task LoadData()
        {
            var accountStatments = await CmsModule.ExecuteQueryAsync(new GetPartyBankAccountsQuery { PartyId = PartyId });
            if (accountStatments.Succeeded)
            {
                partyBankAccountList = accountStatments.Data ?? partyBankAccountList;
            }
            else
            {
                foreach (var error in accountStatments.Errors)
                {
                    _snackBar.Add(localizer[error], Severity.Error);
                }
            }
            _loading = false;
        }
    }
}
