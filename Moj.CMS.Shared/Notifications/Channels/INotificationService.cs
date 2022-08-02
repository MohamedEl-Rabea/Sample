using System.Threading.Tasks;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Shared.Notifications.Channels
{
    [ScopedService]
    public interface INotificationService<TArgs>
    {
        Task<object> GetTemplateModelAsync(TArgs args);
        Task SendAsync(TArgs args);
    }
}