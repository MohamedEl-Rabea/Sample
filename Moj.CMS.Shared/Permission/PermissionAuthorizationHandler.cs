using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Moj.CMS.Shared.Constants.Permission;

namespace Moj.CMS.Shared.Permission
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IPermissionsProvider _permissionsProvider;

        public PermissionAuthorizationHandler(IPermissionsProvider permissionsProvider)
        {
            _permissionsProvider = permissionsProvider;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User == null)
            {
                await Task.CompletedTask;
            }

            var permissionParentCode = _permissionsProvider.GetPermissionParentCode(requirement.Permission);
            var hasPermission = context.User.Claims.Where(claim => claim.Type == ApplicationClaimTypes.Permission).Any
                                                               (c => c.Value == requirement.Permission || c.Value == permissionParentCode);
            if (hasPermission)
            {
                context.Succeed(requirement);
                await Task.CompletedTask;
            }
        }
    }
}