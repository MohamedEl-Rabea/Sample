using System.Threading.Tasks;
using Moj.CMS.Shared.Notifications.Channels;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Shared.Notifications
{
    [ScopedService]
    public interface INotificationManager 
    {
        Task Notify<TArgs>(TArgs args) where TArgs : NotificationInputBase;
    }
}
