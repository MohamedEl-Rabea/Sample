using System.Collections.Generic;

namespace Moj.CMS.UserAccess.Application.DTO
{
    public class PermissionResponseDto
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public List<PermissionDto> Permissions { get; set; }
    }
}