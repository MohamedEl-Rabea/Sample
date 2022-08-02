using MediatR;
using Moj.CMS.Application.AppServices.Party.Services;
using Moj.CMS.Application.Dtos;
using Moj.CMS.Application.Extensions;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Party.Commands.AddParty
{
    public class AddPartiesCommand : Command<Result<ResourceCreatedDto>>
    {
        public IEnumerable<PartyDto> Parties { get; set; }

    }

    public class AddPartiesCommandHandler : IRequestHandler<AddPartiesCommand, Result<ResourceCreatedDto>>
    {
        private readonly IPartyService _partyService;

        public AddPartiesCommandHandler(IPartyService partyService)
        {
            _partyService = partyService;
        }

        public async Task<Result<ResourceCreatedDto>> Handle(AddPartiesCommand request, CancellationToken cancellationToken)
        {
            var result = await _partyService.AddPartiesAsync(request.Parties, request.Parties, false);
            var resultIds = result.Select(r => r.Id);
            return Result<ResourceCreatedDto>.Success(resultIds.MapToResourceCreatedDto());
        }
    }
}