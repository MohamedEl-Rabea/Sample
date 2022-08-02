using Microsoft.Extensions.Logging;
using Moj.CMS.Infrastructure.Contexts;
using Moj.CMS.Infrastructure.Seed;
using Moj.CMS.Shared.Infrastructure.Seed;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Moj.CMS.Shared.Testing.FakeImplementations
{
    public class TestLookupSeeder : IDatabaseSeeder
    {
        private readonly CMSDbContext _db;
        private readonly ILogger<TestLookupSeeder> _logger;

        public TestLookupSeeder(CMSDbContext db, ILogger<TestLookupSeeder> logger)
        {
            _db = db;
            _logger = logger;
        }
        public async Task SeedAsync()
        {
            try
            {
                await _db.CaseStatuses.AddRangeAsync(CmsLookupsProvider.CaseStatusList.Where(x => !_db.CaseStatuses.Select(z => z.Id).Contains(x.Id)));

                await _db.PromissoryTypes.AddRangeAsync(CmsLookupsProvider.PromissoryTypeList.Where(x => !_db.PromissoryTypes.Select(z => z.Id).Contains(x.Id)));

                await _db.CaseTypes.AddRangeAsync(CmsLookupsProvider.CaseTypeList.Where(x => !_db.CaseTypes.Select(z => z.Id).Contains(x.Id)));

                await _db.CaseOperation.AddRangeAsync(CmsLookupsProvider.CaseOperationList.Where(x => !_db.CaseOperation.Select(z => z.Id).Contains(x.Id)));

                await _db.PartyFinancialTypes.AddRangeAsync(CmsLookupsProvider.PartyFinancialTypeList.Where(x => !_db.PartyFinancialTypes.Select(z => z.Id).Contains(x.Id)));

                await _db.PartyLocations.AddRangeAsync(CmsLookupsProvider.PartyLocationList.Where(x => !_db.PartyLocations.Select(z => z.Id).Contains(x.Id)));

                await _db.PartyIdentityTypes.AddRangeAsync(CmsLookupsProvider.PartyIdentityTypeList.Where(x => !_db.PartyIdentityTypes.Select(z => z.Id).Contains(x.Id)));

                await _db.PartyTypes.AddRangeAsync(CmsLookupsProvider.PartyTypeList.Where(x => !_db.PartyTypes.Select(z => z.Id).Contains(x.Id)));

                await _db.PartyStatuses.AddRangeAsync(CmsLookupsProvider.PartyStatusList.Where(x => !_db.PartyStatuses.Select(z => z.Id).Contains(x.Id)));

                await _db.RequestTerminationReasons.AddRangeAsync(CmsLookupsProvider.RequestTerminationReasonsList.Where(x => !_db.RequestTerminationReasons.Select(z => z.Id).Contains(x.Id)));

                await _db.DebtTypes.AddRangeAsync(CmsLookupsProvider.DebtTypeList.Where(x => !_db.DebtTypes.Select(z => z.Id).Contains(x.Id)));

                await _db.ClaimFinancialStatuses.AddRangeAsync(CmsLookupsProvider.ClaimFinancialStatusList.Where(x => !_db.ClaimFinancialStatuses.Select(z => z.Id).Contains(x.Id)));

                await _db.ClaimStatuses.AddRangeAsync(CmsLookupsProvider.ClaimStatusList.Where(x => !_db.ClaimStatuses.Select(z => z.Id).Contains(x.Id)));

                await _db.PartyRoles.AddRangeAsync(CmsLookupsProvider.PartyRoleList.Where(x => !_db.PartyRoles.Select(z => z.Id).Contains(x.Id)));

                await _db.PartyClassification.AddRangeAsync(CmsLookupsProvider.PartyClassificationList.Where(x => !_db.PartyClassification.Select(z => z.Id).Contains(x.Id)));

                await _db.FinancialEffects.AddRangeAsync(CmsLookupsProvider.FinancialEffectTypeList.Where(x => !_db.FinancialEffects.Select(z => z.Id).Contains(x.Id)));

                await _db.VIbanReferenceTypes.AddRangeAsync(CmsLookupsProvider.VIbanReferenceTypeList.Where(x => !_db.VIbanReferenceTypes.Select(z => z.Id).Contains(x.Id)));

                await _db.IntegrationRequestStatuses.AddRangeAsync(CmsLookupsProvider.IntegrationRequestStatusList.Where(x => !_db.IntegrationRequestStatuses.Select(z => z.Id).Contains(x.Id)));

                await _db.IbanPurposes.AddRangeAsync(CmsLookupsProvider.IbanPurposeList.Where(x => !_db.IntegrationRequestStatuses.Select(z => z.Id).Contains(x.Id)));

                await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($"An error occured while saving seeding data\n {e.Message}");
            }
        }
    }
}
