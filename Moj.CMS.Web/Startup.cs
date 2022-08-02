using Blazored.LocalStorage;
using Blazored.SessionStorage;
using MatBlazor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Moj.CMS.Shared.Constants.Application;
using Moj.CMS.Shared.Extensions;
using Moj.CMS.Web.Extensions;
using Moj.CMS.Web.Middlewares;
using MudBlazor;
using MudBlazor.Services;
using System;
using System.IO;
using Moj.CMS.Settings.Shared.Extensions;
using Moj.CMS.Settings.Shared.Helpers;

namespace Moj.CMS.Web
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMatBlazor();
            services.AddCommonServices(Configuration, _env);
            services.AddBlazoredLocalStorage();
            services.AddBlazoredSessionStorage();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddMudServices();
            services.AddCookieAuthentication(services.GetApplicationSettings(Configuration));
            services.AddControllers();
            services.AddLazyCache();
            services.AddMudServices(configuration =>
                {
                    configuration.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
                    configuration.SnackbarConfiguration.HideTransitionDuration = 100;
                    configuration.SnackbarConfiguration.ShowTransitionDuration = 100;
                    configuration.SnackbarConfiguration.VisibleStateDuration = 3000;
                    configuration.SnackbarConfiguration.ShowCloseIcon = false;
                });
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseExceptionHandling(_env);
            app.UseHttpsRedirection();

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseStaticFiles();
            if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Files")))
            {
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Files"));
            }
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Files")),
                RequestPath = new PathString("/Files")
            });
            app.UseRequestLocalizationByCulture();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            //https://github.com/dotnet/aspnetcore/issues/13601
            app.UseMiddleware<BlazorCookieLoginMiddleware>();

            app.UseHangfireDashboard(_env);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });

            Initialize(app.ApplicationServices, _env);
            
            SetClientTimeZone();
        }

        private void Initialize(IServiceProvider applicationServices, IWebHostEnvironment env)
        {
            using var scope = applicationServices.CreateScope();
            var appInitializer = scope.ServiceProvider.GetService<IAppInitializer>();
            appInitializer.Initialize(scope.ServiceProvider, env.EnvironmentName);
        }

        private void SetClientTimeZone()
        {
            DateExtensions.TimeZone = _env.IsProduction()
                ? TimeZoneInfo.FindSystemTimeZoneById(ApplicationTimeZone.TimeZone)
                : TimeZoneInfo.Local;
        }
    }
}
