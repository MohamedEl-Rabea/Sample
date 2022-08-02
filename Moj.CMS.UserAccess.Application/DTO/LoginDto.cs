namespace Moj.CMS.UserAccess.Application.DTO
{
    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string UserName { get; set; }
    }
}
