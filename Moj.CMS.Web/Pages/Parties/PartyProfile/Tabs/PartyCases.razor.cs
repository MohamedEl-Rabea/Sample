using Microsoft.AspNetCore.Components;
using Moj.CMS.Application.AppServices.Case.Queries.GetAllCases;
using Moj.CMS.Application.AppServices.Party.Queries;
using Moj.CMS.Application.AppServices.Party.Queries.Dtos;
using Moj.CMS.Domain.Shared.Enums;
using MudBlazor;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moj.CMS.Web.Pages.Parties.PartyProfile.Tabs
{
    public partial class PartyCases
    {
        [Parameter] public int PartyId { get; set; }
        public IEnumerable<PartyCaseListItemDto> complaintPartyCases { get; set; } = new List<PartyCaseListItemDto>();
        public IEnumerable<PartyCaseListItemDto> accusedPartyCases { get; set; } = new List<PartyCaseListItemDto>();

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
            await base.OnInitializedAsync();
        }

        private async Task LoadData()
        {
            var allPartyCases = await CmsModule.ExecuteQueryAsync(new GetPartyCasesQuery { PartyId = PartyId });
            if (allPartyCases.Succeeded)
            {
                var allCases = allPartyCases.Data;
                if (allCases != null)
                {
                    complaintPartyCases = allCases.ComplaintCaseList;
                    accusedPartyCases = allCases.AccusedCaseList;
                }
            }
            else
            {
                foreach (var error in allPartyCases.Errors)
                {
                    _snackBar.Add(localizer[error], Severity.Error);
                }
            }
        }
    }
}
