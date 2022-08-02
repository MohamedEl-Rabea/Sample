using AutoMapper;
using Moj.CMS.Application.AppServices.Party.Commands.AddParty;
using Moj.CMS.Domain.Aggregates.Party;
using Moj.CMS.Domain.ParameterObjects.Party;

namespace Moj.CMS.Application.Mappings
{
    public class PartyProfile : Profile
    {
        public PartyProfile()
        {
            CreateMap<PartyDto, PartyInfoParam>().ReverseMap();
            CreateMap<Party, PartyDto>();
        }
    }
}
