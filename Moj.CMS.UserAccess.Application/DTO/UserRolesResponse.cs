using System.Collections.Generic;

namespace Moj.CMS.UserAccess.Application.DTO
{
    public class UserRolesResponseDto
    {
        public List<UserRoleModel> UserRoles { get; set; } = new List<UserRoleModel>();
    }

    public class UserRoleModel
    {
        public string RoleName { get; set; }
        public bool Selected { get; set; }
    }
}