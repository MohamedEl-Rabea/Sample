using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moj.CMS.Infrastructure.Contexts;
using Moj.CMS.Shared.Constants.Application;
using Moj.CMS.Shared.Infrastructure.Seed;
using Moj.CMS.UserAccess.Infrastructure.Contexts;
using System;
using System.Threading.Tasks;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace Moj.CMS.Settings.Shared.Helpers
{
    [TransientService]
    public interface IAppInitializer
    {
        void Initialize(IServiceProvider serviceProvider, string environmentName);
    }

    public class AppInitializer : IAppInitializer
    {
        public void Initialize(IServiceProvider serviceProvider, string environmentName)
        {
            Task.Run(async () =>
            {
                using var serviceScope = serviceProvider.CreateScope();

                //Migarte Database
                if (environmentName != AppEnvironments.Testing)
                    await MigrateDatabaseAsync(serviceScope);

                //Seed Data
                await SeedDataAsync(serviceScope);

            }).GetAwaiter().GetResult();
        }

        private static async Task MigrateDatabaseAsync(IServiceScope serviceScope)
        {
            var cmsDbContext = serviceScope.ServiceProvider.GetService<CMSDbContext>();
            await cmsDbContext.Database.MigrateAsync();

            var userAccessDbContext = serviceScope.ServiceProvider.GetService<UserAccessDbContext>();
            await userAccessDbContext.Database.MigrateAsync();
        }

        private static async Task SeedDataAsync(IServiceScope serviceScope)
        {
            var seeders = serviceScope.ServiceProvider.GetServices<IDatabaseSeeder>();

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync();
            }
        }
    }
}
