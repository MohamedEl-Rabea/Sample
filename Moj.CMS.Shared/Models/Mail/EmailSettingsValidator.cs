using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Moj.CMS.Shared.Models.Mail
{
    public class EmailSettingsValidator : AbstractValidator<EmailSettingsDto>
    {
        public EmailSettingsValidator(IStringLocalizer<CMSLocalizer> localizer)
        {
            RuleFor(e => e.EmailHost)
                .NotEmpty().WithMessage(x => $"{localizer["Email Host"]} {localizer["is required!"]}");

            RuleFor(e => e.Port)
                .NotEmpty().WithMessage(x => $"{localizer["Port"]} {localizer["is required!"]}");

            RuleFor(e => e.Email)
                    .NotEmpty().WithMessage(x => $"{localizer["Email"]} {localizer["is required!"]}")
                    .EmailAddress().WithMessage(localizer["Email is not valid"]);

            RuleFor(e => e.Password)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Password is required!"]);
        }
    }
}
