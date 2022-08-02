using FluentValidation;
using Moj.CMS.Shared.Helpers;

namespace Moj.CMS.Application.AppServices.Claims.Commands.CloseClaim
{
    public class CloseClaimCommandValidator : AbstractValidator<CloseClaimCommand>
    {
        public CloseClaimCommandValidator(IValidator<CloseClaimDto> closeClaimValidator)
        {
            RuleFor(c => c.CloseClaimDto).SetValidator(closeClaimValidator);
        }
    }

    public class CloseClaimDtoValidator : AbstractValidator<CloseClaimDto>
    {
        public CloseClaimDtoValidator()
        {
            RuleFor(c => c.ClaimNumber)
                .NotNull()
                .NotEmpty()
                .Matches(RegularExpressions.DigitsOnly)
                .WithMessage("{PropertyName} must be numbers only");

            RuleFor(c => c.ReferenceNumber)
                .NotNull()
                .NotEmpty();

            RuleFor(c => c.CloseDate)
                .NotNull()
                .NotEmpty()
                .Must(x => ValidationHelper.ValidDate(x))
                .WithMessage("{PropertyName} must be valid date");
        }
    }
}
