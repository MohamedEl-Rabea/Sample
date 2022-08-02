using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Moj.CMS.Shared.Constants.Permission;

namespace Moj.CMS.UserAccess.Application.Extensions
{
    public static class ClaimsHelper
    {
        public static async Task AddPermissionClaim(this RoleManager<IdentityRole> roleManager, IdentityRole role, string permission)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            if (!allClaims.Any(a => a.Type == ApplicationClaimTypes.Permission && a.Value == permission))
            {
                await roleManager.AddClaimAsync(role, new Claim(ApplicationClaimTypes.Permission, permission));
            }
        }

        public static async Task GeneratePermissionClaims(this RoleManager<IdentityRole> roleManager, IdentityRole role, IPermissionsProvider permissionsProvider)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            foreach (var permission in permissionsProvider.GetAllPermissions())
            {
                if (!allClaims.Any(a => a.Type == ApplicationClaimTypes.Permission && a.Value == permission.Code))
                {
                    await roleManager.AddClaimAsync(role, new Claim(ApplicationClaimTypes.Permission, permission.Code));
                }
            }
        }

        public static async Task AddCustomPermissionClaim(this RoleManager<IdentityRole> roleManager, IdentityRole role, string permission)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            if (!allClaims.Any(a => a.Type == ApplicationClaimTypes.Permission && a.Value == permission))
            {
                await roleManager.AddClaimAsync(role, new Claim(ApplicationClaimTypes.Permission, permission));
            }
        }
    }
}