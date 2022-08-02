using Moj.CMS.Shared.Notifications.Channels;

namespace Moj.CMS.Application.Notifications.SadadInvoiceCreated
{
    public class SadadInvoiceNotificationInput : NotificationInputBase
    {
        public string CaseNumber { get; set; }

        public string InvoiceNumber { get; set; }
    }
}
