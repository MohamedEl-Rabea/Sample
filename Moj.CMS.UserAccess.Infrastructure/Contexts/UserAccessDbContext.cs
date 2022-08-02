using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Moj.CMS.Shared.Constants.User;
using Moj.CMS.Shared.Helpers;
using Moj.CMS.Shared.Runtime;
using Moj.CMS.UserAccess.Application.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Moj.CMS.UserAccess.Infrastructure.Contexts
{
    public class UserAccessDbContext : IdentityDbContext<CMSUser, IdentityRole, string>
    {
        private readonly string _userId;

        public UserAccessDbContext(DbContextOptions<UserAccessDbContext> options, IApplicationSession applicationSession) : base(options)
        {
            _userId = applicationSession.UserId ?? UserConstants.SystemUserId;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //TODO: Decimal configuration should be moved to shared base
            DbContextHelpers.ConfigureDecimals(builder);
            //ConfigureStrings(builder);
            DbContextHelpers.ConfigureAuditingProperties(builder);
            base.OnModelCreating(builder);
            ConfigureEntities(builder);
        }

        private static void ConfigureEntities(ModelBuilder builder)
        {
            builder.Entity<CMSUser>(entity =>
            {
                entity.ToTable(name: "Users", "Identity");
                entity.OwnsOne(u => u.ProfilePicture, ownedConfig =>
                {
                    ownedConfig.Property(up => up.DocumentId).HasColumnName("ProfilePictureId");
                    ownedConfig.Property(up => up.DocumentUrl).HasColumnName("ProfilePictureUrl");
                });
            });

            builder.Entity<IdentityRole>(entity => { entity.ToTable(name: "Roles", "Identity"); });
            builder.Entity<IdentityUserRole<string>>(entity => { entity.ToTable("UserRoles", "Identity"); });

            builder.Entity<IdentityUserClaim<string>>(entity => { entity.ToTable("UserClaims", "Identity"); });

            builder.Entity<IdentityUserLogin<string>>(entity => { entity.ToTable("UserLogins", "Identity"); });

            builder.Entity<IdentityRoleClaim<string>>(entity => { entity.ToTable("RoleClaims", "Identity"); });

            builder.Entity<IdentityUserToken<string>>(entity => { entity.ToTable("UserTokens", "Identity"); });
        }

        protected virtual void ApplyAuditingConcepts()
        {
            DbContextHelpers.ApplyAuditingConcepts(ChangeTracker, _userId);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = new CancellationToken())
        {
            ApplyAuditingConcepts();
            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            return result;
        }

        protected static void ConfigureDecimals(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,2)");
            }
        }

        protected static void ConfigureStrings(ModelBuilder modelBuilder)
        {
            var modelTypes = modelBuilder.Model.GetEntityTypes();
            foreach (var property in modelTypes.SelectMany(t => t.GetProperties()).Where(p => p.ClrType == typeof(string)))
            {
                if (property.GetMaxLength() == null)
                    property.SetMaxLength(512);
            }
        }
    }
}