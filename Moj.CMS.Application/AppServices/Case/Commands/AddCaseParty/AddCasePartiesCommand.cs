using MediatR;
using Moj.CMS.Application.AppServices.Party.Services;
using Moj.CMS.Domain.Aggregates.Case;
using Moj.CMS.Domain.Aggregates.Case.ValueObjects;
using Moj.CMS.Domain.ParameterObjects.Case;
using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Case.Commands.AddCaseParty
{
    public class AddCasePartiesCommand : Command<IResult>
    {
        public IEnumerable<AddCasePartiesDto> AddCasePartiesDtoList { get; set; }
    }

    public class AddCasePartiesCommandHandler : IRequestHandler<AddCasePartiesCommand, IResult>
    {
        private readonly ICaseRepository _caseRepository;
        private readonly IPartyService _partyService;

        public AddCasePartiesCommandHandler(ICaseRepository caseRepository, IPartyService partyService)
        {
            _caseRepository = caseRepository;
            _partyService = partyService;
        }

        public async Task<IResult> Handle(AddCasePartiesCommand request, CancellationToken cancellationToken)
        {
            var requestCasesNumbers = request.AddCasePartiesDtoList.Select(c => c.CaseNumber);
            var casesAggregates = await _caseRepository.GetAllAsync(c => requestCasesNumbers.Contains(c.CaseNumber));
            var nonExistCaseNumbers = requestCasesNumbers.Except(casesAggregates.Select(c => c.CaseNumber));
            if (nonExistCaseNumbers.Any())
                throw new Exception($"Case/s with the following numbers [{string.Join(",", nonExistCaseNumbers.ToList())}] not exist.");

            foreach (var casePartyDto in request.AddCasePartiesDtoList)
            {
                var caseAggregate = casesAggregates.Single(c => c.CaseNumber == casePartyDto.CaseNumber);
                var promissoryNumber = caseAggregate.CasePromissories.Single().PromissoryNumber;
                var caseParties = await CreateCasePartyDetailsList(casePartyDto, promissoryNumber);
                caseAggregate.AssignParties(caseParties);
            }
            return Result.Success();
        }

        private async Task<IEnumerable<CaseParty>> CreateCasePartyDetailsList(AddCasePartiesDto casePartyDetails, string promissoryNumber)
        {
            await _partyService.AddPartiesAsync(casePartyDetails.Requesters
                .Select(p => p.Details), casePartyDetails.Respondents.Select(p => p.Details), true);

            var requesters = casePartyDetails.Requesters.Select(rq =>
                CaseParty.Create(new CasePartyCreationParam
                {
                    PartyNumber = rq.Details.PartyNumber,
                    PromissoryNumber = promissoryNumber,
                    PartyRoleTypeId = rq.PartyRole,
                    IsApplicant = rq.IsApplicant,
                    PartyClassificationId = PartyClassificationEnum.Requester
                }
                ));

            var respondents = casePartyDetails.Respondents.Select(rp =>
                CaseParty.Create(new CasePartyCreationParam
                {
                    PartyNumber = rp.Details.PartyNumber,
                    PromissoryNumber = promissoryNumber,
                    PartyRoleTypeId = rp.PartyRole,
                    IsApplicant = rp.IsApplicant,
                    PartyClassificationId = PartyClassificationEnum.Respondent
                }
                ));

            return requesters.Union(respondents);
        }
    }
}
