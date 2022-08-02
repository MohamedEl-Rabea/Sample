using Microsoft.AspNetCore.Components;
using Moj.CMS.Application.AppServices.Case.Queries.GetCaseClaims;
using Moj.CMS.Application.AppServices.Case.Queries.GetCaseCollectionChannels;
using System.Threading.Tasks;
using MudBlazor;

namespace Moj.CMS.Web.Pages.Cases.CaseProfile.Tabs
{
    public partial class CollectionChannels
    {
        [Parameter] public int CaseId { get; set; }

        public CollectionChannelsDto CollectionChannelsDto { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            await GetCaseClaims();
            await base.OnInitializedAsync();
        }

        private async Task GetCaseClaims()
        {
            var result = await CmsModule.ExecuteQueryAsync(new GetCaseCollectionChannelsQuery { CaseId = CaseId });
            if (result.Succeeded)
            {
                CollectionChannelsDto = result.Data ?? new CollectionChannelsDto();
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    _snackBar.Add(localizer[error], Severity.Error);
                }
            }
        }
    }
}
