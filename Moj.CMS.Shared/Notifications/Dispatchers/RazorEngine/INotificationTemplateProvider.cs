using System;
using System.Threading.Tasks;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Shared.Notifications.Dispatchers.RazorEngine
{
    [ScopedService]
    public interface INotificationTemplateProvider
    {
        Task<string> GetTemplateAsync(object model, Type notificationServiceType);
    }
}
