using Microsoft.AspNetCore.Components;
using Moj.CMS.Application.AppServices.Party.Queries;
using Moj.CMS.Application.AppServices.Party.Queries.Dtos;
using MudBlazor;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moj.CMS.Web.Pages.Parties.PartyProfile.Tabs
{
    public partial class PartyAccountStatement
    {
        [Parameter] public int PartyId { get; set; }
        public IEnumerable<PartyAccountStatementDto> partyAccountStatement { get; set; } = new List<PartyAccountStatementDto>();
        private MudTable<PartyAccountStatementDto> table;
        private bool _loading = true;
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await LoadData();
        }
        private async Task LoadData()
        {
            var accountStatments = await CmsModule.ExecuteQueryAsync(new GetPartyAccountStatementQuery { PartyId = PartyId });
            if (accountStatments.Succeeded)
            {
                partyAccountStatement = accountStatments.Data ?? partyAccountStatement;
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
