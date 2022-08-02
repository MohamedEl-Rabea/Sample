using Moj.CMS.Domain.Shared.Audit;

namespace Moj.CMS.Shared.Notifications.Dispatchers.Email
{
    public class EmailSettings : AuditedEntity
    {
        public string EmailHost { get; set; }
        public int Port { get; set; }
        public bool UseSsl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public bool IsActive { get; set; }
    }
}
