using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moj.CMS.Domain.Aggregates.Claim.ValueObjects;

namespace Moj.CMS.Infrastructure.EntityTypesConfiguration
{
    public class ClaimDetailsEntityTypeConfiguration : EntityTypeConfigurationBase<ClaimDetails>
    {
        public override void ConfigureEntity(EntityTypeBuilder<ClaimDetails> builder)
        {

            builder.ToTable("ClaimDetails");
            builder.Property<int>("Id");
            builder.HasKey("Id");
            builder.Property(c => c.PartyNumber).IsRequired();

            builder.Property<int>("ClaimId").IsRequired();

            builder.OwnsOne(c => c.RequiredAmount, amount =>
            {
                amount.Property(a => a.Value).HasColumnName("RequiredAmount");
                amount.Property(a => a.CurrencyIso).HasMaxLength(3).HasColumnName("Currency").IsRequired();
            }).Navigation(x => x.RequiredAmount).IsRequired();

            builder.OwnsOne(c => c.BillingAmount, amount =>
            {
                amount.Property(a => a.Value).HasColumnName("BillingAmount");
                amount.Property(a => a.CurrencyIso).HasMaxLength(3).HasColumnName("Currency").IsRequired();
            }).Navigation(x => x.BillingAmount).IsRequired();

            builder.OwnsMany(detailsHistory => detailsHistory.ClaimDetailsHistoryList, history =>
            {
                history.ToTable("ClaimDetailsHistory");
                history.WithOwner().HasForeignKey("ClaimDetailsId");
                history.Property<int>("Id");
                history.HasKey("Id");
                history.Property(c => c.PartyNumber).IsRequired();

                history.OwnsOne(c => c.NewRequiredAmount, amount =>
                {
                    amount.Property(a => a.Value).HasColumnName("NewRequiredAmount");
                    amount.Property(a => a.CurrencyIso).HasMaxLength(3).HasColumnName("Currency").IsRequired();
                }).Navigation(x => x.NewRequiredAmount).IsRequired();

                history.OwnsOne(c => c.OldRequiredAmount, amount =>
                {
                    amount.Property(a => a.Value).HasColumnName("OldRequiredAmount");
                    amount.Property(a => a.CurrencyIso).HasMaxLength(3).HasColumnName("Currency").IsRequired();
                }).Navigation(x => x.OldRequiredAmount).IsRequired();

                history.OwnsOne(c => c.NewBillingAmount, amount =>
                {
                    amount.Property(a => a.Value).HasColumnName("NewBillingAmount");
                    amount.Property(a => a.CurrencyIso).HasMaxLength(3).HasColumnName("Currency").IsRequired();
                }).Navigation(x => x.NewBillingAmount).IsRequired();

                history.OwnsOne(c => c.OldBillingAmount, amount =>
                {
                    amount.Property(a => a.Value).HasColumnName("OldBillingAmount");
                    amount.Property(a => a.CurrencyIso).HasMaxLength(3).HasColumnName("Currency").IsRequired();
                }).Navigation(x => x.OldBillingAmount).IsRequired();
            });
        }

    }
}
