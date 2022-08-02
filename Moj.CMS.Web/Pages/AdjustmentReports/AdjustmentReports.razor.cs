using Moj.CMS.Web.Constants;
using MudBlazor;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Moj.CMS.Web.Pages.AdjustmentReports
{
    public partial class AdjustmentReports
    {
        protected override async Task OnInitializedAsync()
        {
            InitializeBreadcrumps(new ObservableCollection<BreadcrumbItem>
            {
                new BreadcrumbItem(Globallocalizer[ Urls.General.Tilte], href: "",disabled:true),
                new BreadcrumbItem(Globallocalizer[Urls.General.AdjustmentReports.Title], href: Urls.General.AdjustmentReports.Href, icon: Urls.General.AdjustmentReports.Icon),
            });
            await base.OnInitializedAsync();
        }
    }
}
