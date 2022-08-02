using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moj.CMS.Domain.Shared.Audit;
using Moj.CMS.Shared.Constants.User;
using Moj.CMS.Shared.CustomAttributes;
using Moj.CMS.Shared.Enums;
using Moj.CMS.Shared.Helpers;
using Moj.CMS.Shared.Models;
using Moj.CMS.Shared.Models.Audit;
using Moj.CMS.Shared.Runtime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Moj.CMS.Shared.Infrastructure.Contexts
{
    public abstract class AuditedDbContext : DbContext
    {
        private readonly string _userId;
        private readonly IApplicationSession _session;

        protected AuditedDbContext(DbContextOptions options, IApplicationSession session) : base(options)
        {
            _userId = session.UserId ?? UserConstants.SystemUserId;
            _session = session;
        }

        public DbSet<Audit> AuditTrails { get; set; }
        public DbSet<Log> Logs { get; set; }

        public virtual async Task<int> SaveChangesAsync()
        {
            ApplyAuditingConcepts();
            var auditEntries = BuildEntitiesHistory();
            var result = await base.SaveChangesAsync();
            await AssignKeysForAddedEntities(auditEntries);
            return result;
        }

        protected virtual void ApplyAuditingConcepts()
        {
            DbContextHelpers.ApplyAuditingConcepts(ChangeTracker, _userId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.LogTo(message => Debug.WriteLine(message));

        private List<AuditEntry> BuildEntitiesHistory()
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            foreach (var entry in ChangeTracker.Entries())
            {
                var isIgnored = entry.Entity.GetType().IsDefined(typeof(IgnoreAuditingAttribute), false);
                if (isIgnored || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;

                var auditEntry = new AuditEntry(entry);
                auditEntry.TableName = entry.Entity.GetType().Name;
                auditEntry.UserId = _userId;
                auditEntry.RequestId = _session.RequestId;
                auditEntry.RequestName = _session.RequestName;
                auditEntries.Add(auditEntry);
                foreach (var property in entry.Properties)
                {
                    if (property.IsTemporary)
                    {
                        auditEntry.TemporaryProperties.Add(property);
                        continue;
                    }

                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValue = property.CurrentValue.ToString();
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.AuditType = AuditType.Create;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;

                        case EntityState.Deleted:
                            auditEntry.AuditType = AuditType.Delete;
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;

                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.ChangedColumns.Add(propertyName);
                                auditEntry.AuditType = AuditType.Update;
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }

                            break;
                    }
                }
            }

            foreach (var auditEntry in auditEntries.Where(_ => !_.HasTemporaryProperties))
            {
                AuditTrails.Add(auditEntry.ToAudit());
            }

            return auditEntries.Where(_ => _.HasTemporaryProperties).ToList();
        }

        private Task AssignKeysForAddedEntities(List<AuditEntry> auditEntries)
        {
            if (auditEntries == null || auditEntries.Count == 0)
                return Task.CompletedTask;

            foreach (var auditEntry in auditEntries)
            {
                foreach (var prop in auditEntry.TemporaryProperties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValue = prop.CurrentValue.ToString();
                    }
                    else
                    {
                        auditEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                }

                AuditTrails.Add(auditEntry.ToAudit());
            }

            return SaveChangesAsync();
        }

        protected static void ConfigureStrings(ModelBuilder modelBuilder)
        {
            var modelTypes = modelBuilder.Model.GetEntityTypes();
            var auditingProps = typeof(FullAuditedEntity).GetProperties().Select(p => p.Name);
            foreach (var property in modelTypes.SelectMany(t => t.GetProperties()).Where(p => p.ClrType == typeof(string)))
            {
                var entityTypeName = property.DeclaringEntityType.ClrType.Name;
                var audtingProperty = auditingProps.Contains(property.Name);
                if (!audtingProperty && (entityTypeName == nameof(Audit) || entityTypeName == nameof(Log)))
                    continue;

                if (property.GetMaxLength() == null)
                    property.SetMaxLength(256);
            }
        }
    }
}