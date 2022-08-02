using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Moj.CMS.Domain.Shared.Audit;
using Moj.CMS.Domain.Shared.Entities;
using Moj.CMS.Domain.Shared.Extensions;
using Moj.CMS.Domain.Shared.Values;
using System;
using System.Linq;

namespace Moj.CMS.Shared.Helpers
{
    public static class DbContextHelpers
    {
        public static string UserId { get; set; }

        public static void ApplyAuditingConcepts(ChangeTracker changeTracker, string userId)
        {
            UserId = userId;
            var changeEntries = changeTracker.Entries().ToList();
            foreach (var entry in changeEntries)
            {
                MarkParentWithModifiedCollectionAsModified(entry);
                switch (entry.State)
                {
                    case EntityState.Added:
                        ApplyApplicationConceptsForAddedEntity(entry);
                        break;
                    case EntityState.Modified:
                        ApplyApplicationConceptsForModifiedEntity(entry);
                        break;
                    case EntityState.Deleted:
                        ApplyApplicationConceptsForDeletedEntity(entry);
                        break;
                }
            }
        }

        private static void MarkParentWithModifiedCollectionAsModified(EntityEntry entry)
        {
            entry.State = entry.State == EntityState.Unchanged && entry.Members.Any(m => m.IsModified)
                ? EntityState.Modified
                : entry.State;
        }

        private static void ApplyApplicationConceptsForAddedEntity(EntityEntry entry)
        {
            CheckAndSetId(entry);
            SetCreationAuditProperties(entry.Entity);
        }

        private static void ApplyApplicationConceptsForModifiedEntity(EntityEntry entry)
        {
            SetModificationAuditProperties(entry.Entity);

            if (entry.Entity is ISoftDelete && entry.Entity.As<ISoftDelete>().IsDeleted)
            {
                SetDeletionAuditProperties(entry.Entity);
            }
        }

        private static void ApplyApplicationConceptsForDeletedEntity(EntityEntry entry)
        {
            CancelDeletionForSoftDelete(entry);
            SetDeletionAuditProperties(entry.Entity);
        }

        private static void CheckAndSetId(EntityEntry entry)
        {
            //Set GUID Ids
            var entity = entry.Entity as IEntity<Guid>;
            if (entity != null && entity.Id == Guid.Empty)
            {
                var idPropertyEntry = entry.Property("Id");

                if (idPropertyEntry != null && idPropertyEntry.Metadata.ValueGenerated == ValueGenerated.Never)
                {
                    entity.Id = Guid.NewGuid();
                }
            }
        }

        private static void SetCreationAuditProperties(object entityAsObj)
        {
            EntityAuditingHelper.SetCreationAuditProperties(entityAsObj, UserId);
        }

        private static void SetModificationAuditProperties(object entityAsObj)
        {
            EntityAuditingHelper.SetModificationAuditProperties(entityAsObj, UserId);
        }

        private static void SetDeletionAuditProperties(object entityAsObj)
        {
            if (entityAsObj is IHasDeletionTime)
            {
                var entity = entityAsObj.As<IHasDeletionTime>();
                if (entity.DeletionTime == null)
                    entity.DeletionTime = CLock.Now;
            }

            if (entityAsObj is IDeletionAudited)
            {
                var entity = entityAsObj.As<IDeletionAudited>();

                if (entity.DeleterUserId != null)
                    return;

                if (UserId == null)
                {
                    entity.DeleterUserId = null;
                    return;
                }

                entity.DeleterUserId = UserId;
            }
        }

        private static void CancelDeletionForSoftDelete(EntityEntry entry)
        {
            if (!(entry.Entity is ISoftDelete))
            {
                return;
            }

            entry.Reload();
            entry.State = EntityState.Modified;
            entry.Entity.As<ISoftDelete>().IsDeleted = true;
        }

        public static void ConfigureAuditingProperties(ModelBuilder modelBuilder)
        {
            var modelTypes = modelBuilder.Model.GetEntityTypes();
            var auditingProps = typeof(ICreationAudited).GetProperties().Select(p => p.Name);
            foreach (var property in modelTypes.SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(string) && auditingProps.Contains(p.Name)))
            {
                property.IsNullable = false;
            }
        }

        public static void ConfigureDecimals(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,2)");
            }
        }
    }
}
