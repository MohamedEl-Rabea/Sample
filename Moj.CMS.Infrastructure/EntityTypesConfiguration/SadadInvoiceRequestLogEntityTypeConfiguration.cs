using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moj.CMS.Application.Integration.Models;

namespace Moj.CMS.Infrastructure.EntityTypesConfiguration
{
    public class SadadInvoiceRequestLogEntityTypeConfiguration : EntityTypeConfigurationBase<SadadInvoiceRequestLog>
    {
        public override void ConfigureEntity(EntityTypeBuilder<SadadInvoiceRequestLog> builder)
        {
            builder.ToTable("SadadInvoiceRequestLog");
            builder.Property(v => v.Request).HasMaxLength(int.MaxValue);
            builder.Property(v => v.Response).HasMaxLength(int.MaxValue);

            HasSerializedType(builder, log => log.Request);
        }
    }
}
