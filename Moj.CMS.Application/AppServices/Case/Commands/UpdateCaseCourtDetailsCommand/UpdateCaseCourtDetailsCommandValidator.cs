using FluentValidation;
using Moj.CMS.Shared.Helpers;

namespace Moj.CMS.Application.AppServices.Case.Commands.UpdateCaseCourtDetailsCommand
{
    public class UpdateCaseCourtDetailsCommandValidator : AbstractValidator<UpdateCaseCourtDetailsCommand>
    {
        public UpdateCaseCourtDetailsCommandValidator(IValidator<UpdateCaseCourtDetailsDto> updateCaseCourtDetailsValidator)
        {
            RuleFor(request => request.UpdateCaseCourtDetailsDto).NotNull();

            RuleFor(x => x.UpdateCaseCourtDetailsDto).SetValidator(updateCaseCourtDetailsValidator);
        }

        public class UpdateCaseCourtDetailsDtoValidator : AbstractValidator<UpdateCaseCourtDetailsDto>
        {
            public UpdateCaseCourtDetailsDtoValidator()
            {
                RuleFor(request => request.CaseNumber).NotEmpty().MaximumLength(15).Matches(RegularExpressions.DigitsOnly).WithMessage("Must be numbers only.");
                RuleFor(request => request.DivisionCode).NotEmpty().MaximumLength(15).Matches(RegularExpressions.DigitsOnly).WithMessage("Must be numbers only.");
                RuleFor(request => request.CourtCode).NotEmpty().MaximumLength(15).Matches(RegularExpressions.DigitsOnly).WithMessage("Must be numbers only.");
                RuleFor(request => request.JudgeCode).NotEmpty().MaximumLength(15).Matches(RegularExpressions.DigitsOnly).WithMessage("Must be numbers only.");
            }
        }
    }
}
