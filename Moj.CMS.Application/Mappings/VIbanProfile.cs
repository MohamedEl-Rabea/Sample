using AutoMapper;
using Moj.CMS.Application.AppServices.Case.Commands.AddCaseVIban;
using Moj.CMS.Domain.ParameterObjects.VIban;

namespace Moj.CMS.Application.Mappings
{
    public class VIbanProfile : Profile
    {
        public VIbanProfile()
        {
            CreateMap<CreateCaseVIbanDto, CreateVIbanParam>().ReverseMap();
        }
    }
}
