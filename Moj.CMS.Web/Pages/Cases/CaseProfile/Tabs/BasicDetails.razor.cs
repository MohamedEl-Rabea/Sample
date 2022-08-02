using Microsoft.AspNetCore.Components;
using Moj.CMS.Application.AppServices.Case.Queries.GetCaseBasicDetails;
using MudBlazor;
using System.Threading.Tasks;

namespace Moj.CMS.Web.Pages.Cases.CaseProfile.Tabs
{
    public partial class BasicDetails
    {
        [Parameter] public int CaseId { get; set; }
        private bool _loading = true;

        public CaseBasicDetailsDto CaseBasicDetailsDto { get; set; } = new CaseBasicDetailsDto();

        protected override async Task OnInitializedAsync()
        {
            var caseBasicDetails = await CmsModule.ExecuteQueryAsync(new GetCaseBasicDetailsQuery { CaseId = CaseId });
            if (caseBasicDetails.Succeeded)
            {
                CaseBasicDetailsDto = caseBasicDetails.Data ?? CaseBasicDetailsDto;
            }
            else
            {
                foreach (var error in caseBasicDetails.Errors)
                {
                    _snackBar.Add(localizer[error], Severity.Error);
                }
            }
            await base.OnInitializedAsync();
            _loading = false;
        }
    }
}
