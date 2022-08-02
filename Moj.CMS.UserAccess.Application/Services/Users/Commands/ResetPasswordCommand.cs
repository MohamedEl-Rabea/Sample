using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Moj.CMS.Shared;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using Moj.CMS.UserAccess.Application.DTO;
using Moj.CMS.UserAccess.Application.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.UserAccess.Application.Services.Users.Commands
{

    public class ResetPasswordCommand : Command<IResult>
    {
        public ResetPasswordDto ResetPasswordDto { get; set; }
    }

    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, IResult>
    {
        private readonly IStringLocalizer<UsersLocalizer> _localizer;
        private readonly UserManager<CMSUser> _userManager;

        public ResetPasswordCommandHandler(UserManager<CMSUser> userManager, IStringLocalizer<UsersLocalizer> localizer)
        {
            _localizer = localizer;
            _userManager = userManager;
        }

        public async Task<IResult> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.ResetPasswordDto.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return Result<string>.Fail(_localizer["An Error has occured!"]);
            }

            var result = await _userManager.ResetPasswordAsync(user, request.ResetPasswordDto.Token, request.ResetPasswordDto.Password);
            if (result.Succeeded)
            {
                return Result<string>.Success(_localizer["Password Reset Successful!"]);
            }
            else
            {
                return Result<string>.Fail(_localizer["An Error has occured!"]);
            }
        }
    }
}
