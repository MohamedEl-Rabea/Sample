using AutoMapper;
using Moj.CMS.Shared.Models.Mail;
using Moj.CMS.Shared.Models.SMS;
using Moj.CMS.Shared.Notifications.Dispatchers.Email;
using Moj.CMS.Shared.Notifications.Dispatchers.SMS;

namespace Moj.CMS.Shared.Mappings
{
    public class SettingsProfile : Profile
    {
        public SettingsProfile()
        {
            CreateMap<EmailSettings, EmailSettingsDto>()
                .ForMember(dest => dest.Email, opt =>
                    opt.MapFrom(src => src.UserName))
                .ReverseMap();
            CreateMap<SMSSettings, SmsSettingsDto>().ReverseMap();
        }
    }
}
