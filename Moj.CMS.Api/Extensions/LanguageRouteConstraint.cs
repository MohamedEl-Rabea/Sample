using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Moj.CMS.Api.Extensions
{
    public class LanguageRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            string pat = @"^[a-z]{2}(-[A-Z]{2})?$";//matches ar,en,ar-EG,en-US
            var regex = new Regex(pat);

            var selectedCulture = httpContext.Request.Query["culture"].ToString().Trim();
            if (!regex.IsMatch(selectedCulture))
            {
                CultureInfo.CurrentCulture = new CultureInfo("ar");
                CultureInfo.CurrentUICulture = new CultureInfo("ar");
                return true;
            }
            CultureInfo.CurrentCulture = new CultureInfo(selectedCulture);
            CultureInfo.CurrentUICulture = new CultureInfo(selectedCulture);
            return true;
        }
    }
}
