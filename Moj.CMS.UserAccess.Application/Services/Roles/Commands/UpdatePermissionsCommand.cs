using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Moj.CMS.Shared;
using Moj.CMS.Shared.Constants.Permission;
using Moj.CMS.Shared.Constants.Role;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Runtime;
using Moj.CMS.Shared.Wrapper;
using Moj.CMS.UserAccess.Application.DTO;
using Moj.CMS.UserAccess.Application.Extensions;
using Moj.CMS.UserAccess.Application.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.UserAccess.Application.Services.Roles.Commands
{

    public class UpdatePermissionsCommand : Command<IResult<string>>
    {
        public PermissionRequestDto PermissionRequestDto { get; set; }
    }

    public class UpdatePermissionsCommandHandler : IRequestHandler<UpdatePermissionsCommand, IResult<string>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IApplicationSession _appSession;
        private readonly UserManager<CMSUser> _userManager;
        private readonly IStringLocalizer<UsersLocalizer> _localizer;
        public UpdatePermissionsCommandHandler(RoleManager<IdentityRole> roleManager, IStringLocalizer<UsersLocalizer> localizer, UserManager<CMSUser> userManager, IApplicationSession appSession)
        {
            _roleManager = roleManager;
            _localizer = localizer;
            _userManager = userManager;
            _appSession = appSession;
        }

        public async Task<IResult<string>> Handle(UpdatePermissionsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.PermissionRequestDto.RoleId))
                {
                    return Result<string>.Fail(_localizer["Role is required."]);
                }
                var role = await _roleManager.FindByIdAsync(request.PermissionRequestDto.RoleId);
                if (role.Name == RoleConstants.AdministratorRole)
                {
                    var currentUser = await _userManager.Users.SingleAsync(x => x.Id == _appSession.UserId);
                    if (!await _userManager.IsInRoleAsync(currentUser, RoleConstants.AdministratorRole))
                    {
                        return Result<string>.Fail(_localizer["Not allowed to modify Permissions for this Role."]);
                    }
                }
                var userPermissionClaims = (await _roleManager.GetClaimsAsync(role)).Where(c => c.Type == ApplicationClaimTypes.Permission);
                foreach (var permissionClaim in userPermissionClaims)
                {
                    await _roleManager.RemoveClaimAsync(role, permissionClaim);
                }
                foreach (var permission in request.PermissionRequestDto.RolePermissions)
                {
                    await _roleManager.AddPermissionClaim(role, permission);
                }
                return Result<string>.Success(_localizer["Updated"]);
            }
            catch (Exception ex)
            {
                return Result<string>.Fail(ex.Message);
            }
        }
    }
}
