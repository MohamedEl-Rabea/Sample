using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Moj.CMS.Shared;
using Moj.CMS.Shared.Constants.Role;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using Moj.CMS.UserAccess.Application.DTO;
using Moj.CMS.UserAccess.Application.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.UserAccess.Application.Services.Roles.Commands
{

    public class AddRoleCommand : Command<IResult>
    {
        public RoleDto RoleDto { get; set; }
    }

    public class AddRoleCommandHandler : IRequestHandler<AddRoleCommand, IResult>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IStringLocalizer<UsersLocalizer> _localizer;
        private readonly UserManager<CMSUser> _userManager;

        public AddRoleCommandHandler(RoleManager<IdentityRole> roleManager, UserManager<CMSUser> userManager, IStringLocalizer<UsersLocalizer> localizer)
        {
            _roleManager = roleManager;
            _localizer = localizer;
            _userManager = userManager;
        }

        public async Task<IResult> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.RoleDto.Id))
            {
                var existingRole = await _roleManager.FindByNameAsync(request.RoleDto.Name);
                if (existingRole != null) return Result.Fail(_localizer["Similar Role already exists."]);
                var response = await _roleManager.CreateAsync(new IdentityRole(request.RoleDto.Name));
                if (response.Succeeded)
                {
                    return Result.Success(_localizer["Created"]);
                }
                else
                {
                    return Result.Fail(response.Errors.Select(e => _localizer[e.Description].Value).ToList());
                }
            }
            else
            {
                var existingRole = await _roleManager.FindByIdAsync(request.RoleDto.Id);
                if (existingRole.Name == RoleConstants.AdministratorRole || existingRole.Name == RoleConstants.BasicRole)
                {
                    return Result.Fail($"{_localizer["Not allowed to modify"]} {existingRole.Name} {_localizer["Role"]}.");
                }
                existingRole.Name = request.RoleDto.Name;
                existingRole.NormalizedName = request.RoleDto.Name.ToUpper();
                await _roleManager.UpdateAsync(existingRole);
                return Result.Success(_localizer["Updated"]);
            }
        }
    }
}
