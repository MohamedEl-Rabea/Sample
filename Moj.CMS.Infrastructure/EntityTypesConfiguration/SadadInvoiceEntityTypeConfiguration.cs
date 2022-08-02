using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moj.CMS.Domain.Aggregates.SadadInvoice;

namespace Moj.CMS.Infrastructure.EntityTypesConfiguration
{
    public class SadadInvoiceEntityTypeConfiguration : EntityTypeConfigurationBase<SadadInvoice>
    {
        public override void ConfigureEntity(EntityTypeBuilder<SadadInvoice> builder)
        {

            builder.ToTable("SadadInvoices");
            builder.HasKey(v => v.Id);
            builder.Property(d => d.Id).UseHiLo("SadadInvoiceSequenceHilo");
            builder.Property(p => p.Number).IsRequired(false);
            builder.Property(p => p.PartyNumber).IsRequired();
            builder.Property(p => p.ClaimNumber).IsRequired();

            builder.OwnsOne(p => p.Amount, amount =>
            {
                amount.Property("SadadInvoiceId").UseHiLo("SadadInvoiceSequenceHilo");
                amount.Property(a => a.Value).HasColumnName("Amount");
                amount.Property(a => a.CurrencyIso).HasMaxLength(3).HasColumnName("Currency").IsRequired();
            }).Navigation(x => x.Amount).IsRequired();

            builder.OwnsOne(p => p.MinBillableAmount, amount =>
            {
                amount.Property("SadadInvoiceId").UseHiLo("SadadInvoiceSequenceHilo");
                amount.Property(a => a.Value).HasColumnName("MinBillableAmount");
                amount.Property(a => a.CurrencyIso).HasMaxLength(3).HasColumnName("Currency").IsRequired();
            }).Navigation(x => x.MinBillableAmount).IsRequired();

            builder.OwnsMany(s => s.SadadPaymentNotification, pn =>
            {
                pn.ToTable("SadadPaymentNotifications");
                pn.WithOwner().HasForeignKey("SadadId");
                pn.Property<int>("Id");
                pn.HasKey("Id");
            });

            builder.OwnsMany(s => s.SadadCashNotifications, cn =>
            {
                cn.ToTable("SadadCashNotifications");
                cn.WithOwner().HasForeignKey("SadadId");
                cn.Property<int>("Id");
                cn.HasKey("Id");
            });
        }
    }
}
