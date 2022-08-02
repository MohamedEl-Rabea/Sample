using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moj.CMS.Domain.Aggregates.VIban;

namespace Moj.CMS.Infrastructure.EntityTypesConfiguration
{
    public class VIbanEntityTypeConfiguration : EntityTypeConfigurationBase<VIban>
    {
        public override void ConfigureEntity(EntityTypeBuilder<VIban> builder)
        {
            builder.ToTable("VIbans");

            builder.Property(v => v.Number).IsRequired();
            builder.Property(v => v.Alias).IsRequired();

            builder.OwnsOne(v => v.ReferenceDetails, reference =>
            {
                reference.Property(r => r.ReferenceNumber).IsRequired().HasColumnName("ReferenceNumber");
                reference.Property(r => r.ReferenceType).HasColumnName("ReferenceType");
            }).Navigation(v => v.ReferenceDetails).IsRequired();
        }
    }
}
