namespace Moj.CMS.Shared.Models.Mail
{
    public class MailRequestDto
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string From { get; set; }
    }
}