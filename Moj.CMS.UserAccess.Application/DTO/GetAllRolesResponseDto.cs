using System.Collections.Generic;

namespace Moj.CMS.UserAccess.Application.DTO
{
    public class GetAllRolesResponseDto
    {
        public IEnumerable<RoleDto> Roles { get; set; }
    }
}