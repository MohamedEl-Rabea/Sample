using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Moj.CMS.Shared;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using Moj.CMS.UserAccess.Application.DTO;
using Moj.CMS.UserAccess.Application.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.UserAccess.Application.Services.Users.Commands
{
    public class ChangePasswordCommand : Command<IResult>
    {
        public ChangePasswordRequestDto ChangePasswordRequestDto { get; set; }
        public string UserId { get; set; }
    }
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, IResult>
    {
        private readonly UserManager<CMSUser> _userManager;
        private readonly IStringLocalizer<UsersLocalizer> _localizer;
        public ChangePasswordCommandHandler(UserManager<CMSUser> userManager, IStringLocalizer<UsersLocalizer> localizer)
        {
            _userManager = userManager;
            _localizer = localizer;
        }
        public async Task<IResult> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return Result.Fail(_localizer["User Not Found."]);
            }

            var identityResult = await _userManager.ChangePasswordAsync(
                user,
                request.ChangePasswordRequestDto.Password,
                 request.ChangePasswordRequestDto.NewPassword);
            var errors = identityResult.Errors.Select(e => e.Description).ToList();
            return identityResult.Succeeded ? Result.Success() : Result.Fail(errors);
        }
    }
}
