using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Moj.CMS.Shared.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSharedApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
