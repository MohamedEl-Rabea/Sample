using FluentValidation;
using Microsoft.Extensions.Localization;
using Moj.CMS.Shared;
using Moj.CMS.UserAccess.Application.DTO;

namespace Moj.CMS.UserAccess.Application.Validators
{
    public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequestDto>
    {
        public ChangePasswordRequestValidator(IStringLocalizer<UsersLocalizer> localizer)
        {
            RuleFor(request => request.Password)
                .Must(x => !string.IsNullOrWhiteSpace(x))
                .WithMessage(x => localizer["Current password is required!"]);
            //.Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer[$"Current {0} is required!", localizer["{PropertyName}"]]);
            RuleFor(request => request.NewPassword)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Password is required!"])
                .MinimumLength(8).WithMessage(localizer["Password must be at least of length 8"])
                .Matches(@"[A-Z]").WithMessage(localizer["Password must contain at least one capital letter"])
                .Matches(@"[a-z]").WithMessage(localizer["Password must contain at least one lowercase letter"])
                .Matches(@"[0-9]").WithMessage(localizer["Password must contain at least one digit"]);
            RuleFor(request => request.ConfirmNewPassword)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Password Confirmation is required!"])
                .Equal(request => request.NewPassword).WithMessage(x => localizer["Passwords don't match"]);
        }
    }
}