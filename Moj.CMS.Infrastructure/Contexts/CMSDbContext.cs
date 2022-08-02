using Microsoft.EntityFrameworkCore;
using Moj.CMS.Application.Integration.Models;
using Moj.CMS.Application.Models;
using Moj.CMS.Domain.Aggregates.Case;
using Moj.CMS.Domain.Aggregates.CaseHistory;
using Moj.CMS.Domain.Aggregates.Claim;
using Moj.CMS.Domain.Aggregates.Court;
using Moj.CMS.Domain.Aggregates.Iban;
using Moj.CMS.Domain.Aggregates.Party;
using Moj.CMS.Domain.Aggregates.Promissory;
using Moj.CMS.Domain.Aggregates.SadadInvoice;
using Moj.CMS.Domain.Aggregates.VIban;
using Moj.CMS.Domain.Shared.LookupModels;
using Moj.CMS.Shared.Extensions;
using Moj.CMS.Shared.Helpers;
using Moj.CMS.Shared.Infrastructure.Contexts;
using Moj.CMS.Shared.Models.Currency;
using Moj.CMS.Shared.Notifications.Dispatchers.Email;
using Moj.CMS.Shared.Notifications.Dispatchers.SMS;
using Moj.CMS.Shared.Runtime;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.Infrastructure.Contexts
{
    public class CMSDbContext : AuditedDbContext
    {
        public CMSDbContext(DbContextOptions<CMSDbContext> options, IApplicationSession applicationSession)
            : base(options, applicationSession)
        {
        }

        public DbSet<Case> Cases { get; set; }
        public DbSet<Party> Parties { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<CaseHistory> CaseHistory { get; set; }
        public DbSet<Court> Courts { get; set; }
        public DbSet<VIban> VIbans { get; set; }
        public DbSet<Iban> Ibans { get; set; }
        public DbSet<SadadInvoice> SadadInvoices { get; set; }

        #region LookUps tables 

        public DbSet<IbanPurpose> IbanPurposes { get; set; }
        public DbSet<VIbanReferenceType> VIbanReferenceTypes { get; set; }
        public DbSet<IntegrationRequestStatus> IntegrationRequestStatuses { get; set; }
        public DbSet<CaseStatus> CaseStatuses { get; set; }
        public DbSet<PromissoryType> PromissoryTypes { get; set; }
        public DbSet<Promissory> Promissories { get; set; }
        public DbSet<CaseType> CaseTypes { get; set; }
        public DbSet<CaseOperation> CaseOperation { get; set; }
        public DbSet<PartyFinancialType> PartyFinancialTypes { get; set; }
        public DbSet<PartyLocation> PartyLocations { get; set; }
        public DbSet<PartyStatus> PartyStatuses { get; set; }
        public DbSet<PartyIdentityType> PartyIdentityTypes { get; set; }
        public DbSet<PartyType> PartyTypes { get; set; }
        public DbSet<Judge> Judges { get; set; }
        public DbSet<Nationality> Nationality { get; set; }
        public DbSet<Currency> Currency { get; set; }
        public DbSet<RequestTerminationReasons> RequestTerminationReasons { get; set; }
        public DbSet<PartyRole> PartyRoles { get; set; }
        public DbSet<PartyClassification> PartyClassification { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<DebtType> DebtTypes { get; set; }
        public DbSet<ClaimFinancialStatus> ClaimFinancialStatuses { get; set; }
        public DbSet<ClaimStatus> ClaimStatuses { get; set; }
        public DbSet<FinancialEffectType> FinancialEffects { get; set; }
        public DbSet<ClaimTerminationReason> ClaimTerminationReasons { get; set; }

        #endregion

        #region Setting

        public DbSet<EmailSettings> EmailSettings { get; set; }
        public DbSet<SMSSettings> SMSSettings { get; set; }

        #endregion

        #region Integration
        public DbSet<ClientIntegrationSettings> ClientIntegrationSettings { get; set; }
        public DbSet<VIbanRequestLog> VIbanRequestLogs { get; set; }
        public DbSet<SadadInvoiceRequestLog> SadadInvoiceRequestLogs { get; set; }
        #endregion

        public DbSet<User> Users { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return await base.SaveChangesAsync();
        }

        public override int SaveChanges()
        {
            return SaveChangesAsync().GetAwaiter().GetResult();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("CMS");
            ConfigureStrings(modelBuilder);
            DbContextHelpers.ConfigureDecimals(modelBuilder);
            DbContextHelpers.ConfigureAuditingProperties(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            modelBuilder.ApplyGlobalFilters();
            base.OnModelCreating(modelBuilder);
        }
    }
}