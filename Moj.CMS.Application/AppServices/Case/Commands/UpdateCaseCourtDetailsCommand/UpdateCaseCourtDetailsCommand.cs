using MediatR;
using Moj.CMS.Domain.Aggregates.Case;
using Moj.CMS.Domain.Aggregates.Case.ValueObjects;
using Moj.CMS.Domain.DomainServices;
using Moj.CMS.Domain.ParameterObjects.Case;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Case.Commands.UpdateCaseCourtDetailsCommand
{

    public class UpdateCaseCourtDetailsCommand : Command<IResult>
    {
        public UpdateCaseCourtDetailsDto UpdateCaseCourtDetailsDto { get; set; }
    }

    public class UpdateCaseCourtDetailsCommandHandler : IRequestHandler<UpdateCaseCourtDetailsCommand, IResult>
    {
        private readonly ICaseRepository _caseRepository;
        private readonly IEnforceJudgeIsExists _enforceJudgeIsExist;
        private readonly IEnforceCourtIsExists _enforceCourtIsExist;
        private readonly IGetDivisionCourtCode _getDivisionCourtCode;

        public UpdateCaseCourtDetailsCommandHandler(ICaseRepository caseRepository,
            IEnforceJudgeIsExists enforceJudgeIsExist,
            IEnforceCourtIsExists enforceCourtIsExist,
            IGetDivisionCourtCode getDivisionCourtCode)
        {
            _caseRepository = caseRepository;
            _enforceJudgeIsExist = enforceJudgeIsExist;
            _enforceCourtIsExist = enforceCourtIsExist;
            _getDivisionCourtCode = getDivisionCourtCode;
        }


        public async Task<IResult> Handle(UpdateCaseCourtDetailsCommand request, CancellationToken cancellationToken)
        {
            var caseAggregate = await _caseRepository.GetCaseByNumberAsync(request.UpdateCaseCourtDetailsDto.CaseNumber);
            var caseDetailsParam = BuildUpdateCaseCourtDetailsParameter(request.UpdateCaseCourtDetailsDto);
            await caseAggregate.UpdateCourtDetailsAsync(caseDetailsParam);
            return Result.Success();
        }

        private CaseDetailsParam BuildUpdateCaseCourtDetailsParameter(UpdateCaseCourtDetailsDto input)
        {
            return new CaseDetailsParam
            {
                CaseDetails = CaseDetails.Create(input.DivisionCode, input.CourtCode, input.JudgeCode),
                EnforceJudgeIsExists = _enforceJudgeIsExist,
                EnforceCourtIsExists = _enforceCourtIsExist,
                GetDivisionCourtCode = _getDivisionCourtCode
            };
        }
    }
}
