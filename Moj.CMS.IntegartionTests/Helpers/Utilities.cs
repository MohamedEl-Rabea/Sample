using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moj.CMS.Application.AppServices.Court;
using Moj.CMS.Application.Lookups.Division;
using Moj.CMS.Domain.Aggregates.Court;
using Moj.CMS.Domain.Aggregates.Court.Entities;
using Moj.CMS.Domain.Shared.LookupModels;
using Moj.CMS.Infrastructure.Contexts;
using Moj.CMS.Settings.Shared.Helpers;
using Moj.CMS.Shared.Constants.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moj.CMS.IntegartionTests.Helpers
{
    public static class Utilities
    {
        public static async Task InializeDbForTestingAsync(CMSDbContext db)
        {
            await SeedNonSystemLookups(db);
            await SeedCourtsAsync(db);
            await db.SaveChangesAsync();
        }

        private static async Task SeedNonSystemLookups(CMSDbContext db)
        {
            await db.Areas.AddRangeAsync(SeedingAreas);
            await db.Judges.AddRangeAsync(SeedingJudges);
            await db.Nationality.AddRangeAsync(SeedingNationalities);
        }

        private static async Task SeedCourtsAsync(CMSDbContext db)
        {
            var courts = SeedingCourts.Select(c =>
            {
                var divisions = SeedingDivisions.Where(d => d.CourtCode == c.Code).Select(d => Division.Create(d.Name, d.Code, d.IsActive ?? false));
                return Court.Create(new Domain.ParameterObjects.Court.CourtInfoParam
                {
                    Code = c.Code,
                    Name = c.Name,
                    AreaCode = c.AreaCode,
                    IsActive = c.IsActive,
                    Divisions = divisions,
                });
            });
            await db.Courts.AddRangeAsync(courts);
        }

        public static async Task ReInializeDbForTestingAsync(IServiceProvider serviceProvider)
        {
            var db = serviceProvider.GetRequiredService<CMSDbContext>();
            db.ChangeTracker
                .Entries()
                .ToList()
                .ForEach(e => e.State = EntityState.Detached);

            await db.Database.EnsureDeletedAsync();
            await db.Database.EnsureCreatedAsync();

            var appInitializer = serviceProvider.GetRequiredService<IAppInitializer>();
            appInitializer.Initialize(serviceProvider, AppEnvironments.Testing);
            await InializeDbForTestingAsync(db);
        }

        public static IEnumerable<Area> SeedingAreas => new List<Area>
        {
            new Area { Name = "المنطقة الاولى", Code = "1", IsActive = true }
        };

        public static IEnumerable<CourtDto> SeedingCourts => new List<CourtDto>
        {
            new CourtDto
            {
                Name = "المحكمة الاولى", Code = "1", IsActive = true, AreaCode = "1", BankAccounts = new string[] { "123456" }
            },
             new CourtDto
            {
                Name = "المحكمة الثانية", Code = "2", IsActive = true, AreaCode = "1", BankAccounts = new string[] {"123457" }
            }
        };

        public static IEnumerable<DivisionDto> SeedingDivisions => new List<DivisionDto>
        {
            new DivisionDto { Name = "الدائرة الاولى", Code = "1", IsActive = true, CourtCode = "1" },
            new DivisionDto { Name = "الدائرة الثانية", Code = "2", IsActive = true, CourtCode = "1" },
            new DivisionDto { Name = "الدائرة الثالثة", Code = "3", IsActive = true, CourtCode = "2" },
            new DivisionDto { Name = "الدائرة الرابعة", Code = "4", IsActive = true, CourtCode = "2" }
        };

        public static IEnumerable<Judge> SeedingJudges => new List<Judge>
        {
            new Judge { Name = "القاضى الاول", Code = "1", IsActive = true },
            new Judge { Name = "القاضى الثانى", Code = "2", IsActive = true },
            new Judge { Name = "القاضى الثالث", Code = "3", IsActive = true }
        };

        public static IEnumerable<Nationality> SeedingNationalities => new List<Nationality>
        {
            new Nationality { Name = "الممكلة العربية السعودية", Code = "SA" },
            new Nationality { Name = "الممكلة الأردنية الهاشمية", Code = "JO" },
            new Nationality { Name = "جمهورية مصر العربية", Code = "EG" },
            new Nationality { Name = "دولة الكويت", Code = "KU" }
        };
    }
}