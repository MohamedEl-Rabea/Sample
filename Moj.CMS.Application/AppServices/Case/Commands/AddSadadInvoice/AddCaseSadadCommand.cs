using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Application.Notifications.SadadInvoiceCreated;
using Moj.CMS.Domain.Aggregates.Case;
using Moj.CMS.Domain.Aggregates.Case.Entities;
using Moj.CMS.Domain.ParameterObjects.Case;
using Moj.CMS.Shared;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Notifications;
using Moj.CMS.Shared.Wrapper;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Case.Commands.AddSadadInvoice
{
    public class AddCaseSadadCommand : Command<IResult>
    {
        public CreateCaseSadadDto CaseSadadDto { get; set; }
    }

    public class AddCaseSadadCommandHandler : IRequestHandler<AddCaseSadadCommand, IResult>
    {
        private readonly IPartyQueries _partyQueries;
        private readonly ICaseRepository _caseRepository;
        private readonly IMapper _mapper;
        private readonly INotificationManager _notificationManager;

        public AddCaseSadadCommandHandler(IPartyQueries partyQueries, ICaseRepository caseRepository,
            IMapper mapper,
            INotificationManager notificationManager)
        {
            _partyQueries = partyQueries;
            _caseRepository = caseRepository;
            _mapper = mapper;
            _notificationManager = notificationManager;
        }

        public async Task<IResult> Handle(AddCaseSadadCommand request, CancellationToken cancellationToken)
        {
            var partyId = (await _partyQueries.GetPartyIdByNumberAsync(request.CaseSadadDto?.PartyIdentityNumber)).Id;

            var createSadadParam = _mapper.Map<CreateSadadParam>(request.CaseSadadDto);

            if (partyId != 0)
                createSadadParam.PartyId = partyId;
            else
                throw new Exception("Party identity number is not exist or not correct");

            var sadad = CaseSadad.Create(createSadadParam);
            var caseAggregate = await _caseRepository.GetCaseByNumberAsync(request.CaseSadadDto.CaseNumber);
            //caseAggregate.CreateSadad(sadad);
            await _notificationManager.Notify(new SadadInvoiceNotificationInput
            {
                CaseNumber = request.CaseSadadDto.CaseNumber,
                InvoiceNumber = sadad.SadadNumber
            });
            return Result.Success();
        }
    }
}
