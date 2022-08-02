namespace Moj.CMS.UserAccess.Application.DTO
{
    public class PermissionDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string ParentCode { get; set; }
        public bool Selected { get; set; }
    }
}
