using FluentValidation;
using Microsoft.Extensions.Localization;
using Moj.CMS.Shared;
using Moj.CMS.Shared.Helpers;
using System.Linq;

namespace Moj.CMS.Application.AppServices.Party.Commands.AddParty
{
    public class AddPartiesCommandValidator : AbstractValidator<AddPartiesCommand>
    {
        public AddPartiesCommandValidator(IStringLocalizer<CMSLocalizer> localizer,
            IValidator<PartyDto> partyDto)
        {
            RuleFor(r => r.Parties)
                .Must(ValidationHelper.ListNotEmpty)
                .WithMessage(x => localizer["{PropertyName} should not be empty or null"])
                .Must(list => ValidationHelper.ListItemsAreUnique(list.ToList().Select(a => a.PartyNumber)))
                .WithMessage("Party Number must be unique.");

            RuleForEach(x => x.Parties).SetValidator(partyDto);
        }
    }

    public class PartyDtoValidator : AbstractValidator<PartyDto>
    {
        public PartyDtoValidator(IStringLocalizer<CMSLocalizer> localizer)
        {
            RuleFor(request => request.PartyNumber)
                .NotNull().NotEmpty()
                .WithMessage(x => localizer["{PropertyName} is required"])
                .MaximumLength(15)
                .WithMessage(x => "{PropertyName} cannot be more than 15 char")
                .Matches(RegularExpressions.DigitsOnly)
                .WithMessage(x => localizer["{PropertyName} must be numbers only"]);

            RuleFor(request => request.PartyIdentityNumber)
                .NotNull().NotEmpty()
                .WithMessage(x => localizer["{PropertyName} is required"])
                .MaximumLength(15)
                .WithMessage(x => localizer["{PropertyName} cannot be more than 15 char"]);

            RuleFor(request => request.FullName)
                .NotNull().NotEmpty()
                .WithMessage(x => localizer["{PropertyName} is required"])
                .MaximumLength(60)
                .WithMessage(x => localizer["{PropertyName} cannot be more than 60 char"]);


            RuleFor(request => request.NationalityCode)
                .NotNull().NotEmpty()
                .WithMessage(x => localizer["{PropertyName} is required"])
                .MaximumLength(3)
                .WithMessage(x => localizer["{PropertyName} cannot be more than 3 char"]);

            RuleFor(request => request.DateOfBirth)
                .Must(ValidationHelper.ValidOptionalDate)
                .WithMessage(x => localizer["{PropertyName} is required"]);

            RuleFor(x => x.PartyTypeId)
                .IsInEnum()
                .WithMessage(localizer["{PropertyValue} is invalid value for {PropertyName}"]);
            RuleFor(x => x.PartyIdentityTypeId)
                .IsInEnum()
                .WithMessage(localizer["{PropertyValue} is invalid value for {PropertyName}"]);
            RuleFor(x => x.PartyLocationId)
                .IsInEnum()
                .WithMessage(localizer["{PropertyValue} is invalid value for {PropertyName}"]);
            RuleFor(x => x.Gender)
                .IsInEnum()
                .WithMessage(localizer["{PropertyValue} is invalid value for {PropertyName}"]);
        }
    }
}