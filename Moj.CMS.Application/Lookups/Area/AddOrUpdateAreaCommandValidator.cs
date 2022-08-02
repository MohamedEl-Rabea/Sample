using FluentValidation;
using Moj.CMS.Shared.Helpers;
using System.Linq;

namespace Moj.CMS.Application.Lookups.Area
{
    public class AddOrUpdateAreaCommandValidator : AbstractValidator<AddOrUpdateAreaCommand>
    {
        public AddOrUpdateAreaCommandValidator(IValidator<AreaDto> AreaDtoValidator)
        {
            RuleForEach(x => x.AreaDtoList).SetValidator(AreaDtoValidator);

            RuleFor(request => request.AreaDtoList)
             .Must(areaList => ValidationHelper.ListItemsAreUnique(areaList.Select(a=>a.Code)))
             .WithMessage("Area Items should be unique.")
             .Must(list => ValidationHelper.ListNotEmpty(list))
             .WithMessage("Area list Items are required.");
    }
    }

    public class AreaDtoValidator : AbstractValidator<AreaDto>
    {
        public AreaDtoValidator()
        {
            RuleFor(dto => dto.Code).NotNull().NotEmpty()
                .WithMessage("Area code cannot be empty")
                .MaximumLength(3)
                .WithMessage("Area code cannot be more than 15 char")
                .Matches(RegularExpressions.DigitsOnly)
                .WithMessage("Area code must be numbers only");

            RuleFor(dto => dto.Name)
                .NotNull().NotEmpty()
                .WithMessage("Area name cannot be empty")
                .MaximumLength(50)
                .WithMessage("Area name cannot be more than 50 char")
                .Matches(RegularExpressions.ArabicLettersOnly)
                .WithMessage("Area name must be in arabic");
        }
    }
}
