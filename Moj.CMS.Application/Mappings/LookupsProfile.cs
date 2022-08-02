using AutoMapper;
using Moj.CMS.Application.Lookups.Area;
using Moj.CMS.Application.Lookups.Judge;
using Moj.CMS.Domain.Shared.LookupModels;

namespace Moj.CMS.Application.Mappings
{
    public class LookupsProfile : Profile
    {
        public LookupsProfile()
        {
            CreateMap<JudgeDto, Judge>().ReverseMap();
            CreateMap<AreaDto, Area>().ReverseMap();
        }
    }
}
