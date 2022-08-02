using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Moj.CMS.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(config => config.AsTransient(), Assembly.GetExecutingAssembly());
        }
    }
}