using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moj.CMS.Application.Integration.Models;

namespace Moj.CMS.Infrastructure.EntityTypesConfiguration
{
    public class VIbanRequestLogEntityTypeConfiguration : EntityTypeConfigurationBase<VIbanRequestLog>
    {
        public override void ConfigureEntity(EntityTypeBuilder<VIbanRequestLog> builder)
        {
            builder.ToTable("VIbanRequestLog");
            builder.Property(v => v.Request).HasMaxLength(int.MaxValue);
            builder.Property(v => v.Response).HasMaxLength(int.MaxValue);

            builder.OwnsOne(v => v.ReferenceDetails, reference =>
            {
                reference.Property(r => r.ReferenceNumber).IsRequired().HasColumnName("ReferenceNumber");
                reference.Property(r => r.ReferenceType).HasColumnName("ReferenceType");
            }).Navigation(v => v.ReferenceDetails).IsRequired();

            HasSerializedType(builder, log => log.Request);
        }
    }
}
