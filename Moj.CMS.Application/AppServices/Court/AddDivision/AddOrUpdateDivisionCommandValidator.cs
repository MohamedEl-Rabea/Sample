using FluentValidation;
using Moj.CMS.Shared.Helpers;
using System.Linq;

namespace Moj.CMS.Application.Lookups.Division
{
    public class AddOrUpdateDivisionCommandValidator : AbstractValidator<AddOrUpdateDivisionCommand>
    {
        public AddOrUpdateDivisionCommandValidator(IValidator<DivisionDto> divisionDtoValidator)
        {
            RuleForEach(x => x.DivisionDtoList).SetValidator(divisionDtoValidator);
            RuleFor(request => request.DivisionDtoList)
                .Must(list => ValidationHelper.ListItemsAreUnique(list.Select(a => a.Code)))
                .WithMessage("Division items must be unique.")
                .Must(list => ValidationHelper.ListNotEmpty(list))
                .WithMessage("Division list Items are required.");
        }
    }

    public class DivisionDtoValidator : AbstractValidator<DivisionDto>
    {
        public DivisionDtoValidator()
        {
            RuleFor(dto => dto.Code).NotNull().NotEmpty()
                .WithMessage("Division code cannot be empty")
                .MaximumLength(15)
                .WithMessage("Division code cannot be more than 15 char")
                .Matches(RegularExpressions.DigitsOnly)
                .WithMessage("Division code must be numbers only");

            RuleFor(dto => dto.Name)
                .NotNull().NotEmpty()
                .WithMessage("Division name cannot be empty")
                .MaximumLength(50)
                .WithMessage("Division name cannot be more than 50 char")
                .Matches(RegularExpressions.ArabicLettersOnly)
                .WithMessage("Division name must be in arabic");

            RuleFor(dto => dto.CourtCode).NotNull().NotEmpty()
                .WithMessage("Court code cannot be empty")
                .MaximumLength(15)
                .WithMessage("Court code cannot be more than 15 char")
                .Matches(RegularExpressions.DigitsOnly)
                .WithMessage("Court code must be numbers only");

            RuleFor(dto => dto.IsActive).NotNull()
                .WithMessage("Division status cannot be empty");

        }
    }
}
