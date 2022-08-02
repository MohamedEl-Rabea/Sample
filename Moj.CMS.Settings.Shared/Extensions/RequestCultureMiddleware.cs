using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.Threading.Tasks;

namespace Moj.CMS.Settings.Shared.Extensions
{
    public class RequestCultureMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestCultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var cultureQuery = context.Request.Cookies[".AspNetCore.Culture"];
            if (!string.IsNullOrEmpty(cultureQuery))
            {

            }
            else if (!CultureInfo.CurrentCulture.Name.Contains("ar"))
            {
                CultureInfo.CurrentCulture = new CultureInfo("ar-EG");
                CultureInfo.CurrentUICulture = new CultureInfo("ar-EG");
            }

            await _next(context);
        }
    }
}