using MediatR;
using Moj.CMS.Domain.Aggregates.Case;
using Moj.CMS.Domain.Aggregates.Case.ValueObjects;
using Moj.CMS.Domain.ParameterObjects.Case;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Case.Commands.UpdateCase
{
    public class UpdateCaseCommand : Command<IResult>
    {
        public UpdateCaseDto UpdateCaseDto { get; set; }
    }

    public class UpdateCaseCommandHandler : IRequestHandler<UpdateCaseCommand, IResult>
    {
        private readonly ICaseRepository _caseRepository;

        public UpdateCaseCommandHandler(ICaseRepository caseRepository)
        {
            _caseRepository = caseRepository;
        }

        public async Task<IResult> Handle(UpdateCaseCommand request, CancellationToken cancellationToken)
        {
            var caseAggregate = await _caseRepository.GetCaseByNumberAsync(request.UpdateCaseDto.CaseNumber);
            var updateParameter = BuildUpdateCaseParameter(request.UpdateCaseDto);
            await caseAggregate.UpdateCaseInfoAsync(updateParameter);
            return Result.Success();
        }

        private UpdateCaseParameter BuildUpdateCaseParameter(UpdateCaseDto input)
        {
            var basicDetails = input.CaseDetails;
            return new UpdateCaseParameter
            {
                CaseNumber = input.CaseNumber,
                CaseTypeId = input.CaseType,
                CaseStatusId = input.CaseStatus,
                CaseDetailsParam = new CaseDetailsParam
                {
                    CaseDetails = CaseDetails.Create(basicDetails.DivisionCode, basicDetails.CourtCode, basicDetails.JudgeCode)
                }
            };
        }
    }
}