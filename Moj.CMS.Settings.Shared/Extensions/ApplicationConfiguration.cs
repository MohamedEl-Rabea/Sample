using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Reflection;

namespace Moj.CMS.Settings.Shared.Extensions
{
    public static class ApplicationConfiguration
    {
        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().Location;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
        public static void Configure(HostBuilderContext hostingContext, IConfigurationBuilder config)
        {
            var env = hostingContext.HostingEnvironment;

            // find the shared folder in the parent folder
            var sharedSettings = Path.Combine(AssemblyDirectory, "sharedappsettings.json");
            var sharedEnvironmentSettings = Path.Combine(AssemblyDirectory, $"sharedappsettings.{env.EnvironmentName}.json");

            //load the SharedSettings first, so that appsettings.json overwrites it
            config
                .AddJsonFile(sharedSettings, optional: true)
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile(sharedEnvironmentSettings, optional: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            config.AddEnvironmentVariables();
        }
    }
}
