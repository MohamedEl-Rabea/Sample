using MediatR;
using Moj.CMS.Domain.Aggregates.Case;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Case.Commands.TerminateCase
{
    public class TerminateCaseCommand : Command<IResult>
    {
        public TerminateCaseDto TerminateCaseDto { get; set; }
    }

    public class TerminateCaseCommandHandler : IRequestHandler<TerminateCaseCommand, IResult>
    {
        private readonly ICaseRepository _caseRepository;

        public TerminateCaseCommandHandler(ICaseRepository caseRepository)
        {
            _caseRepository = caseRepository;
        }

        public async Task<IResult> Handle(TerminateCaseCommand request, CancellationToken cancellationToken)
        {
            var caseAggregate = await _caseRepository.GetCaseByNumberAsync(request.TerminateCaseDto.CaseNumber);
            caseAggregate.Terminate(request.TerminateCaseDto.TerminationReasonId);
            return Result.Success();
        }
    }
}
