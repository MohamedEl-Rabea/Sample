using FluentValidation;
using Microsoft.Extensions.Localization;
using Moj.CMS.Shared;
using Moj.CMS.Shared.Helpers;

namespace Moj.CMS.Application.AppServices.Case.Commands.AddSadadInvoice
{
    public class AddCaseSadadCommandValidator : AbstractValidator<AddCaseSadadCommand>
    {
        public AddCaseSadadCommandValidator(IStringLocalizer<CMSLocalizer> localizer, IValidator<CreateCaseSadadDto> createCaseSadadDto)
        {
            RuleFor(x => x.CaseSadadDto).NotEmpty().WithMessage(x => localizer["{PropertyName} is required"])
              .SetValidator(createCaseSadadDto);
        }
    }
    public class CreateCaseSadadDtoValidator : AbstractValidator<CreateCaseSadadDto>
    {
        public CreateCaseSadadDtoValidator(IStringLocalizer<CMSLocalizer> localizer, IValidator<CaseSadadDto> caseSadadDto)
        {
            Include(caseSadadDto);
            //IssueDate validate date 
            RuleFor(request => request.IssueDate)
               .Must(ValidationHelper.ValidDate)
               .WithMessage(x => localizer["{PropertyName} is required"]);
        }
    }

    public class CaseSadadDtoValidator : AbstractValidator<CaseSadadDto>
    {
        public CaseSadadDtoValidator(IStringLocalizer<CMSLocalizer> localizer)
        {
            RuleFor(request => request.PartyIdentityNumber)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["{PropertyName} is required"]);
            RuleFor(request => request.CaseNumber)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["{PropertyName} is required"]);
            RuleFor(x => x.RequiredAmount).NotEmpty()
                 .WithMessage(x => localizer["{PropertyName} is required"])
                .GreaterThan(0).WithMessage(localizer["{PropertyValue}  should be greater than zero"]);
            RuleFor(x => x.CAP).NotEmpty()
                .WithMessage(x => localizer["{PropertyName} is required"])
                .GreaterThan(0).WithMessage(localizer["{PropertyValue}  should be greater than zero"]);
        }
    }
}
