using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Moj.CMS.Settings.Shared.Extensions;
using Moj.CMS.Shared.Logging;
using Serilog;

namespace Moj.CMS.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(ApplicationConfiguration.Configure)
                .UseSerilog(SeriLogger.Configure)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
