using MudBlazor;
using System.Collections.Generic;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Web.Shared.Services
{
    [ScopedService]
    public interface ISharedState
    {
        List<BreadcrumbItem> BreadcrumbItems { get; set; }
    }
}
