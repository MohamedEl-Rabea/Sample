namespace Moj.CMS.UserAccess.Application.DTO
{
    public class ChangePasswordRequestDto
    {
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}