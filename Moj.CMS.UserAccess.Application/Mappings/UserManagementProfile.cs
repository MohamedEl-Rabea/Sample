using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Moj.CMS.Domain.Shared.Values;
using Moj.CMS.Shared.Models;
using Moj.CMS.UserAccess.Application.DTO;
using Moj.CMS.UserAccess.Application.Models;

namespace Moj.CMS.UserAccess.Application.Mappings
{
    public class UserManagementProfile : Profile
    {
        public UserManagementProfile()
        {
            CreateMap<Document, DocumentDto>().ReverseMap();
            CreateMap<CMSUser, UserDto>().ReverseMap();
            CreateMap<IdentityRole, RoleDto>().ReverseMap();
        }
    }
}
