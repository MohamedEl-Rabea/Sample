using FluentValidation;
using Microsoft.Extensions.Localization;
using Moj.CMS.Application.AppServices.Case.Commands.AddSadadInvoice;
using Moj.CMS.Shared;

namespace Moj.CMS.Application.AppServices.Case.Commands.UpdateSadadInvoice
{

    public class UpdateCaseSadadCommandValidator : AbstractValidator<UpdateCaseSadadCommand>
    {
        public UpdateCaseSadadCommandValidator(IStringLocalizer<CMSLocalizer> localizer, IValidator<UpdateCaseSadadDto> updateCaseSadadDto)
        {
            RuleFor(x => x.UpdateCaseSadadDto).NotEmpty().WithMessage(x => localizer["{PropertyName} is required"])
                .SetValidator(updateCaseSadadDto);
        }
    }
    public class UpdateCaseSadadDtoValidator : AbstractValidator<UpdateCaseSadadDto>
    {
        public UpdateCaseSadadDtoValidator(IStringLocalizer<CMSLocalizer> localizer, IValidator<CaseSadadDto> caseSadadDto)
        {
            Include(caseSadadDto);
        }
    }
}