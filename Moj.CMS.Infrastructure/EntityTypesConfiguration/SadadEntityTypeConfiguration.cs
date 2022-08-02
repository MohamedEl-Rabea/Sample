using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moj.CMS.Domain.Aggregates.Case.Entities;

namespace Moj.CMS.Infrastructure.EntityTypesConfiguration
{
    public class SadadEntityTypeConfiguration : IEntityTypeConfiguration<CaseSadad>
    {
        public void Configure(EntityTypeBuilder<CaseSadad> builder)
        {
            builder.ToTable("Sadad");
            builder.HasKey(v => v.Id);
            builder.Property(p => p.IssueDate).HasColumnType("date");
            builder.Property(v => v.SadadNumber).IsRequired();
            builder.Property<int>("CaseId").IsRequired();
            //TODO:Check RowVersion
        }
    }
}
