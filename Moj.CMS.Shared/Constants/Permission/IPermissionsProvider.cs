using System.Collections.Generic;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Shared.Constants.Permission
{
    [ScopedService]
    public interface IPermissionsProvider
    {
        IEnumerable<Permission> GetAllPermissions();
        Permission GetPermission(string permissionCode);
        string GetPermissionParentCode(string permissionCode);
    }
}