using Microsoft.AspNetCore.Components;
using Moj.CMS.Application.AppServices.Case.Queries.GetCaseParties;
using MudBlazor;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moj.CMS.Web.Pages.Cases.CaseProfile.Tabs
{
    public partial class CaseParties
    {
        [Parameter] public int CaseId { get; set; }
        private bool _loading = true;
        private CasePartiesDto CasePartiesDto { get; set; } = new CasePartiesDto
        {
            Summary = new PartyRoleSummary()
        };
        private IEnumerable<PartyDto> _parties = new List<PartyDto>();
        private MudTable<PartyDto> table = new MudTable<PartyDto>();

        protected override async Task OnInitializedAsync()
        {
            await GetCaseInfoASync();
            await base.OnInitializedAsync();
            _loading = false;
        }

        private async Task GetCaseInfoASync()
        {
            var caseParties = await _mediator.Send(new GetCasePartiesQuery() { CaseId = CaseId });
            if (caseParties.Succeeded)
            {
                CasePartiesDto = caseParties.Data ?? CasePartiesDto;
                _parties = CasePartiesDto.Parties;
            }
            else
            {
                foreach (var error in caseParties.Errors)
                {
                    _snackBar.Add(localizer[error], Severity.Error);
                }
            }
        }
    }
}
