using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Moj.CMS.Shared;
using Moj.CMS.Shared.Constants.Role;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using Moj.CMS.UserAccess.Application.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.UserAccess.Application.Services.Roles.Commands
{
    public class DeleteRoleCommand : Command<IResult>
    {
        public string RoleId { get; set; }
    }

    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, IResult>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IStringLocalizer<UsersLocalizer> _localizer;
        private readonly UserManager<CMSUser> _userManager;

        public DeleteRoleCommandHandler(RoleManager<IdentityRole> roleManager, UserManager<CMSUser> userManager, IStringLocalizer<UsersLocalizer> localizer)
        {
            _roleManager = roleManager;
            _localizer = localizer;
            _userManager = userManager;
        }

        public async Task<IResult> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var existingRole = await _roleManager.FindByIdAsync(request.RoleId);
            if (existingRole.Name != RoleConstants.AdministratorRole && existingRole.Name != RoleConstants.BasicRole)
            {
                bool roleIsNotUsed = (await _userManager.GetUsersInRoleAsync(existingRole.Name)).Count() == 0;
                if (roleIsNotUsed)
                {
                    await _roleManager.DeleteAsync(existingRole);
                    return Result.Success($"{_localizer["deleted"]}.");
                }
                else
                {
                    return Result.Fail($"{_localizer["Not allowed to delete"]} {existingRole.Name} {_localizer["Role as it is being used."]}");
                }
            }
            else
            {
                return Result.Fail($"{_localizer["Not allowed to delete"]} {existingRole.Name}.");
            }
        }
    }
}
