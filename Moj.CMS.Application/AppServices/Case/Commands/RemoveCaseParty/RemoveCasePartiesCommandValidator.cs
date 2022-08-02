using FluentValidation;
using Microsoft.Extensions.Localization;
using Moj.CMS.Shared;
using Moj.CMS.Shared.Helpers;

namespace Moj.CMS.Application.AppServices.Case.Commands.RemoveCaseParty
{
    public class RemoveCasePartiesCommandValidator : AbstractValidator<RemoveCasePartiesCommand>
    {
        public RemoveCasePartiesCommandValidator(IStringLocalizer<CMSLocalizer> localizer, IValidator<RemoveCasePartiesDto> removeCasePartiesDto)
        {
            RuleForEach(x => x.RemoveCasePartiesDtoList).NotEmpty().WithMessage(x => localizer["{PropertyName} is required"])
              .SetValidator(removeCasePartiesDto);
        }
    }
    public class RemoveCasePartiesDtoValidator : AbstractValidator<RemoveCasePartiesDto>
    {
        public RemoveCasePartiesDtoValidator(IStringLocalizer<CMSLocalizer> localizer)
        {
            RuleFor(x => x.PartiesIdentityNumbers)
            .Must(s => ValidationHelper.ListNotEmpty(s))
            .WithMessage(x => localizer["{PropertyName} should not be empty or null"]);

            RuleFor(request => request.CaseNumber)
                 .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["{PropertyName} is required"]);
        }
    }
}
