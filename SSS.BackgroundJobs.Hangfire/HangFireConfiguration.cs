using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace SSS.BackgroundJobs.Hangfire
{
    public static class HangFireConfiguration
    {
        public static IServiceCollection ConfigureHangFire(this IServiceCollection services, IWebHostEnvironment env, IConfiguration configuration)
        {
            if (!env.IsProduction())
            {
                services.AddHangfire(config => config.UseInMemoryStorage());
            }
            else
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                services.AddHangfire(config => config
               .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
               .UseSimpleAssemblyNameTypeSerializer()
               .UseRecommendedSerializerSettings()
               .UseSqlServerStorage(connectionString, new SqlServerStorageOptions
               {
                   CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                   SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                   QueuePollInterval = TimeSpan.Zero,
                   UseRecommendedIsolationLevel = true,
                   DisableGlobalLocks = true,
                   PrepareSchemaIfNecessary = true
               }));
            }
            // Add the processing server as IHostedService
            services.AddHangfireServer();
            return services;
        }
        public static IApplicationBuilder ConfigureHangfireDashboard(this IApplicationBuilder app, IWebHostEnvironment env,string title 
            , params string[] allowdRoles)
        {
            app.UseHangfireDashboard("/jobs", new DashboardOptions
            {
                Authorization = new[] { new HangfireDashboardAuthorizationFilter(env, allowdRoles) },
                DashboardTitle = title,
            });
            return app;
        }
    }
}
