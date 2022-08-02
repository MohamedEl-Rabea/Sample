using FluentValidation;
using Moj.CMS.Shared.Helpers;

namespace Moj.CMS.Application.AppServices.Claims.Commands.CreateFinancialClaim
{
    public class CreateClaimCommandValidator : AbstractValidator<CreateClaimCommand>
    {
        public CreateClaimCommandValidator(IValidator<CreateClaimDto> CreateClaimDtoValidator)
        {
            RuleFor(x => x.ClaimDtoList)
                .Must(x => ValidationHelper.ListNotEmpty(x))
                .WithMessage("{PropertyName} must have elements");
            RuleForEach(x => x.ClaimDtoList).SetValidator(CreateClaimDtoValidator);
        }
    }

    public class CreateClaimDtoValidator : AbstractValidator<CreateClaimDto>
    {
        public CreateClaimDtoValidator(IValidator<ClaimDto> claimValidator)
        {
            RuleFor(dto => dto.Claim).SetValidator(claimValidator);

            RuleFor(dto => dto.CaseNumber).NotEmpty()
                .MaximumLength(15)
                .Matches(RegularExpressions.DigitsOnly)
                .WithMessage("{PropertyName} must be numbers only");

            RuleFor(dto => dto.PromissoryNumber).NotEmpty()
                .MaximumLength(15)
                .Matches(RegularExpressions.DigitsOnly)
                .WithMessage("{PropertyName} must be numbers only");
        }
    }

    public class CaseClaimDtoValidator : AbstractValidator<ClaimDto>
    {
        public CaseClaimDtoValidator(IValidator<ClaimDetailsDto> claimDetailsDtoValidator)
        {
            RuleForEach(dto => dto.ClaimDetails).SetValidator(claimDetailsDtoValidator);//ClaimDetailsList is optional

            RuleFor(dto => dto.ComplaintPartyNumber).NotEmpty()
                .MaximumLength(15)
                .Matches(RegularExpressions.DigitsOnly)
                .WithMessage("{PropertyName} must be numbers only");

            RuleFor(dto => dto.ClaimDateTime)
                .NotEmpty()
                .Must(x => ValidationHelper.ValidDate(x))
                .WithMessage("{PropertyName} must be valid date");

            RuleFor(dto => dto.RequiredAmount).NotEmpty();
            RuleFor(dto => dto.RequiredAmount.CurrencyIso).NotEmpty().MaximumLength(3);
            RuleFor(dto => dto.RequiredAmount.Value).GreaterThanOrEqualTo(0);

            RuleFor(dto => dto.RemainingAmount).NotEmpty();
            RuleFor(dto => dto.RemainingAmount.CurrencyIso).NotEmpty().MaximumLength(3);
            RuleFor(dto => dto.RemainingAmount.Value).GreaterThanOrEqualTo(0);

            RuleFor(dto => dto.DebtTypeId).NotEmpty().IsInEnum();
        }
    }

    public class ClaimDetailsDtoValidator : AbstractValidator<ClaimDetailsDto>
    {
        public ClaimDetailsDtoValidator()
        {
            RuleFor(dto => dto.AccusedPartyNumber).NotEmpty()
               .MaximumLength(15)
               .Matches(RegularExpressions.DigitsOnly)
               .WithMessage("{PropertyName} must be numbers only");


            RuleFor(dto => dto.RequiredAmount).NotEmpty();
            RuleFor(dto => dto.RequiredAmount.CurrencyIso).NotEmpty().MaximumLength(3);
            RuleFor(dto => dto.RequiredAmount.Value).GreaterThanOrEqualTo(0);

            RuleFor(dto => dto.BillingAmount).NotEmpty();
            RuleFor(dto => dto.BillingAmount.CurrencyIso).NotEmpty().MaximumLength(3);
            RuleFor(dto => dto.BillingAmount.Value).GreaterThanOrEqualTo(0);

        }
    }
}
