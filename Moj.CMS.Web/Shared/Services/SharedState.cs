using MudBlazor;
using System.Collections.Generic;

namespace Moj.CMS.Web.Shared.Services
{
    public class SharedState : ISharedState
    {
        public SharedState()
        {
            BreadcrumbItems = new List<BreadcrumbItem>();
        }

        public List<BreadcrumbItem> BreadcrumbItems { get; set; }
    }
}