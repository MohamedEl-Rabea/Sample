using FluentValidation;
using Moj.CMS.Application.AppServices.Case.Commands.AddCase;

namespace Moj.CMS.Application.AppServices.Case.Commands.AddCaseParty
{
    public class AddCasePartiesCommandValidator : AbstractValidator<AddCasePartiesCommand>
    {
        public AddCasePartiesCommandValidator(IValidator<AddCasePartiesDto> addCasePartiesDto)
        {
            RuleForEach(x => x.AddCasePartiesDtoList)
                       .NotNull()
                       .NotEmpty()
                       .WithMessage("{PropertyName} is required")
                       .SetValidator(addCasePartiesDto);
        }
    }
    public class AddCasePartiesDtoValidator : AbstractValidator<AddCasePartiesDto>
    {
        public AddCasePartiesDtoValidator(IValidator<CasePartyDto> casePartyDto)
        {
            RuleFor(request => request)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} should not be empty or null");

            RuleForEach(request => request.Requesters)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} should not be empty or null")
                .SetValidator(casePartyDto);

            RuleForEach(request => request.Respondents)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} should not be empty or null")
                .SetValidator(casePartyDto);

            RuleFor(request => request.CaseNumber)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required");
        }
    }
}
