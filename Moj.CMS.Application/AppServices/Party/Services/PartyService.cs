using AutoMapper;
using Moj.CMS.Application.AppServices.Party.Commands.AddParty;
using Moj.CMS.Application.AppServices.Party.Queries;
using Moj.CMS.Application.AppServices.VIban.Dtos;
using Moj.CMS.Application.AppServices.VIban.Services;
using Moj.CMS.Application.Interfaces.Queries;
using Moj.CMS.Domain.Aggregates.Iban;
using Moj.CMS.Domain.Aggregates.Party;
using Moj.CMS.Domain.DomainServices.Party;
using Moj.CMS.Domain.ParameterObjects.Party;
using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Domain.Shared.Repositories;
using Moj.CMS.Domain.Shared.Values;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moj.CMS.Application.AppServices.Party.Services
{
    public class PartyService : IPartyService
    {
        private readonly IPartyRepository _partyRepository;
        private readonly IMapper _mapper;
        private readonly IPartyQueries _partyQueries;
        private readonly IRepository<Iban> _ibanRepository;
        private readonly IRepository<Domain.Aggregates.VIban.VIban> _vIbanRepository;
        private readonly IVIbanService _vIbanService;
        private readonly IEnforcePartyNumberIsUnique _enforcePartyNumberIsUnique;

        public PartyService(IPartyRepository partyRepository, IMapper mapper, IPartyQueries partyQueries,
            IRepository<Iban> ibanRepository,
            IRepository<Domain.Aggregates.VIban.VIban> vIbanRepository,
            IVIbanService vIbanService,
            IEnforcePartyNumberIsUnique enforcePartyNumberIsUnique)
        {
            _partyRepository = partyRepository;
            _mapper = mapper;
            _partyQueries = partyQueries;
            _ibanRepository = ibanRepository;
            _vIbanRepository = vIbanRepository;
            _vIbanService = vIbanService;
            _enforcePartyNumberIsUnique = enforcePartyNumberIsUnique;
        }

        public async Task<IEnumerable<SavedPartyInfo>> AddPartiesAsync(IEnumerable<PartyDto> requesters, IEnumerable<PartyDto> respondents, bool ignoreExistingParties)
        {
            var partyList = requesters.Union(respondents);

            await AddPartiesVIbanAsync(respondents);

            var result = new List<SavedPartyInfo>();
            var existPartiesInfo = ignoreExistingParties ? (await _partyQueries.GetPartiesBasicInfoByNumbersAsync(
                partyList.Select(p => p.PartyNumber))).ToList() : new List<PartyBasicInfoDto>();

            result.AddRange(existPartiesInfo.Where(p => p.Id != 0).Select(p => new SavedPartyInfo
            {
                Id = p.Id,
                Number = p.Number
            }));

            var newParties = partyList
                .Where(p => !existPartiesInfo.Any(x => x.Number == p.PartyNumber))
                .ToList();

            var partyInfoParamList = _mapper.Map<List<PartyInfoParam>>(newParties);

            foreach (var partyParam in partyInfoParamList)
            {
                var partyDto = newParties.First(p => p.PartyNumber == partyParam.PartyNumber);
                partyParam.PartyIdentity =
                    PartyIdentity.NewPartyIdentity(partyDto.PartyIdentityNumber, partyDto.PartyIdentityTypeId);
                partyParam.EnforcePartyNumberIsUnique = _enforcePartyNumberIsUnique;
                var party = await Domain.Aggregates.Party.Party.CreateAsync(partyParam);
                await _partyRepository.AddAsync(party);
                result.Add(new SavedPartyInfo
                {
                    Id = party.Id,
                    Number = party.PartyNumber
                });
            }
            return result;
        }

        private async Task AddPartiesVIbanAsync(IEnumerable<PartyDto> respondents)
        {
            var iban = (await _ibanRepository.GetAllListAsync(p =>
                p.IbanReferenceDetails.ReferenceType == IbanPurposeEnum.Aggregator &&
                p.VIbanRemaining > 0 &&
                p.IsActive)).FirstOrDefault();

            if (iban != null)
            {
                foreach (var accusedParty in respondents)
                {
                    var vIban = (await _vIbanRepository.GetAllListAsync(v =>
                        v.Iban == iban.Number &&
                        v.ReferenceDetails.ReferenceType == VIbanReferenceTypeEnum.Party &&
                        v.ReferenceDetails.ReferenceNumber == accusedParty.PartyNumber &&
                        v.IsActive)).FirstOrDefault();

                    if (vIban == null)
                    {
                        await _vIbanService.CreateVIbanAsync(new CreateVIbanDto
                        {
                            ParentAccount = iban.Number,
                            Alias = "TestName", //TODO
                            ReferenceDetails = VIbanReferenceDetails.Create(accusedParty.PartyNumber, VIbanReferenceTypeEnum.Party),
                            CAP = 1000 //TODO
                        });
                    }
                }
            }
        }
    }
}
