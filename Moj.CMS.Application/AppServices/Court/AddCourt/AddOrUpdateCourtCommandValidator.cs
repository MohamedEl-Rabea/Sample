using FluentValidation;
using Moj.CMS.Shared.Helpers;
using System.Linq;

namespace Moj.CMS.Application.AppServices.Court
{
    public class AddOrUpdateCourtCommandValidator : AbstractValidator<AddOrUpdateCourtCommand>
    {
        public AddOrUpdateCourtCommandValidator(IValidator<CourtDto> courtDtoValidator)
        {
            RuleForEach(x => x.CourtDtoList).SetValidator(courtDtoValidator);

            RuleFor(request => request.CourtDtoList)
                .Must(areaList => ValidationHelper.ListItemsAreUnique(areaList.Select(a => a.Code)))
                .WithMessage("Court Items should be unique.")
                .Must(list => ValidationHelper.ListNotEmpty(list))
                .WithMessage("Court list Items are required.");
        }
    }

    public class CourtDtoValidator : AbstractValidator<CourtDto>
    {
        public CourtDtoValidator()
        {
            RuleFor(dto => dto.Code).NotNull().NotEmpty()
                .WithMessage("Court code cannot be empty")
                .MaximumLength(15)
                .WithMessage("Court code cannot be more than 15 char")
                .Matches(RegularExpressions.DigitsOnly)
                .WithMessage("Court code must be numbers only");

            RuleFor(dto => dto.Name)
                .NotNull().NotEmpty()
                .WithMessage("Court name cannot be empty")
                .MaximumLength(50)
                .WithMessage("Court name cannot be more than 50 char")
                .Matches(RegularExpressions.ArabicLettersOnly)
                .WithMessage("Court name must be in arabic");

            RuleFor(dto => dto.AreaCode).NotNull().NotEmpty()
                .WithMessage("Area code cannot be empty")
                .MaximumLength(3)
                .WithMessage("Area code cannot be more than 2 char")
                .Matches(RegularExpressions.DigitsOnly)
                .WithMessage("Area code must be numbers only");

            RuleForEach(dto => dto.BankAccounts)
                .NotNull().NotEmpty()
                .WithMessage("Banck account cannot be empty")
                .MaximumLength(24)
                .WithMessage("Banck account cannot be more than 24 char")
                .Matches(RegularExpressions.LettersAndNumbersOnly)
                .WithMessage("Banck account must be in lletters or numbers only");
        }
    }
}
