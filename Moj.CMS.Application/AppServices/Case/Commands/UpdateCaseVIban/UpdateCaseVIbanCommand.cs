using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using Moj.CMS.Domain.Aggregates.Case;
using Moj.CMS.Domain.ParameterObjects.Case;
using Moj.CMS.Shared;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Case.Commands.UpdateCaseVIban
{
    public class UpdateCaseVIbanCommand : Command<IResult>
    {
        public UpdateCaseVIbanDto UpdateCaseVIbanDto { get; set; }
    }

    public class UpdateCaseVIbanCommandHandler : IRequestHandler<UpdateCaseVIbanCommand, IResult>
    {
        private readonly ICaseRepository _caseRepository;
        private readonly IMapper _mapper;

        public UpdateCaseVIbanCommandHandler(ICaseRepository caseRepository, IMapper mapper)
        {
            _caseRepository = caseRepository;
            _mapper = mapper;
        }

        public async Task<IResult> Handle(UpdateCaseVIbanCommand request, CancellationToken cancellationToken)
        {
            //var updateVIbanParam = _mapper.Map<UpdateVIbanParam>(request.UpdateCaseVIbanDto);
            //var caseAggregate = await _caseRepository.GetCaseByNumberAsync(request.UpdateCaseVIbanDto.CaseNumber);
            ////caseAggregate.EditVIban(updateVIbanParam);
            return Result.Success();
        }
    }
}