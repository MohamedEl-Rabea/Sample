using Microsoft.Extensions.DependencyInjection;
using Moj.CMS.Integration.Application.Services;
using Moj.CMS.Integration.Contracts.Runtime;
using System.Reflection;

namespace Moj.CMS.Application.Integration.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddIntegrationApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient<IIntegrationExecutionContextAccessor, IntegrationExecutionContextAccessor>();
            services.AddTransient<IIntegrationAuthorizationProvider, IntegrationAuthorizationProvider>();
        }
    }
}