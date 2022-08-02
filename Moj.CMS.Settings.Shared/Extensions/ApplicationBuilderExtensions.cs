using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Hosting;
using Moj.CMS.Shared.Constants.Localization;
using Moj.CMS.Shared.Constants.Role;
using SSS.BackgroundJobs.Hangfire;
using System.Globalization;
using System.Linq;

namespace Moj.CMS.Settings.Shared.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            return app;
        }

        public static IApplicationBuilder UseRequestLocalizationByCulture(this IApplicationBuilder app)
        {
            var supportedCultures = LocalizationConstants.SupportedLanguages.Select(l => new CultureInfo(l.Code)).ToArray();
            app.UseRequestLocalization(options =>
            {
                options.SupportedUICultures = supportedCultures;
                options.SupportedCultures = supportedCultures;
                options.DefaultRequestCulture = new RequestCulture(supportedCultures.First());
                //options.ApplyCurrentCultureToResponseHeaders = true;
            });

            app.UseMiddleware<RequestCultureMiddleware>();

            return app;
        }

        public static IApplicationBuilder UseHangfireDashboard(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            var allowedRoles = new string[] {
                RoleConstants.AdministratorRole
            };
            app.ConfigureHangfireDashboard(env, "MOJ Background Jobs", allowedRoles);
            return app;
        }
    }
}