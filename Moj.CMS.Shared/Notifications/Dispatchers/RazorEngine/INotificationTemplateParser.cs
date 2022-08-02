using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Shared.Notifications.Dispatchers.RazorEngine
{
    [ScopedService]
    public interface INotificationTemplateParser
    {
        string Parse(string template, string templateName, object model);
    }
}
