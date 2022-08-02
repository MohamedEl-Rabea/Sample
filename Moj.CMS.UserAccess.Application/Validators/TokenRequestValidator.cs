using FluentValidation;
using Microsoft.Extensions.Localization;
using Moj.CMS.Shared;
using Moj.CMS.UserAccess.Application.DTO;

namespace Moj.CMS.UserAccess.Application.Validators
{
    public class TokenRequestValidator : AbstractValidator<TokenRequestDto>
    {
        public TokenRequestValidator(IStringLocalizer<UsersLocalizer> localizer)
        {
            RuleFor(request => request.Email)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Email is required"])
                .EmailAddress().WithMessage(x => localizer["Email is not correct"]);
            RuleFor(request => request.Password)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Password is required!"]);
        }
    }
}
