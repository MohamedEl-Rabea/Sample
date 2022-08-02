using System.Collections.Generic;
namespace Moj.CMS.Shared.Notifications.Dispatchers.SMS
{
    public class SMSInput
    {
        public IEnumerable<string> DestinationPhoneNumbers { get; set; }
        public string Message { get; set; }
    }
}
