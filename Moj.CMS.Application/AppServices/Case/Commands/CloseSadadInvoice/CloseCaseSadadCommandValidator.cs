using FluentValidation;
using Microsoft.Extensions.Localization;
using Moj.CMS.Shared;

namespace Moj.CMS.Application.AppServices.Case.Commands.CloseSadadInvoice
{
    public class CloseCaseSadadCommandValidator : AbstractValidator<CloseCaseSadadCommand>
    {
        public CloseCaseSadadCommandValidator(IStringLocalizer<CMSLocalizer> localizer, IValidator<CloseCaseSadadDto> closeCaseSadadDtoValidator)
        {
            RuleFor(x => x.CloseCaseSadadDto).NotEmpty().WithMessage(x => localizer["{PropertyName} is required"])
                .SetValidator(closeCaseSadadDtoValidator);
        }
    }
    public class CloseCaseSadadDtoValidator : AbstractValidator<CloseCaseSadadDto>
    {
        public CloseCaseSadadDtoValidator(IStringLocalizer<CMSLocalizer> localizer)
        {
            RuleFor(request => request.CaseNumber)
                     .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["{PropertyName} is required"]);
            RuleFor(request => request.SadadNumber)
               .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["{PropertyName} is required"]);
        }
    }
}