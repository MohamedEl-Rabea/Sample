using Microsoft.AspNetCore.Components;
using Moj.CMS.Application.AppServices.Case.Queries.GetCaseSummary;
using Moj.CMS.Shared.DTO;
using Moj.CMS.Web.Constants;
using MudBlazor;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Moj.CMS.Web.Pages.Cases.CaseProfile
{
    public partial class CaseProfile : IDisposable
    {
        [Parameter] public int CaseId { get; set; }
        public int CurrentActiveTabIndex { get; set; }
        private bool _loading = true;

        private CaseSummaryDto CaseSummaryDto { get; set; } = new CaseSummaryDto
        {
            CaseDetails = new DetailsDto(),
            FinancialDetails = new FinancialDto
            {
                CurrentAmount = new MoneyDto(),
                RemainingAmount = new MoneyDto()
            }
        };

        private bool _caseExists;

        protected override async Task OnInitializedAsync()
        {
            await GetCaseSummary();
            await base.OnInitializedAsync();
            _loading = false;
        }

        private async Task GetCaseSummary()
        {
            var caseSummary = await CmsModule.ExecuteQueryAsync(new GetCaseSummaryQuery() { CaseId = CaseId });
            if (caseSummary.Succeeded)
            {
                CaseSummaryDto = caseSummary.Data ?? CaseSummaryDto;
                CaseSummaryDto.FinancialDetails = CaseSummaryDto.FinancialDetails ?? new FinancialDto
                {
                    CurrentAmount = new MoneyDto(),
                    RemainingAmount = new MoneyDto()
                };
                _caseExists = !string.IsNullOrWhiteSpace(CaseSummaryDto.CaseDetails?.CaseNumber);
            }
            else
            {
                foreach (var error in caseSummary.Errors)
                {
                    _snackBar.Add(localizer[error], Severity.Error);
                }
            }
        }

        protected void ActivePanelChanged(int index)
        {
            CurrentActiveTabIndex = index;
        }

        public void Dispose()
        {
            SharedState.BreadcrumbItems = SharedState.BreadcrumbItems.Where(x => !x.Href.Contains(Urls.General.Cases.ProfileHref)).ToList();
        }
    }
}
