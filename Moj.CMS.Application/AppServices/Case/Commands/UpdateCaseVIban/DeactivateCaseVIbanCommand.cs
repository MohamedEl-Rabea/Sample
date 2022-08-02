using MediatR;
using Microsoft.Extensions.Localization;
using Moj.CMS.Domain.Aggregates.Case;
using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Shared;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Case.Commands.UpdateCaseVIban
{
    public class DeactivateCaseVIbanCommand : Command<IResult>
    {
        public object UpdateCaseVIbanDto { get; set; }
    }

    public class DeactivateCaseVIbanHandler : IRequestHandler<DeactivateCaseVIbanCommand, IResult>
    {
        private readonly ICaseRepository _caseRepository;

        public DeactivateCaseVIbanHandler(ICaseRepository caseRepository)
        {
            _caseRepository = caseRepository;
        }

        public async Task<IResult> Handle(DeactivateCaseVIbanCommand request, CancellationToken cancellationToken)
        {
            var caseAggregates = await _caseRepository.GetAllAsync(c => c.CaseStatus != CaseStatusEnum.Closed);
            //caseAggregates.ForAll(c => c.DeactivateAllVIbans());
            return Result.Success();
        }
    }

}
