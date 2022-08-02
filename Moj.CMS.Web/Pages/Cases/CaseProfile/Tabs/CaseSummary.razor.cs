using Microsoft.AspNetCore.Components;
using Moj.CMS.Application.AppServices.Case.Queries.GetCaseSummary;
using Moj.CMS.Domain.Shared.Enums;
using MudBlazor;
using System.Threading.Tasks;

namespace Moj.CMS.Web.Pages.Cases.CaseProfile.Tabs
{
    public partial class CaseSummary
    {
        [Parameter] public int CaseId { get; set; }
        [Parameter] public bool Loading { get; set; }
        [Parameter] public EventCallback<int> OnActivePanelChanged { get; set; }
        [Parameter] public CaseSummaryDto CaseSummaryDto { get; set; }

        private Color StatusColor => CaseSummaryDto.CaseDetails?.StatusId == (int)CaseStatusEnum.Active ? Color.Secondary :
                CaseSummaryDto.CaseDetails?.StatusId == (int)CaseStatusEnum.InProgress ? Color.Warning :
                CaseSummaryDto.CaseDetails?.StatusId == (int)CaseStatusEnum.Closed ? Color.Dark :
                Color.Inherit;

        private Color TypeColor => CaseSummaryDto.CaseDetails?.TypeId == (int)CaseTypeEnum.Financial ? Color.Tertiary :
                CaseSummaryDto.CaseDetails?.TypeId == (int)CaseTypeEnum.Direct ? Color.Error :
                CaseSummaryDto.CaseDetails?.TypeId == (int)CaseTypeEnum.Personal ? Color.Primary :
                Color.Inherit;

        public async Task ChangeActivePanel(int index)
        {
            await OnActivePanelChanged.InvokeAsync(index);
        }

        private const int PartiesPanel = 2;
        private const int PromissoriesPanel = 3;
        private const int ClaimsPanel = 4;
        private const int BankAccountsPanel = 7;
        private const int CaseEventsPanel = 8;
    }
}
