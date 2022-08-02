using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moj.CMS.Shared.Notifications.Channels;

namespace Moj.CMS.Shared.Notifications.Dispatchers.RazorEngine
{
    public class NotificationTemplateProvider : INotificationTemplateProvider
    {
        private readonly INotificationTemplateParser _notificationTemplateParser;

        public NotificationTemplateProvider(INotificationTemplateParser notificationTemplateParser)
        {
            _notificationTemplateParser = notificationTemplateParser;
        }

        public virtual async Task<string> GetTemplateAsync(object model, Type notificationServiceType)
        {
            //1- Get template name to use it in reading the template file, and a razor parsing key 
            var templateName = notificationServiceType.Name.Remove(notificationServiceType.Name.IndexOf('`'));

            //2- Read file bytes based on Template Path + TName + "-ar / -en" + .HTML
            var template = await ReadTemplateFile(templateName, notificationServiceType);

            //3- Parse template content using razor engine
            var parsedTemplate = _notificationTemplateParser.Parse(template, templateName, model);

            return parsedTemplate;
        }

        private async Task<string> ReadTemplateFile(string templateName, Type notificationServiceType)
        {
            var template = "";

            var language = Thread.CurrentThread.CurrentCulture.Name;

            var extension = notificationServiceType.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ISmsNotifications<>))
                ? "txt"
                : "html";

            var resourcePath = notificationServiceType.Assembly.GetManifestResourceNames()
                .FirstOrDefault(r => r.EndsWith($"{templateName}-{language}.{extension}"));

            await using (Stream stream = notificationServiceType.Assembly.GetManifestResourceStream(resourcePath))
            {
                using StreamReader reader = new StreamReader(stream);
                template = await reader.ReadToEndAsync();
            }

            return template;
        }
    }
}
