using Moj.CMS.Web.Constants;
using MudBlazor;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Moj.CMS.Web.Pages.Settings.Notifications
{
    public partial class NotificationSettings
    {
        protected override Task OnInitializedAsync()
        {
            InitializeBreadcrumps(new ObservableCollection<BreadcrumbItem>
            {
                new BreadcrumbItem(Globallocalizer[ Urls.Settings.Tilte], href: "",disabled:true),
                new BreadcrumbItem(Globallocalizer[Urls.Settings.Notify.Title], href: Urls.Settings.Notify.Href, icon: Urls.Settings.Notify.Icon),
            });
            return base.OnInitializedAsync();
        }
    }
}
