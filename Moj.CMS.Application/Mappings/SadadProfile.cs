using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using Moj.CMS.Application.AppServices.SadadInvoice.Dtos;
using Moj.CMS.Application.Jobs.CreateSadadInvoice;
using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Integration.Contracts.ThirdParties.Tahseel.Dto;

namespace Moj.CMS.Application.Mappings
{
    public class SadadProfile : Profile
    {
        public SadadProfile()
        {
            CreateMap<CreateSadadInvoiceJobInput, SadadInvoiceCreationRequest>()
                .ForMember(dest => dest.DisplayLabelAr, opt => opt.MapFrom(src => src.DisplayLabel.AR))
                .ForMember(dest => dest.DisplayLabelEn, opt => opt.MapFrom(src => src.DisplayLabel.EN))
                .ForMember(dest => dest.PartyIdentityTypeId, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    switch (src.PartyIdentityTypeId)
                    {
                        case PartyIdentityTypeEnum.SaudiNationalId:
                            dest.PartyIdentityTypeId = BenefeciaryIdentityType.NationalId;
                            break;
                        case PartyIdentityTypeEnum.ResidencyId:
                            dest.PartyIdentityTypeId = BenefeciaryIdentityType.Iqama;
                            break;
                        case PartyIdentityTypeEnum.TemporaryId:
                            dest.PartyIdentityTypeId = BenefeciaryIdentityType.BusinessID; //TODO
                            break;
                        case PartyIdentityTypeEnum.Passport:
                            dest.PartyIdentityTypeId = BenefeciaryIdentityType.PassportNumber;
                            break;
                        case PartyIdentityTypeEnum.GulfNational:
                            dest.PartyIdentityTypeId = BenefeciaryIdentityType.GCCPassportNumber;
                            break;
                        case PartyIdentityTypeEnum.BorderNumber:
                            dest.PartyIdentityTypeId = BenefeciaryIdentityType.BorderNumber;
                            break;
                        default:
                            dest.PartyIdentityTypeId = BenefeciaryIdentityType.NationalId;
                            break;
                    }
                });

            CreateMap<CreateSadadInvoiceDto, CreateSadadInvoiceJobInput>();

            CreateMap<CreateSadadInvoiceDto, SadadInvoiceCreationRequest>()
                .ForMember(dest => dest.DisplayLabelAr, opt =>
                    opt.MapFrom(src => src.DisplayLabel.AR))
                .ForMember(dest => dest.DisplayLabelEn, opt =>
                    opt.MapFrom(src => src.DisplayLabel.EN));
        }
    }
}
