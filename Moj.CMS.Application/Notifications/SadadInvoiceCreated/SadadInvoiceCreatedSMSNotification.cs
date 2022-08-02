using System.Threading.Tasks;
using Moj.CMS.Shared.Notifications.Channels;
using Moj.CMS.Shared.Notifications.Dispatchers.SMS;
using System.Collections.Generic;

namespace Moj.CMS.Application.Notifications.SadadInvoiceCreated
{
    public class SadadInvoiceCreatedSMSNotification<TArgs> : ISmsNotifications<TArgs>
        where TArgs : SadadInvoiceNotificationInput
    {
        private readonly ISMSDispatcher _smsDispatcher;

        public SadadInvoiceCreatedSMSNotification(ISMSDispatcher smsDispatcher)
        {
            _smsDispatcher = smsDispatcher;
        }

        public async Task<object> GetTemplateModelAsync(TArgs args)
        {
            return await Task.FromResult(args);
        }

        public async Task SendAsync(TArgs args)
        {
            if (!string.IsNullOrEmpty(args.Template))
            {
                await _smsDispatcher.DispatchAsync(new SMSInput
                {
                    DestinationPhoneNumbers = new List<string> { "", "" },// "TODO",// Add your phone number to test
                    Message = args.Template
                });
            }
        }
    }
}
