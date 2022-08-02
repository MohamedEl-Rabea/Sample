using Moj.CMS.Web.Constants;
using MudBlazor;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Moj.CMS.Web.Pages.SeizingOrders
{
    public partial class SeizingOrders
    {
        protected override async Task OnInitializedAsync()
        {
            InitializeBreadcrumps(new ObservableCollection<BreadcrumbItem>
            {
                new BreadcrumbItem(Globallocalizer[Urls.General.Tilte], href: "",disabled:true),
                new BreadcrumbItem(Globallocalizer[Urls.General.SeizingOrders.Title], href: Urls.General.SeizingOrders.Href, icon: Urls.General.SeizingOrders.Icon),
            });
            await base.OnInitializedAsync();
        }
    }
}