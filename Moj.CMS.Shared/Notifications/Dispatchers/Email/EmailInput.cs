using System.Collections.Generic;

namespace Moj.CMS.Shared.Notifications.Dispatchers.Email
{
    public class EmailInput
    {
        public List<string> To { get; set; }
        public List<string> CC { get; set; } = new List<string>();
        public string MessageBody { get; set; }
        public string Subject { get; set; }
    }
}
