using Moj.CMS.Domain.Shared.Audit;

namespace Moj.CMS.Shared.Notifications.Dispatchers.SMS
{
    public class SMSSettings : AuditedEntity
    {
        public string SourcePhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
    }
}
