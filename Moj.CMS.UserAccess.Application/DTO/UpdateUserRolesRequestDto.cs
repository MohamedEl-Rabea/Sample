using System.Collections.Generic;

namespace Moj.CMS.UserAccess.Application.DTO
{
    public class UpdateUserRolesRequestDto
    {
        public string UserId { get; set; }
        public IList<UserRoleModel> UserRoles { get; set; }
    }
}