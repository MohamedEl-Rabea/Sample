using Microsoft.AspNetCore.Components;
using Moj.CMS.Application.AppServices.Case.Queries.GetCaseClaims;
using MudBlazor;
using System.Threading.Tasks;

namespace Moj.CMS.Web.Pages.Cases.CaseProfile.Tabs
{
    public partial class CaseClaims
    {
        [Parameter] public int CaseId { get; set; }
        private bool _Loading = true;
        public GetCaseClaimsDto caseClaimsDto;

        protected override async Task OnInitializedAsync()
        {
            await GetCaseClaims();
            await base.OnInitializedAsync();
        }

        private async Task GetCaseClaims()
        {
            _Loading = true;
            var result = await CmsModule.ExecuteQueryAsync(new GetCaseClaimsQuery { CaseId = CaseId });
            if (result.Succeeded)
            {
                caseClaimsDto = result.Data ?? new GetCaseClaimsDto();
                _Loading = false;
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