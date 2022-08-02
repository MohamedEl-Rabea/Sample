using Moj.CMS.Web.Constants;
using MudBlazor;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Moj.CMS.Web.Pages.Audit.AuditLogs
{
    public partial class Logs
    {
        protected override async Task OnInitializedAsync()
        {
            InitializeBreadcrumps(new ObservableCollection<BreadcrumbItem>
            {
                new BreadcrumbItem(Globallocalizer[Urls.Administration.Tilte], href: "",disabled:true),
                new BreadcrumbItem(Globallocalizer[Urls.Administration.Logs.Title], href: Urls.Administration.Logs.Href, icon: Urls.Administration.Logs.Icon),
            });
            await base.OnInitializedAsync();
        }
    }
}
