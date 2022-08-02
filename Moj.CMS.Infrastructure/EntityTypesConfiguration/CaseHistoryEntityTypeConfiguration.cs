using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moj.CMS.Domain.Aggregates.CaseHistory;

namespace Moj.CMS.Infrastructure.EntityTypesConfiguration
{
    public class CaseHistoryEntityTypeConfiguration : EntityTypeConfigurationBase<CaseHistory>
    {
        public override void ConfigureEntity(EntityTypeBuilder<CaseHistory> builder)
        {
            builder.ToTable("CaseHistory");
            builder.Property(ch => ch.CaseNumber).IsRequired();
            builder.Property(ch => ch.Operation).HasConversion<int>();
            HasLocalizedText(builder, ch => ch.Details);

            //TODO:Check RowVersion
        }
    }
}
