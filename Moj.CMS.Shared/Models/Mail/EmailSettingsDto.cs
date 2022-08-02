namespace Moj.CMS.Shared.Models.Mail
{
    public class EmailSettingsDto
    {
        public int Id { get; set; }
        public string EmailHost { get; set; }
        public int Port { get; set; }
        public bool UseSsl { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public bool IsActive { get; set; }
    }
}
