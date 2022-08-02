using FluentValidation;
using Moj.CMS.Shared.Helpers;
using System.Linq;

namespace Moj.CMS.Application.Lookups.Judge
{
    public class AddOrUpdateJudgeCommandValidator : AbstractValidator<AddOrUpdateJudgeCommand>
    {
        public AddOrUpdateJudgeCommandValidator(IValidator<JudgeDto> judgeDtoValidator)
        {
            RuleFor(request => request.JudgeDtoList)
                .Must(list => ValidationHelper.ListNotEmpty(list))
                .WithMessage("Judge list Items are required.")
                .Must(list => ValidationHelper.ListItemsAreUnique(list.Select(a => a.Code)))
                .WithMessage("Judge Items should be unique.");

            RuleForEach(x => x.JudgeDtoList).SetValidator(judgeDtoValidator);
        }
    }

    public class JudgeDtoValidator : AbstractValidator<JudgeDto>
    {
        public JudgeDtoValidator()
        {
            RuleFor(dto => dto.Code).NotNull().NotEmpty()
                .WithMessage("Judge code cannot be empty")
                .MaximumLength(15)
                .WithMessage("Judge code cannot be more than 15 char")
                .Matches(RegularExpressions.DigitsOnly)
                .WithMessage("Judge code must be numbers only");

            RuleFor(dto => dto.Name)
                .NotNull().NotEmpty()
                .WithMessage("Judge name cannot be empty")
                .MaximumLength(50)
                .WithMessage("Judge name cannot be more than 50 char")
                .Matches(RegularExpressions.ArabicLettersOnly)
                .WithMessage("Judge name must be in arabic");
        }
    }
}
