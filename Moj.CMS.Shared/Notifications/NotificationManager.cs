using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Moj.CMS.Shared.Notifications.Channels;
using Moj.CMS.Shared.Notifications.Dispatchers.RazorEngine;

namespace Moj.CMS.Shared.Notifications
{
    public class NotificationManager : INotificationManager
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly INotificationTemplateProvider _templateProvider;

        public NotificationManager(IServiceProvider serviceProvider, INotificationTemplateProvider templateProvider)
        {
            _serviceProvider = serviceProvider;
            _templateProvider = templateProvider;
        }

        public async Task Notify<TArgs>(TArgs args) where TArgs : NotificationInputBase
        {
            var notifications = _serviceProvider.GetServices<INotificationService<TArgs>>();
            foreach (var notification in notifications)
            {
                var templateModel = await notification.GetTemplateModelAsync(args);
                args.Template = await _templateProvider.GetTemplateAsync(templateModel, notification.GetType());
                await notification.SendAsync(args);
            }
        }
    }
}
