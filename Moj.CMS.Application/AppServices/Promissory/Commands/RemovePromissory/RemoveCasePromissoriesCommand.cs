using MediatR;
using Moj.CMS.Domain.Aggregates.Case;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Promissory.Commands.RemovePromissory
{
    public class RemoveCasePromissoriesCommand : Command<IResult>
    {
        public RemoveCasePromissoriesDto RemoveCasePromissoriesDto { get; set; }
    }

    public class RemoveCasePromissoriesCommandHandler : IRequestHandler<RemoveCasePromissoriesCommand, IResult>
    {
        private readonly ICaseRepository _caseRepository;

        public RemoveCasePromissoriesCommandHandler(ICaseRepository caseRepository)
        {
            _caseRepository = caseRepository;
        }

        public async Task<IResult> Handle(RemoveCasePromissoriesCommand request, CancellationToken cancellationToken)
        {
            var caseAggregate = await _caseRepository.GetCaseByNumberAsync(request.RemoveCasePromissoriesDto.CaseNumber);
            // caseAggregate.RemovePromissories(request.RemoveCasePromissoriesDto.PromissoryNumbers);
            return Result.Success();
        }
    }
}
