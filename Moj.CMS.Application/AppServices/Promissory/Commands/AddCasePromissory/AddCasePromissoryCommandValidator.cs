using FluentValidation;
using Moj.CMS.Application.AppServices.Promissory.Dtos;
using Moj.CMS.Shared.Helpers;
using System.Linq;

namespace Moj.CMS.Application.AppServices.Promissory.Commands.AddCasePromissory
{
    public class AddCasePromissoryCommandValidator : AbstractValidator<AddCasePromissoryCommand>
    {
        public AddCasePromissoryCommandValidator(IValidator<AddCasePromissoryDto> casePromissoryDto)
        {
            RuleFor(request => request.CasePromissoryList)
             .Must(ValidationHelper.ListNotEmpty)
             .WithMessage("Case promissory list cannot be empty.");

            RuleFor(request => request.CasePromissoryList)
                .Must(list => ValidationHelper.ListItemsAreUnique(list.Select(a => a.CaseNumber)))
                .WithMessage("Case number should be unique.");

            RuleForEach(x => x.CasePromissoryList).SetValidator(casePromissoryDto);

        }
    }

    public class CasePromissoryDtoValidator : AbstractValidator<AddCasePromissoryDto>
    {
        public CasePromissoryDtoValidator(IValidator<PromissoryDto> promissoryDto)
        {
            RuleFor(p => p.CaseNumber)
                .NotNull().NotEmpty()
                .WithMessage("Case number is required.");

            RuleFor(x => x.PromissoryDtoList)
                .Must(ValidationHelper.ListNotEmpty)
                .WithMessage(p => $"Promissory List of case number: '{p.CaseNumber}' can't be empty.");

            RuleFor(request => request.PromissoryDtoList)
                .Must(list => ValidationHelper.ListItemsAreUnique(list.Select(a => a.PromissoryNumber)))
                .WithMessage("Promissory Items should be unique.");

            RuleForEach(x => x.PromissoryDtoList).SetValidator(promissoryDto);
        }
    }

    public class PromissoryDtoValidator : AbstractValidator<PromissoryDto>
    {
        public PromissoryDtoValidator()
        {
            RuleFor(dto => dto.PromissoryNumber).NotNull().NotEmpty()
              .WithMessage("Promissory Number cannot be empty.")
              .MaximumLength(15)
              .WithMessage("Promissory Number cannot be more than 15 char.")
              .Matches(RegularExpressions.DigitsOnly)
              .WithMessage("Promissory Number must be numbers only.");
            RuleFor(dto => dto.PromissoryTypeId).NotEmpty().IsInEnum();
        }
    }

}
