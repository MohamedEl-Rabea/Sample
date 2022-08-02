using Microsoft.AspNetCore.Components;
using Moj.CMS.Application.AppServices.Case.Queries.GetEffectsAndDiscount;
using MudBlazor;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moj.CMS.Web.Pages.Cases.CaseProfile.Tabs
{
    public partial class EffectsAndDiscounts
    {
        [Parameter] public int CaseId { get; set; }

        private MudTable<GetEffectsAndDiscountDto> table;
        private bool _loading = true;
        public List<GetEffectsAndDiscountDto> effectAndDiscountList = new();

        protected override async Task OnInitializedAsync()
        {
            await GetCaseClaimEffects();
            await base.OnInitializedAsync();
        }

        private async Task GetCaseClaimEffects()
        {
            _loading = true;
            var result = await CmsModule.ExecuteQueryAsync(new GetClaimEffectsQuery { CaseId = CaseId });
            if (result.Succeeded)
            {
                effectAndDiscountList = result.Data?.ToList() ?? effectAndDiscountList;
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
