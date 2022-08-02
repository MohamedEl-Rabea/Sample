using FluentValidation;
using Moj.CMS.Application.AppServices.Claims.Commands.CreateFinancialClaim;
using Moj.CMS.Shared.Helpers;

namespace Moj.CMS.Application.AppServices.Claims.Commands.CreateNewDebt
{
    public class CreateNewDebtCommandValidator : AbstractValidator<CreateNewDebtCommand>
    {
        public CreateNewDebtCommandValidator(IValidator<CreateNewDebtDto> CreateNewDebtDtoValidator)
        {
            RuleFor(x => x.CreateNewDebtDtoList)
                .Must(x => ValidationHelper.ListNotEmpty(x))
                .WithMessage("{PropertyName} must have elements");

            RuleForEach(x => x.CreateNewDebtDtoList).SetValidator(CreateNewDebtDtoValidator);
        }
    }

    public class CreateNewDebtDtoValidator : AbstractValidator<CreateNewDebtDto>
    {
        public CreateNewDebtDtoValidator(ClaimDetailsDtoValidator ClaimDetailsDtoValidator)
        {
            RuleFor(dto => dto.ClaimDetailsList)
                .Must(x => ValidationHelper.ListNotEmpty(x))
                .WithMessage("{PropertyName} must have elements");//ClaimDetailsList is not optional here

            RuleForEach(dto => dto.ClaimDetailsList).SetValidator(ClaimDetailsDtoValidator);

            RuleFor(dto => dto.ClaimNumber).NotNull().NotEmpty()
              .MaximumLength(15)
              .Matches(RegularExpressions.DigitsOnly)
              .WithMessage("{PropertyName} must be numbers only");

        }
    }
}
