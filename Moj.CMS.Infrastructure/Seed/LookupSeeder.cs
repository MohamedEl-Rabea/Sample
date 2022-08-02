using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moj.CMS.Domain.Shared.LookupModels;
using Moj.CMS.Infrastructure.Contexts;
using Moj.CMS.Infrastructure.Extensions;
using Moj.CMS.Shared.Infrastructure.Seed;
using System.Linq;
using System.Threading.Tasks;

namespace Moj.CMS.Infrastructure.Seed
{
    public class LookupSeeder : IDatabaseSeeder
    {
        private readonly CMSDbContext db;
        private readonly ILogger<LookupSeeder> logger;

        public LookupSeeder(CMSDbContext db, ILogger<LookupSeeder> logger)
        {
            this.db = db;
            this.logger = logger;
        }

        public async Task SeedAsync()
        {

            try
            {
                await db.CaseStatuses.AddRangeAsync(CmsLookupsProvider.CaseStatusList.Where(x => !db.CaseStatuses.Select(z => z.Id).Contains(x.Id)));
                await db.SaveChangesWithIdentityInsertAsync<CaseStatus>();

                await db.PromissoryTypes.AddRangeAsync(CmsLookupsProvider.PromissoryTypeList.Where(x => !db.PromissoryTypes.Select(z => z.Id).Contains(x.Id)));
                await db.SaveChangesWithIdentityInsertAsync<PromissoryType>();

                await db.CaseTypes.AddRangeAsync(CmsLookupsProvider.CaseTypeList.Where(x => !db.CaseTypes.Select(z => z.Id).Contains(x.Id)));
                await db.SaveChangesWithIdentityInsertAsync<CaseType>();

                await db.CaseOperation.AddRangeAsync(CmsLookupsProvider.CaseOperationList.Where(x => !db.CaseOperation.Select(z => z.Id).Contains(x.Id)));
                await db.SaveChangesWithIdentityInsertAsync<CaseOperation>();

                await db.PartyFinancialTypes.AddRangeAsync(CmsLookupsProvider.PartyFinancialTypeList.Where(x => !db.PartyFinancialTypes.Select(z => z.Id).Contains(x.Id)));
                await db.SaveChangesWithIdentityInsertAsync<PartyFinancialType>();

                await db.PartyLocations.AddRangeAsync(CmsLookupsProvider.PartyLocationList.Where(x => !db.PartyLocations.Select(z => z.Id).Contains(x.Id)));
                await db.SaveChangesWithIdentityInsertAsync<PartyLocation>();

                await db.PartyIdentityTypes.AddRangeAsync(CmsLookupsProvider.PartyIdentityTypeList.Where(x => !db.PartyIdentityTypes.Select(z => z.Id).Contains(x.Id)));
                await db.SaveChangesWithIdentityInsertAsync<PartyIdentityType>();

                await db.PartyTypes.AddRangeAsync(CmsLookupsProvider.PartyTypeList.Where(x => !db.PartyTypes.Select(z => z.Id).Contains(x.Id)));
                await db.SaveChangesWithIdentityInsertAsync<PartyType>();

                await db.PartyStatuses.AddRangeAsync(CmsLookupsProvider.PartyStatusList.Where(x => !db.PartyStatuses.Select(z => z.Id).Contains(x.Id)));
                await db.SaveChangesWithIdentityInsertAsync<PartyStatus>();

                await db.RequestTerminationReasons.AddRangeAsync(CmsLookupsProvider.RequestTerminationReasonsList.Where(x => !db.RequestTerminationReasons.Select(z => z.Id).Contains(x.Id)));
                await db.SaveChangesWithIdentityInsertAsync<RequestTerminationReasons>();

                await db.DebtTypes.AddRangeAsync(CmsLookupsProvider.DebtTypeList.Where(x => !db.DebtTypes.Select(z => z.Id).Contains(x.Id)));
                await db.SaveChangesWithIdentityInsertAsync<DebtType>();

                await db.ClaimFinancialStatuses.AddRangeAsync(CmsLookupsProvider.ClaimFinancialStatusList.Where(x => !db.ClaimFinancialStatuses.Select(z => z.Id).Contains(x.Id)));
                await db.SaveChangesWithIdentityInsertAsync<ClaimFinancialStatus>();

                await db.ClaimStatuses.AddRangeAsync(CmsLookupsProvider.ClaimStatusList.Where(x => !db.ClaimStatuses.Select(z => z.Id).Contains(x.Id)));
                await db.SaveChangesWithIdentityInsertAsync<ClaimStatus>();

                await db.PartyRoles.AddRangeAsync(CmsLookupsProvider.PartyRoleList.Where(x => !db.PartyRoles.Select(z => z.Id).Contains(x.Id)));
                await db.SaveChangesWithIdentityInsertAsync<PartyRole>();

                await db.PartyClassification.AddRangeAsync(CmsLookupsProvider.PartyClassificationList.Where(x =>
                    !db.PartyClassification.Select(z => z.Id).Contains(x.Id)));
                await db.SaveChangesWithIdentityInsertAsync<PartyClassification>();


                await db.FinancialEffects.AddRangeAsync(CmsLookupsProvider.FinancialEffectTypeList.Where(x => !db.FinancialEffects.Select(z => z.Id).Contains(x.Id)));
                await db.SaveChangesWithIdentityInsertAsync<FinancialEffectType>();

                await db.ClaimTerminationReasons.AddRangeAsync(CmsLookupsProvider.ClaimTerminationReasonEnum.Where(x => !db.ClaimTerminationReasons.Select(z => z.Id).Contains(x.Id)));
                await db.SaveChangesWithIdentityInsertAsync<ClaimTerminationReason>();

                await db.VIbanReferenceTypes.AddRangeAsync(CmsLookupsProvider.VIbanReferenceTypeList.Where(x => !db.VIbanReferenceTypes.Select(z => z.Id).Contains(x.Id)));
                await db.SaveChangesWithIdentityInsertAsync<VIbanReferenceType>();

                await db.IntegrationRequestStatuses.AddRangeAsync(CmsLookupsProvider.IntegrationRequestStatusList.Where(x => !db.IntegrationRequestStatuses.Select(z => z.Id).Contains(x.Id)));
                await db.SaveChangesWithIdentityInsertAsync<IntegrationRequestStatus>();

                await db.IbanPurposes.AddRangeAsync(CmsLookupsProvider.IbanPurposeList.Where(x => !db.IbanPurposes.Select(i => i.Id).Contains(x.Id)));
                await db.SaveChangesWithIdentityInsertAsync<IbanPurpose>();

                logger.LogInformation("Seeding Finished.");
            }
            catch (System.Exception e)
            {
                logger.LogError($"An error occured while saving seeding data\n {e.Message}");
            }
        }
    }
}
