using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
    public class AddUserCommand : Command<IResult>
    {
        public RegisterDto RegisterDto { get; set; }
    }

    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, IResult>
    {
        private readonly IStringLocalizer<UsersLocalizer> _localizer;
        private readonly UserManager<CMSUser> _userManager;

        public AddUserCommandHandler(UserManager<CMSUser> userManager, IStringLocalizer<UsersLocalizer> localizer)
        {
            _localizer = localizer;
            _userManager = userManager;
        }

        public async Task<IResult> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var userWithSameUserName = await _userManager.FindByNameAsync(request.RegisterDto.UserName);
            if (userWithSameUserName != null)
            {
                return Result<string>.Fail($"{_localizer["Username"]} '{request.RegisterDto.UserName}' {_localizer["is already taken."]}");
            }
            var user = new CMSUser
            {
                Email = request.RegisterDto.Email,
                FirstName = request.RegisterDto.FirstName,
                LastName = request.RegisterDto.LastName,
                UserName = request.RegisterDto.UserName,
                PhoneNumber = request.RegisterDto.PhoneNumber,
                IsActive = request.RegisterDto.ActivateUser,
                EmailConfirmed = request.RegisterDto.AutoConfirmEmail
            };
            if (!string.IsNullOrWhiteSpace(user.PhoneNumber))
            {
                var userWithSamePhoneNumber = await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == user.PhoneNumber);
                if (userWithSamePhoneNumber != null)
                {
                    return Result<string>.Fail($"{_localizer["PhoneNumber"]} '{request.RegisterDto.PhoneNumber}' {_localizer["is already taken."]}");
                }
            }
            var userWithSameEmail = await _userManager.FindByEmailAsync(request.RegisterDto.Email);
            if (userWithSameEmail == null)
            {
                var result = await _userManager.CreateAsync(user, request.RegisterDto.Password);
                if (result.Succeeded)
                {
                    if (!request.RegisterDto.AutoConfirmEmail)
                    {
                        return Result<string>.Success(user.Id, message: _localizer[$"User Registered. Please check your Mailbox to verify!"]);
                    }
                    return Result<string>.Success(user.Id, message: _localizer[$"User Registered"]);
                }
                else
                {
                    return Result<string>.Fail(result.Errors.Select(a => a.Description).ToList());
                }
            }
            else
            {
                return Result<string>.Fail($"{_localizer["Email"]} {request.RegisterDto.Email } {_localizer["is already registered."]}");
            }
        }
    }
}
