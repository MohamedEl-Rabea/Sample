using Microsoft.AspNetCore.Components;
using Moj.CMS.Shared.Models;
using Moj.CMS.Shared.Queries;
using MudBlazor;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moj.CMS.Web.Pages.Audit.AuditLogs
{
    public partial class EntityDetailsModal
    {
        [Inject] 
        public ILogsQueries LogsQueries { get; set; }
        
        [CascadingParameter]
        private MudDialogInstance MudDialog { get; set; }

        [Parameter]
        public EntityHistoryDto EntityHistory { get; set; }
        
        [Parameter]
        public string RequestId { get; set; }

        public bool Loading = false;
        public List<EntityChangesDto> TableData { get; set; } = new List<EntityChangesDto>();

        public void Cancel()
        {
            MudDialog.Cancel();
        }

        protected override async Task OnInitializedAsync()
        {
            Loading = true;
            EntityHistory ??= await LogsQueries.GetEntityHistoryByRequestId(RequestId);
            if (EntityHistory != null)
                TableData = EntityHistory.GetEntityChanges();

            Loading = false;
            await base.OnInitializedAsync();
        }
    }
}
