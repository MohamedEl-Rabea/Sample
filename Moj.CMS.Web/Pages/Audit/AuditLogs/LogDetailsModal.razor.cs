using Microsoft.AspNetCore.Components;
using Moj.CMS.Shared.Models;
using MudBlazor;

namespace Moj.CMS.Web.Pages.Audit.AuditLogs
{
    public partial class LogDetailsModal
    {
        [CascadingParameter]
        private MudDialogInstance MudDialog { get; set; }

        [CascadingParameter]
        public bool Rtl { get; set; }

        [Parameter]
        public Log LogDetails { get; set; }

        public void Cancel()
        {
            MudDialog.Cancel();
        }

        protected override void OnInitialized()
        {
        }
    }
}
