using Microsoft.AspNetCore.Authorization;
using Moj.CMS.Shared.Permission;
using System.Threading.Tasks;

namespace Moj.CMS.Shared.Testing
{
    public class TestAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
