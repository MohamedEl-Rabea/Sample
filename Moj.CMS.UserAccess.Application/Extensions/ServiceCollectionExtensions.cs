using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;
 
namespace Moj.CMS.UserAccess.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddUserAccessApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(config => config.AsTransient(), Assembly.GetExecutingAssembly());
        }
    }
}