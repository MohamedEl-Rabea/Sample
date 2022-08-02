using MediatR;
using Microsoft.Extensions.Localization;
using Moj.CMS.Domain.Aggregates.Case;
using Moj.CMS.Shared;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Case.Commands.CloseSadadInvoice
{
    public class CloseCaseSadadCommand : Command<IResult>
    {
        public CloseCaseSadadDto CloseCaseSadadDto { get; set; }
    }

    public class CloseCaseSadadCommandHandler : IRequestHandler<CloseCaseSadadCommand, IResult>
    {
        private readonly ICaseRepository _caseRepository;

        public CloseCaseSadadCommandHandler(ICaseRepository caseRepository)
        {
            _caseRepository = caseRepository;
        }

        public async Task<IResult> Handle(CloseCaseSadadCommand request, CancellationToken cancellationToken)
        {
            var caseAggregate = await _caseRepository.GetCaseByNumberAsync(request.CloseCaseSadadDto.CaseNumber);
            //caseAggregate.CloseSadad(request.CloseCaseSadadDto.SadadNumber);
            return Result.Success();
        }
    }
}