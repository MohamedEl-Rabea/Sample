using Microsoft.AspNetCore.Components;
using Moj.CMS.Application.AppServices.Case.Queries.GetCasePromissories;
using MudBlazor;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moj.CMS.Web.Pages.Cases.CaseProfile.Tabs
{
    public partial class CasePromissories
    {
        [Parameter] public int CaseId { get; set; }

        private MudTable<GetCasePromissoriesDto> table;
        private bool _loading = true;
        public List<GetCasePromissoriesDto> casePromissoryList = new();

        protected override async Task OnInitializedAsync()
        {
            await GetCasePromissories();
            await base.OnInitializedAsync();
        }

        private async Task GetCasePromissories()
        {
            _loading = true;
            var result = await CmsModule.ExecuteQueryAsync(new GetCasePromissoriesQuery { CaseId = CaseId });
            if (result.Succeeded)
            {
                casePromissoryList = result.Data.ToList() ?? casePromissoryList;
                _loading = false;
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
