using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Moj.CMS.UserAccess.Application.DTO;

namespace Moj.CMS.UserAccess.Application.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleDto, IdentityRole>().ReverseMap();
        }
    }
}