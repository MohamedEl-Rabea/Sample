using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Moj.CMS.Shared.Models.SMS
{
    public class SmsSettingsValidator : AbstractValidator<SmsSettingsDto>
    {
        public SmsSettingsValidator(IStringLocalizer<CMSLocalizer> localizer)
        {
            RuleFor(e => e.SourcePhoneNumber)
                .NotEmpty().WithMessage(x => $"{localizer["Source Phone Number"]} {localizer["is required!"]}")
                .Matches(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}").WithMessage(localizer["Phone Number is not valid"]);

            RuleFor(e => e.UserName)
                .NotEmpty().WithMessage(x => $"{localizer["User Name"]} {localizer["is required!"]}");

            RuleFor(e => e.Password)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Password is required!"]);
        }
    }
}
