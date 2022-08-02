using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moj.CMS.Api.Filters;
using Moj.CMS.Application.Interfaces;
using Moj.CMS.UserAccess.Application.Interfaces;

namespace Moj.CMS.Api.Controllers
{
    [ApiController]
    [Internationalization]
    public class BaseController : ControllerBase
    {
        private ICmsModule _cmsModule;
        protected ICmsModule CmsModule => _cmsModule ??= HttpContext.RequestServices.GetService<ICmsModule>();
    }
}
