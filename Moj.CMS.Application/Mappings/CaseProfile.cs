using AutoMapper;
using Moj.CMS.Application.AppServices.Case.Commands.AddSadadInvoice;
using Moj.CMS.Application.AppServices.Case.Commands.UpdateSadadInvoice;
using Moj.CMS.Domain.ParameterObjects.Case;

namespace Moj.CMS.Application.Mappings
{
    public class CaseProfile : Profile
    {
        public CaseProfile()
        {
            CreateMap<CreateCaseSadadDto, CreateSadadParam>().ReverseMap();
            CreateMap<UpdateCaseSadadDto, UpdateSadadParam>().ReverseMap();
        }
    }
}
