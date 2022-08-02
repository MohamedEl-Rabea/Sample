using MediatR;
using Microsoft.AspNetCore.Identity;
using Moj.CMS.Shared.Constants.Permission;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using Moj.CMS.UserAccess.Application.DTO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.UserAccess.Application.Services.Roles.Queries
{

    public class GetAllPermissionsQuery : Query<Result<PermissionResponseDto>>
    {
        public string RoleId { get; set; }
    }

    public class GetAllPermissionsQueryHandler : IRequestHandler<GetAllPermissionsQuery, Result<PermissionResponseDto>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IPermissionsProvider _permissionsProvider;
        public GetAllPermissionsQueryHandler(RoleManager<IdentityRole> roleManager, IPermissionsProvider permissionsProvider)
        {
            _roleManager = roleManager;
            _permissionsProvider = permissionsProvider;
        }
        public async Task<Result<PermissionResponseDto>> Handle(GetAllPermissionsQuery query, CancellationToken cancellationToken)
        {
            var result = new PermissionResponseDto();

            var allPermissions = _permissionsProvider.GetAllPermissions().Aggregate(Enumerable.Empty<PermissionDto>(), (permissionsList, item) =>
             permissionsList.Append(new PermissionDto
             {
                 Code = item.Code,
                 Name = item.Name,
                 Selected = false,
                 ParentCode = string.Empty
             })
             .Concat(item.Permissions.Select(childPermission => new PermissionDto
             {
                 Code = childPermission.Code,
                 Name = childPermission.Name,
                 Selected = false,
                 ParentCode = item.Code
             }))).ToList();

            var role = await _roleManager.FindByIdAsync(query.RoleId);
            if (role != null)
            {
                result.RoleId = role.Id;
                result.RoleName = role.Name;
                var userClaims = await _roleManager.GetClaimsAsync(role);
                var userPermissionClaims = userClaims.Where(c => c.Type == ApplicationClaimTypes.Permission).Select(a => a.Value).ToList();
                foreach (var permission in allPermissions)
                {
                    permission.Selected = userPermissionClaims.Contains(permission.Code);
                }
            }

            result.Permissions = allPermissions.ToList();
            return Result<PermissionResponseDto>.Success(result);
        }
    }
}
