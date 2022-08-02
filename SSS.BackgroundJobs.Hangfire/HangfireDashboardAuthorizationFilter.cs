using Hangfire.Annotations;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace SSS.BackgroundJobs.Hangfire
{
    public class HangfireDashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {
        private readonly IWebHostEnvironment _env;
        private string[] _allowedRoles;
        public HangfireDashboardAuthorizationFilter(IWebHostEnvironment env, params string[] allowdRoles)
        {
            _env = env;
            _allowedRoles = allowdRoles;
        }
        public bool Authorize([NotNull] DashboardContext context)
        {
            if (_env.IsDevelopment())
                return true;

            // Allow all authenticated users to see the Dashboard (potentially dangerous).
            var httpContext = context.GetHttpContext();
            if (httpContext.User.Identity.IsAuthenticated
                && _allowedRoles.Any(r=> httpContext.User.IsInRole(r)))
            {
                return true;
            }
            return false;
        }
    }
}
