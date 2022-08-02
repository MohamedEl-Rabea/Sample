using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Moj.CMS.Shared;

namespace Moj.CMS.Web.Pages.UserManagement
{
    public class UserManagementComponentBase : AppComponentBase
    {
        [Inject] protected IStringLocalizer<UsersLocalizer> localizer { get; set; }
    }
}
