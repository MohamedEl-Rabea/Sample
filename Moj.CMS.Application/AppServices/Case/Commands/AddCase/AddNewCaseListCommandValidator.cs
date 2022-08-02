using FluentValidation;
using Moj.CMS.Application.AppServices.Claims.Commands.CreateFinancialClaim;
using Moj.CMS.Application.AppServices.Party.Commands.AddParty;
using Moj.CMS.Application.AppServices.Promissory.Dtos;
using Moj.CMS.Shared.Helpers;

namespace Moj.CMS.Application.AppServices.Case.Commands.AddCase
{
    public class AddNewCaseListCommandValidator : AbstractValidator<AddCaseListCommand>
    {
        public AddNewCaseListCommandValidator(IValidator<CaseFullDetailsDto> caseDetailsValidator)
        {
            RuleFor(request => request.NewCaseInputList).NotEmpty();

            RuleForEach(x => x.NewCaseInputList).SetValidator(caseDetailsValidator);
        }
    }

    public class NewCaseDtoValidator : AbstractValidator<CaseFullDetailsDto>
    {
        public NewCaseDtoValidator(IValidator<CasePartyDto> caseDateValidator,
            IValidator<ClaimDto> claimDtoValidator,
            IValidator<PromissoryDto> promissoryDtoValidator,
            IValidator<CaseDetailsDto> CaseDetailsDtoValidator)
        {
            RuleFor(request => request.BasicDetails).NotNull().NotEmpty().SetValidator(CaseDetailsDtoValidator);

            RuleForEach(request => request.Requesters).SetValidator(caseDateValidator);
            RuleForEach(request => request.Respondents).SetValidator(caseDateValidator);
            RuleForEach(request => request.Claims).SetValidator(claimDtoValidator);
            RuleForEach(request => request.PromissoryList).SetValidator(promissoryDtoValidator);
        }
    }

    public class CasePartyDetailsDtoValidator : AbstractValidator<CasePartyDto>
    {
        public CasePartyDetailsDtoValidator(IValidator<PartyDto> partyDtoValidator)
        {
            RuleFor(request => request.PartyRole)
                .NotNull()
                .WithMessage("{PropertyName} cannot be empty")
                .IsInEnum()
                .WithMessage("{PropertyName} is invalid");

            RuleFor(request => request.Details).NotEmpty().SetValidator(partyDtoValidator);
        }
    }

    public class CaseDetailsDetailsDtoValidator : AbstractValidator<CaseDetailsDto>
    {
        public CaseDetailsDetailsDtoValidator()
        {
            RuleFor(request => request.CaseNumber).NotEmpty().MaximumLength(15).Matches(RegularExpressions.DigitsOnly).WithMessage("Must be numbers only.");
            RuleFor(request => request.CaseStatus).NotEmpty().IsInEnum();
            RuleFor(request => request.CaseType).NotEmpty().IsInEnum();

            RuleFor(request => request.DivisionCode).NotEmpty().MaximumLength(15).Matches(RegularExpressions.DigitsOnly).WithMessage("Must be numbers only.");
            RuleFor(request => request.CourtCode).NotEmpty().MaximumLength(15).Matches(RegularExpressions.DigitsOnly).WithMessage("Must be numbers only.");
            RuleFor(request => request.JudgeCode).NotEmpty().MaximumLength(15).Matches(RegularExpressions.DigitsOnly).WithMessage("Must be numbers only.");

            RuleFor(request => request.JudgeAcceptanceDate).NotEmpty().Must(x => ValidationHelper.ValidDate(x));
            RuleFor(request => request.ReceiveDate).Must(x => ValidationHelper.ValidDate(x));
        }
    }
}
