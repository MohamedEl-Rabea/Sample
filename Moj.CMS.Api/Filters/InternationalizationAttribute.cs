using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Moj.CMS.Api.Filters
{
    public class InternationalizationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string pat = @"^[a-z]{2}(-[A-Z]{2})?$";//matches ar,en,ar-EG,en-US
            Regex regex = new Regex(pat);
            //if (!regex.IsMatch(values["culture"].ToString().Trim()))
            var SelectedCulture = context.HttpContext.Request.Query["culture"].ToString().Trim();
            if (!regex.IsMatch(SelectedCulture))
            {
                CultureInfo.CurrentCulture = new CultureInfo("ar", true);
                CultureInfo.CurrentUICulture = new CultureInfo("ar", true);
                return;
            }
            CultureInfo.CurrentCulture = new CultureInfo(SelectedCulture, true);
            CultureInfo.CurrentUICulture = new CultureInfo(SelectedCulture, true);
        }
    }
}
