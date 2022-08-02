using System.Collections.Generic;

namespace Moj.CMS.Shared.Constants.Permission
{
    public class Permission
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public IEnumerable<Permission> Permissions { get; set; }
    }
}