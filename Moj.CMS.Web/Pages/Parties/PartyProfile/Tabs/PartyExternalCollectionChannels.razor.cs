using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Moj.CMS.Web.Pages.Parties.PartyProfile.Tabs
{
    public partial class PartyExternalCollectionChannels
    {
        [Parameter] public int PartyId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }
    }
}
