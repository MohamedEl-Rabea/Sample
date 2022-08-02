using MediatR;
using Moj.CMS.Domain.Aggregates.Case;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Case.Commands.CloseSadadInvoice
{
    public class CloseSadadInvoicesCommand : Command<IResult>
    {
    }

    public class CloseSadadInvoicesCommandHandler : IRequestHandler<CloseSadadInvoicesCommand, IResult>
    {
        private readonly ICaseRepository _caseRepository;

        public CloseSadadInvoicesCommandHandler(ICaseRepository caseRepository)
        {
            _caseRepository = caseRepository;
        }

        public async Task<IResult> Handle(CloseSadadInvoicesCommand request, CancellationToken cancellationToken)
        {
            //var casesAggregate = (await _caseRepository.GetAllIncludingAsync(c => !c.IsDeleted && c.CaseStatusId != CaseStatusEnum.Closed, c => c.SadadList.Where(s => s.IsActive)));
            //foreach (var cse in casesAggregate)
            //{
            //    foreach (var sadad in cse.SadadList)
            //    {
            //        sadad.Deactivate();
            //    }
            //}
            return Result.Success();
        }
    }
}
