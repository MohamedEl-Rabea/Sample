using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace Moj.CMS.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class CultureController : Controller
    {
        /// <summary>
        /// Sends a culture cookie to the user browser to presist the current lang
        /// </summary>
        /// <param name="culture"></param>
        /// <param name="redirectUri"></param>
        /// <returns></returns>
        public IActionResult SetCulture(string culture, string redirectUri)
        {
            if (culture != null)
            {
                HttpContext.Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture, culture)));

            }

            return LocalRedirect(redirectUri);
        }
    }
}
