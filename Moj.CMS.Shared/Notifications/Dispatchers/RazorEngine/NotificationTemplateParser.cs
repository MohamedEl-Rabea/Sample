using RazorEngine;
using RazorEngine.Templating;

namespace Moj.CMS.Shared.Notifications.Dispatchers.RazorEngine
{
    public class NotificationTemplateParser : INotificationTemplateParser
    {
        public string Parse(string template, string templateName, object model)
        {
            string parsedTemplate = Engine.Razor.RunCompile(template, templateName, model.GetType(), model);
            return parsedTemplate;
        }
    }
}
