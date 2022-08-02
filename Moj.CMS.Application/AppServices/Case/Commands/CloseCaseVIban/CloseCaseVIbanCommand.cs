using MediatR;
using Microsoft.Extensions.Localization;
using Moj.CMS.Domain.Aggregates.Case;
using Moj.CMS.Shared;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Case.Commands.CloseCaseVIban
{
    public class CloseCaseVIbanCommand : Command<IResult>
    {
        public CloseCaseVIbanDto CloseCaseVIbanDto { get; set; }
    }

    public class CloseCaseVIbanCommandHandler : IRequestHandler<CloseCaseVIbanCommand, IResult>
    {
        private readonly ICaseRepository _caseRepository;

        public CloseCaseVIbanCommandHandler(ICaseRepository caseRepository)
        {
            _caseRepository = caseRepository;
        }

        public async Task<IResult> Handle(CloseCaseVIbanCommand request, CancellationToken cancellationToken)
        {
            var caseAggregate = await _caseRepository.GetCaseByNumberAsync(request.CloseCaseVIbanDto.CaseNumber);
            //caseAggregate.CloseVIban(request.CloseCaseVIbanDto.VIban);
            return Result.Success();
        }
    }
}