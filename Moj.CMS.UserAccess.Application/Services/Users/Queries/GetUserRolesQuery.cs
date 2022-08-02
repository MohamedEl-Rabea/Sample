using MediatR;
using Microsoft.AspNetCore.Identity;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using Moj.CMS.UserAccess.Application.DTO;
using Moj.CMS.UserAccess.Application.Models;
using Moj.CMS.UserAccess.Application.Services.Interfaces.Queries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.UserAccess.Application.Services.Users.Queries
{
    public class GetUserRolesQuery : Query<Result<UserRolesResponseDto>>
    {
        public string UserId { get; set; }
    }

    public class GetUserRolesQueryHandler : IRequestHandler<GetUserRolesQuery, Result<UserRolesResponseDto>>
    {
        private readonly UserManager<CMSUser> _userManager;
        private readonly IRoleQueries _roleQueries;

        public GetUserRolesQueryHandler(IRoleQueries roleQueries, UserManager<CMSUser> userManager)
        {
            _roleQueries = roleQueries;
            _userManager = userManager;
        }

        public async Task<Result<UserRolesResponseDto>> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
        {
            var viewModel = new List<UserRoleModel>();
            var user = await _userManager.FindByIdAsync(request.UserId);
            var roles = await _roleQueries.GetAllAsync();
            foreach (var role in roles)
            {
                var userRolesViewModel = new UserRoleModel
                {
                    RoleName = role.Name
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.Selected = true;
                }
                else
                {
                    userRolesViewModel.Selected = false;
                }
                viewModel.Add(userRolesViewModel);
            }
            var result = new UserRolesResponseDto { UserRoles = viewModel };
            return Result<UserRolesResponseDto>.Success(result);
        }
    }
}
