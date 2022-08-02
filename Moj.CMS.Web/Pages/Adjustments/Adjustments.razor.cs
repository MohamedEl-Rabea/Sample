using Moj.CMS.Web.Constants;
using MudBlazor;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Moj.CMS.Web.Pages.Adjustments
{
    public partial class Adjustments
    {

        protected override async Task OnInitializedAsync()
        {
            InitializeBreadcrumps(new ObservableCollection<BreadcrumbItem>
            {
                new BreadcrumbItem(Globallocalizer[Urls.General.Tilte], href: "",disabled:true),
                new BreadcrumbItem(Globallocalizer[Urls.General.Adjustments.Title], href: Urls.General.Adjustments.Href, icon: Urls.General.Adjustments.Icon),
            });
            await base.OnInitializedAsync();
        }
    }
}