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

namespace Moj.CMS.UserAccess.Application.Services.Users.Commands
{
    public class UpdateUserRolesCommand : Command<IResult>
    {
        public UpdateUserRolesRequestDto UpdateUserRolesRequestDto { get; set; }
    }

    public class UpdateUserRolesCommandHandler : IRequestHandler<UpdateUserRolesCommand, IResult>
    {
        private readonly UserManager<CMSUser> _userManager;
        private readonly IStringLocalizer<CMSLocalizer> _localizer;
        public UpdateUserRolesCommandHandler(UserManager<CMSUser> userManager, IStringLocalizer<CMSLocalizer> localizer)
        {
            _userManager = userManager;
            _localizer = localizer;
        }

        public UpdateUserRolesCommandHandler()
        {

        }

        public async Task<IResult> Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UpdateUserRolesRequestDto.UserId);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Contains(RoleConstants.AdministratorRole))
                return Result.Fail(_localizer["Not Allowed."]);

            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            result = await _userManager.AddToRolesAsync(user, request.UpdateUserRolesRequestDto.UserRoles.Where(x => x.Selected).Select(y => y.RoleName));
            return Result.Success(_localizer["Roles Updated"]);
        }
    }
}
