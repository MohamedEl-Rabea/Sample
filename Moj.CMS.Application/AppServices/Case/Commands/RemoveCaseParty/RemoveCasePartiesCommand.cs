using MediatR;
using Microsoft.Extensions.Localization;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Domain.Aggregates.Case;
using Moj.CMS.Shared;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Case.Commands.RemoveCaseParty
{
    public class RemoveCasePartiesCommand : Command<IResult>
    {
        public IEnumerable<RemoveCasePartiesDto> RemoveCasePartiesDtoList { get; set; }
    }

    public class RemoveCasePartiesCommandHandler : IRequestHandler<RemoveCasePartiesCommand, IResult>
    {
        private readonly IPartyQueries _partyQueries;
        private readonly ICaseRepository _caseRepository;
        private readonly IStringLocalizer<CMSLocalizer> _localizer;

        public RemoveCasePartiesCommandHandler(IPartyQueries partyQueries, ICaseRepository caseRepository)
        {
            _partyQueries = partyQueries;
            _caseRepository = caseRepository;
        }

        public async Task<IResult> Handle(RemoveCasePartiesCommand request, CancellationToken cancellationToken)
        {
            foreach (var removeCasePartyDto in request.RemoveCasePartiesDtoList)
            {
                // validate if no case exist
                var caseAggregate = await _caseRepository.GetCaseByNumberAsync(removeCasePartyDto.CaseNumber);
                var partiesNumbers = (await _partyQueries.GetPartiesBasicInfoByNumbersAsync(removeCasePartyDto.PartiesIdentityNumbers))
                    .Select(p => p.Number);
                caseAggregate.RemoveParties(partiesNumbers);
            }
            return Result.Success();
        }
    }
}
