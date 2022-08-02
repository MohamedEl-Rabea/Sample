using FluentValidation;
using Moj.CMS.Application.AppServices.Case.Commands.ClaimIncreaseDicrease;
using Moj.CMS.Application.AppServices.Claims.Commands.ClaimIncreaseDicrease;
using Moj.CMS.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Claims.Commands.AddClaimEffect
{
    public class AddClaimEffectCommandValidator: AbstractValidator<AddClaimEffectCommand>
    {
        public AddClaimEffectCommandValidator(IValidator<ClaimEffectInputDto> claimEffectValidator)
        {
            RuleFor(c => c.ClaimEffectInput).SetValidator(claimEffectValidator);
        }
    }

    public class ClaimEffectInputDtoValidator : AbstractValidator<ClaimEffectInputDto>
    {
        public ClaimEffectInputDtoValidator()
        {
            RuleFor(c => c.ClaimNumber)
                .NotNull()
                .NotEmpty()
                .Matches(RegularExpressions.DigitsOnly)
                .WithMessage("{PropertyName} must be numbers only");

            RuleFor(c => c.EffectType)
                .NotNull()
                .NotEmpty()
                .IsInEnum();

            RuleFor(c => c.EffectAmount)
                .NotNull()
                .NotEmpty();
            
            RuleFor(c => c.EffectAmount.CurrencyIso)
                .NotEmpty()
                .MaximumLength(3)
                .WithMessage("{PropertyName} Should not be greater than 3 chars");

            RuleFor(c => c.EffectAmount.Value)
                .GreaterThanOrEqualTo(0)
                .WithMessage("{PropertyName} must be positive number");

        }
    }
}
