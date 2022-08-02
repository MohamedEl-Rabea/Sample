using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

namespace Moj.CMS.UserAccess.Application.Services.Users.Commands
{
    public class ToggleUserStatusCommand : Command<IResult>
    {
        public ToggleUserStatusDto ToggleUserStatusDto { get; set; }
    }

    public class ToggleUserStatusCommandHandler : IRequestHandler<ToggleUserStatusCommand, IResult>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IStringLocalizer<UsersLocalizer> _localizer;
        private readonly UserManager<CMSUser> _userManager;

        public ToggleUserStatusCommandHandler(RoleManager<IdentityRole> roleManager, UserManager<CMSUser> userManager, IStringLocalizer<UsersLocalizer> localizer)
        {
            _roleManager = roleManager;
            _localizer = localizer;
            _userManager = userManager;
        }

        public async Task<IResult> Handle(ToggleUserStatusCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.Where(u => u.Id == request.ToggleUserStatusDto.UserId).FirstOrDefaultAsync();
            var isAdmin = await _userManager.IsInRoleAsync(user, RoleConstants.AdministratorRole);
            if (isAdmin)
            {
                return Result<string>.Fail(_localizer["Administrators Profile's Status cannot be toggled"]);
            }
            if (user != null)
            {
                user.IsActive = request.ToggleUserStatusDto.ActivateUser;
                var identityResult = await _userManager.UpdateAsync(user);
            }
            return Result.Success();
        }
    }
}
