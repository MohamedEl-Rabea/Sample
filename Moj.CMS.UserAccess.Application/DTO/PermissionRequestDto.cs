using System.Collections.Generic;

namespace Moj.CMS.UserAccess.Application.DTO
{
    public class PermissionRequestDto
    {
        public string RoleId { get; set; }
        public IList<string> RolePermissions { get; set; }
    }
}