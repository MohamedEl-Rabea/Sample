using FluentValidation;
using Microsoft.Extensions.Localization;
using Moj.CMS.Shared;
using Moj.CMS.UserAccess.Application.DTO;

namespace Moj.CMS.UserAccess.Application.Validators
{
    public class UpdateProfileRequestValidator : AbstractValidator<UpdateProfileRequestDto>
    {
        public UpdateProfileRequestValidator(IStringLocalizer<UsersLocalizer> localizer)
        {
            RuleFor(request => request.FirstName)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["First Name is required"]);
            RuleFor(request => request.LastName)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Last Name is required"]);
        }
    }
}