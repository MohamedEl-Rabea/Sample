using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moj.CMS.Domain.Aggregates.Iban;

namespace Moj.CMS.Infrastructure.EntityTypesConfiguration
{
    public class IbanEntityTypeConfiguration : EntityTypeConfigurationBase<Iban>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Iban> builder)
        {
            builder.ToTable("Ibans");

            builder.Property(v => v.Number).IsRequired();
            builder.Property(v => v.Bank).IsRequired();
            builder.Property(v => v.Branch).IsRequired();
            builder.Property(v=>v.VIbanQuantity).IsRequired();
            builder.Property(v=>v.VIbanRemaining).IsRequired();
            builder.OwnsOne(i => i.IbanReferenceDetails, reference =>
            {
                reference.Property(r=>r.ReferenceNumber).IsRequired().HasColumnName("ReferenceNumber");
                reference.Property(r => r.ReferenceType).HasColumnName("ReferenceType");
            }).Navigation(v=>v.IbanReferenceDetails).IsRequired();
        }
    }
}