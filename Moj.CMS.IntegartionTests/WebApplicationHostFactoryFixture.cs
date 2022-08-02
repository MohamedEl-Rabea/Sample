using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moj.CMS.Api;
using Moj.CMS.Infrastructure.Contexts;
using Moj.CMS.Infrastructure.Seed;
using Moj.CMS.IntegartionTests.Helpers;
using Moj.CMS.Shared.Constants.Application;
using Moj.CMS.Shared.Interfaces;
using Moj.CMS.Shared.Testing;
using Moj.CMS.Shared.Testing.FakeImplementations;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Moj.CMS.IntegartionTests
{
    public class WebApplicationTestHostFactoryFixture<T>
    {
        public IHost Host { get; private set; }

        public HttpClient GetHttpClient()
        {
            return Host.GetTestClient();
        }

        public WebApplicationTestHostFactoryFixture()
        {
            var hostBuilder = Program.CreateHostBuilder(new string[0])
                .UseEnvironment(AppEnvironments.Testing)
                .ConfigureWebHost(webHostBuilder =>
                {
                    webHostBuilder.UseTestServer();
                })
                .ConfigureServices((context, services) =>
                {
                    ConfigureInMemoryDb(services);
                    services.AddScoped<IAuthorizationHandler, TestAuthorizationHandler>();
                    services.AddScoped<IUnitOfwork, TestUnitOfWoek>();
                    UnregisterDuplicateServices(services);
                    InitializeAsync(services).GetAwaiter().GetResult();
                });


            Host = hostBuilder.StartAsync().GetAwaiter().GetResult();
        }

        private static async Task InitializeAsync(IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<CMSDbContext>();
                var logger = scopedServices.GetRequiredService<ILogger<WebApplicationTestHostFactoryFixture<T>>>();
                try
                {
                    await Utilities.InializeDbForTestingAsync(db);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred seeding the " +
                        "database with test data. Error: {Message}", ex.Message);
                }
            }
        }

        private static void ConfigureInMemoryDb(IServiceCollection services)
        {
            var cmsDbDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<CMSDbContext>));
            services.Remove(cmsDbDescriptor);

            services.AddDbContext<CMSDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryCmsDbForTesting_" + typeof(T).Name)
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });
        }


        private static void UnregisterDuplicateServices(IServiceCollection services)
        {
            //TODO: would be more dynamic
            var lookupSeederDiscriptor = services.SingleOrDefault(d => d.ImplementationType == typeof(LookupSeeder));
            services.Remove(lookupSeederDiscriptor);
        }
    }
}
