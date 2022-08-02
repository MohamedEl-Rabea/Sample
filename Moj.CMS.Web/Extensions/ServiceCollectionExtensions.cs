using Microsoft.Extensions.DependencyInjection;
using Moj.CMS.Settings.Shared.Extensions;
using Moj.CMS.UserAccess.Application.Configurations;

namespace Moj.CMS.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCookieAuthentication(this IServiceCollection services, AppConfiguration config)
        {
            services.AddAuthentication("Identity.Application").AddCookie();
            services.AddCommonAuthentication();
            //services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>()
            //    .AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

            //services.AddHttpContextAccessor();
            return services;
        }
    }
}