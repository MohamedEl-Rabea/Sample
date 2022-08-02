using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moj.CMS.Domain.Aggregates.Court;

namespace Moj.CMS.Infrastructure.EntityTypesConfiguration
{
    public class CourtEntityTypeConfiguration : EntityTypeConfigurationBase<Court>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Court> builder)
        {
            builder.Property(c => c.Name).IsRequired();
            builder.Property(c => c.Code).IsRequired();
            builder.Property(c => c.AreaCode).IsRequired();
            
            builder.OwnsMany(c => c.Divisions, dv =>
            {
                dv.ToTable("CourtDivisions");
                dv.WithOwner().HasForeignKey("CourtId");
                dv.HasKey(x => x.Id);
                dv.Property(c => c.Name).IsRequired();
                dv.Property(c => c.Code).IsRequired();
            });

            builder.OwnsMany(c => c.BankAccounts, BA =>
            {
                BA.ToTable("CourtBankAccounts");
                BA.WithOwner().HasForeignKey("CourtId");
                BA.HasKey("Id");
                BA.Property(c => c.AccountNumber).IsRequired();
            });
        }
    }
}
