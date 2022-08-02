using AutoMapper;
using Moj.CMS.Application.AppServices.Claims.Commands.CreateFinancialClaim;

namespace Moj.CMS.Application.Mappings
{
    public class ClaimProfile : Profile
    {
        public ClaimProfile()
        {
            CreateMap<ClaimDto, CreateClaimDto>().ForMember(dest => dest.Claim,
                options => options.MapFrom(src => src));
        }
    }
}
