using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Domain.Aggregates.Case;
using Moj.CMS.Domain.ParameterObjects.Case;
using Moj.CMS.Shared;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Case.Commands.UpdateSadadInvoice
{
    public class UpdateCaseSadadCommand : Command<IResult>
    {
        public UpdateCaseSadadDto UpdateCaseSadadDto { get; set; }
    }

    public class UpdateCaseSadadCommandHandler : IRequestHandler<UpdateCaseSadadCommand, IResult>
    {
        private readonly IPartyQueries _partyQueries;
        private readonly ICaseRepository _caseRepository;
        private readonly IMapper _mapper;

        public UpdateCaseSadadCommandHandler(IPartyQueries partyQueries, ICaseRepository caseRepository)
        {
            _partyQueries = partyQueries;
            _caseRepository = caseRepository;
        }

        public async Task<IResult> Handle(UpdateCaseSadadCommand request, CancellationToken cancellationToken)
        {
            var paertId = (await _partyQueries.GetPartyIdByNumberAsync(request.UpdateCaseSadadDto?.PartyIdentityNumber)).Id;

            var updateSadadParam = _mapper.Map<UpdateSadadParam>(request.UpdateCaseSadadDto);

            if (paertId != 0)
                updateSadadParam.PartyId = paertId;
            else
                throw new Exception("Party identity number is not exist or not correct");

            var caseAggregate = await _caseRepository.GetCaseByNumberAsync(request.UpdateCaseSadadDto.CaseNumber);
            //caseAggregate.UpdateSadad(updateSadadParam);
            return Result.Success();
        }
    }
}