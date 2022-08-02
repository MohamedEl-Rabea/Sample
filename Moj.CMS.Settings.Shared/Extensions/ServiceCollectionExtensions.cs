using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moj.CMS.Application.DomainServices.SadadInvoice;
using Moj.CMS.Application.Extensions;
using Moj.CMS.Domain.DomainServices.SadadInvoice;
using Moj.CMS.Domain.Shared.Repositories;
using Moj.CMS.Infrastructure.Behaviors;
using Moj.CMS.Infrastructure.Contexts;
using Moj.CMS.Infrastructure.Extensions;
using Moj.CMS.Infrastructure.Repositories;
using Moj.CMS.Integration.Application.Services;
using Moj.CMS.Integration.Contracts.AlAhli_B2B;
using Moj.CMS.Integration.Contracts.Extensions;
using Moj.CMS.Integration.Contracts.Runtime;
using Moj.CMS.Integration.Contracts.ThirdParties.Tahseel;
using Moj.CMS.Shared.Constants.Application;
using Moj.CMS.Shared.Extensions;
using Moj.CMS.Shared.Infrastructure;
using Moj.CMS.Shared.Infrastructure.Repositories;
using Moj.CMS.Shared.Interfaces;
using Moj.CMS.Shared.Permission;
using Moj.CMS.Shared.Settings;
using Moj.CMS.UserAccess.Application.Configurations;
using Moj.CMS.UserAccess.Application.Extensions;
using Moj.CMS.UserAccess.Application.Models;
using Moj.CMS.UserAccess.Infrastructure.Contexts;
using SSS.BackgroundJobs.Hangfire;
using System;
using System.Linq;
using System.Reflection;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Settings.Shared.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static AppConfiguration GetApplicationSettings(
      this IServiceCollection services,
      IConfiguration configuration)
        {
            var applicationSettingsConfiguration = configuration.GetSection(nameof(AppConfiguration));
            services.Configure<AppConfiguration>(applicationSettingsConfiguration);
            return applicationSettingsConfiguration.Get<AppConfiguration>();
        }
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CMSDbContext>(options => options
                    .UseSqlServer(configuration.GetConnectionString("DefaultConnection"), sqlOptions => sqlOptions.EnableRetryOnFailure()));

            services.AddDbContext<UserAccessDbContext>(options => options
                    .UseSqlServer(configuration.GetConnectionString("DefaultConnection"), sqlOptions => sqlOptions.EnableRetryOnFailure()));

            return services;
        }

        public static IServiceCollection AddApplicationSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<PasswordAESEncryptionOptions>(configuration.GetSection(nameof(PasswordAESEncryptionOptions)));
            return services;
        }

        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<CMSUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<UserAccessDbContext>()
            .AddDefaultTokenProviders();

            return services;
        }

        public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MailConfiguration>(configuration.GetSection("MailConfiguration"));
            services.AddTransient(typeof(IQueryBuilderCreator<>), typeof(QueryBuilderCreator<>));
            return services;
        }
        public static IServiceCollection AddApplicationRepositories(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient(typeof(IRepository<,>), typeof(Repository<,>));
            services.AddTransient(typeof(IDomainRepository<>), typeof(BaseDomainRepository<>));
            services.AddTransient(typeof(IDomainRepository<,>), typeof(BaseDomainRepository<,>));
            return services;
        }

        public static IServiceCollection AddCommonServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            services.AddIdentity();
            services.AddDatabase(configuration);
            services.AddSharedApplicationLayer();
            services.AddApplicationLayer();
            services.AddUserAccessApplicationLayer();
            services.AddApplicationRepositories();
            services.AddSharedInfrastructure(configuration);
            services.AddInfrastructureMappings();
            services.RegisterMarkedServices(env);
            services.RegisterBahaviours();
            services.AddLocalization(options =>
            {
                options.ResourcesPath = "Resources";
            });
            AddContextAccessors(services);
            services.AddApplicationSettings(configuration);
            services.ConfigureHangFire(env, configuration);
            services.AddServiceAgents(configuration);
            return services;
        }

        private static void AddContextAccessors(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IIntegrationExecutionContextAccessor, IntegrationExecutionContextAccessor>();
        }

        private static IServiceCollection RegisterBahaviours(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehaviour<,>));
            return services;
        }

        public static IServiceCollection RegisterMarkedServices(this IServiceCollection services, IWebHostEnvironment env)
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var assembliesToBeScanned = assemblies.Where(a => env.EnvironmentName == AppEnvironments.Testing || !a.FullName.Contains("Test"));
            services.AddServicesOfAllTypes(assembliesToBeScanned);
            services.AddTransient<IClaimInfoProvider, ClaimInfoProvider>();
            return services;
        }

        public static IServiceCollection AddCommonAuthentication(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>()
                  .AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

            services.AddHttpContextAccessor();
            return services;
        }

        private static IServiceCollection AddServiceAgents(this IServiceCollection services, IConfiguration configuration)
        {
            AddTahseelAgent(services, configuration);
            AddAlAhliB2BAgent(services, configuration);
            return services;
        }

        private static void AddTahseelAgent(IServiceCollection services, IConfiguration configuration)
        {
            var tahseelApiOptions = configuration.GetSection(TahseelApiOptions.SectionName)
                .Get<TahseelApiOptions>();

            services.AddTahseelServiceAgents(tahseelApiOptions);
        }

        private static void AddAlAhliB2BAgent(IServiceCollection services, IConfiguration configuration)
        {
            var alahliApiOptions = configuration.GetSection(nameof(AlahliApiOptions))
                .Get<AlahliApiOptions>();

            services.AddAlAhliB2BServiceAgents(alahliApiOptions);
        }
    }
}
