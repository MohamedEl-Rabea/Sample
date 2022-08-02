namespace Moj.CMS.Shared.Models.SMS
{
    public class SmsSettingsDto
    {
        public int Id { get; set; }
        public string SourcePhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
    }
}
