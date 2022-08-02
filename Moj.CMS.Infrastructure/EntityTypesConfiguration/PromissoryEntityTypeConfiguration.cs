using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moj.CMS.Domain.Aggregates.Promissory;

namespace Moj.CMS.Infrastructure.EntityTypesConfiguration
{
    public class PromissoryEntityTypeConfiguration : EntityTypeConfigurationBase<Promissory>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Promissory> builder)
        {
            builder.ToTable("Promissory");
            builder.Property(c => c.Number).HasColumnName("PromissoryNumber").IsRequired();
            builder.Property(c => c.TypeId).HasColumnName("PromissoryTypeId").IsRequired();

            builder.Property(d => d.Id).UseHiLo("PromissorySequenceHilo");
        }
    }
}
