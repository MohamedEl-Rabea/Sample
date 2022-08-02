using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moj.CMS.Domain.Aggregates.Party;

namespace Moj.CMS.Infrastructure.EntityTypesConfiguration
{
    public class PartyEntityTypeConfiguration : IEntityTypeConfiguration<Party>
    {
        public void Configure(EntityTypeBuilder<Party> builder)
        {
            builder.ToTable("Party");
            builder.Property(d => d.Id).UseHiLo("PartyHilo");
            builder.Property(p => p.FullName).IsRequired().HasMaxLength(60);
            builder.Property(p => p.NationalityCode).IsRequired().HasMaxLength(3);
            builder.Property(p => p.PartyNumber).IsRequired().HasMaxLength(15);
            builder.OwnsMany(c => c.PartyIdentities, cp =>
            {
                cp.ToTable("PartyIdentity");
                cp.WithOwner().HasForeignKey("PartyId");
                cp.Property<int>("Id");
                cp.HasKey("Id");
                cp.Property(p => p.PartyIdentityNumber).IsRequired().HasMaxLength(15);

            });

            builder.OwnsOne(c => c.TotalDebtAmount, amount =>
            {
                amount.Property("PartyId").UseHiLo("PartyHilo");
                amount.Property(a => a.Value).HasColumnName("TotalDebtAmount");
                amount.Property(a => a.CurrencyIso).HasMaxLength(3).HasColumnName("Currency").IsRequired();
            }).Navigation(x => x.TotalDebtAmount).IsRequired();

            builder.OwnsOne(c => c.TotalCreditAmount, amount =>
            {
                amount.Property("PartyId").UseHiLo("PartyHilo");
                amount.Property(a => a.Value).HasColumnName("TotalCreditAmount");
                amount.Property(a => a.CurrencyIso).HasMaxLength(3).HasColumnName("Currency").IsRequired();
            }).Navigation(x => x.TotalCreditAmount).IsRequired();
        }
    }

}
