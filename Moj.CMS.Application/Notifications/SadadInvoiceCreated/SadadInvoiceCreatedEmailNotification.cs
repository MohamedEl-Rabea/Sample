using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moj.CMS.Shared;
using Moj.CMS.Shared.Notifications.Channels;
using Moj.CMS.Shared.Notifications.Dispatchers.Email;

namespace Moj.CMS.Application.Notifications.SadadInvoiceCreated
{
    public class SadadInvoiceCreatedEmailNotification<TArgs> : IEmailNotifications<TArgs>
        where TArgs : SadadInvoiceNotificationInput
    {
        private readonly IEmailDispatcher _emailDispatcher;
        private readonly IStringLocalizer<CMSLocalizer> _localizer;

        public SadadInvoiceCreatedEmailNotification(IEmailDispatcher emailDispatcher, IStringLocalizer<CMSLocalizer> localizer)
        {
            _emailDispatcher = emailDispatcher;
            _localizer = localizer;
        }

        public async Task<object> GetTemplateModelAsync(TArgs args)
        {
            return await Task.FromResult((object)args);
        }

        public async Task SendAsync(TArgs args)
        {

            if (!string.IsNullOrEmpty(args.Template))
            {
                await _emailDispatcher.DispatchAsync(new EmailInput
                {
                    To = new List<string> { "TODO" },// Add your email to test
                    Subject = _localizer["SadadInvoiceCreated"],
                    MessageBody = args.Template
                });
            }
        }
    }
}
