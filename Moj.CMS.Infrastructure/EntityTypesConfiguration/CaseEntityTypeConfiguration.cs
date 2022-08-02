using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moj.CMS.Domain.Aggregates.Case;

namespace Moj.CMS.Infrastructure.EntityTypesConfiguration
{

    public class CaseEntityTypeConfiguration : EntityTypeConfigurationBase<Case>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Case> builder)
        {
            builder.ToTable("Case");

            builder.Property(d => d.Id).UseHiLo("CaseSequenceHilo");
            builder.Property(d => d.CaseNumber).IsRequired();

            builder.OwnsOne(c => c.DatesInfo, date =>
            {
                //Ef core 5 bug:https://github.com/dotnet/efcore/issues/20740
                date.Property("CaseId").UseHiLo("CaseSequenceHilo");
                date.Property(d => d.ReceiveDate).HasColumnName("ReceiveDate");
                date.Property(d => d.JudgeAcceptanceDate).HasColumnName("JudgeAcceptanceDate");
            });

            builder.OwnsOne(c => c.CaseBasicAmount, amount =>
            {
                amount.Property("CaseId").UseHiLo("CaseSequenceHilo");
                amount.Property(a => a.Value).HasColumnName("CaseBasicAmount");
                amount.Property(a => a.CurrencyIso).HasMaxLength(3).HasColumnName("Currency").IsRequired();
            }).Navigation(x => x.CaseBasicAmount).IsRequired();

            builder.OwnsOne(c => c.ApprovedAmount, amount =>
            {
                amount.Property("CaseId").UseHiLo("CaseSequenceHilo");
                amount.Property(a => a.Value).HasColumnName("ApprovedAmount");
                amount.Property(a => a.CurrencyIso).HasMaxLength(3).HasColumnName("Currency").IsRequired();
            }).Navigation(x => x.ApprovedAmount).IsRequired();

            builder.OwnsOne(c => c.TotalRequiredAmount, amount =>
            {
                amount.Property("CaseId").UseHiLo("CaseSequenceHilo");
                amount.Property(a => a.Value).HasColumnName("TotalRequiredAmount");
                amount.Property(a => a.CurrencyIso).HasMaxLength(3).HasColumnName("Currency").IsRequired();
            }).Navigation(x => x.TotalRequiredAmount).IsRequired();

            builder.OwnsOne(c => c.TotalRemainingAmount, amount =>
            {
                amount.Property("CaseId").UseHiLo("CaseSequenceHilo");
                amount.Property(a => a.Value).HasColumnName("TotalRemainingAmount");
                amount.Property(a => a.CurrencyIso).HasMaxLength(3).HasColumnName("Currency").IsRequired();
            }).Navigation(x => x.TotalRemainingAmount).IsRequired();

            builder.OwnsMany(c => c.CaseParties, cp =>
            {
                cp.ToTable("CaseParty");
                cp.WithOwner().HasForeignKey("CaseId");
                cp.Property<int>("Id");
                cp.HasKey("Id");
                cp.Property(p => p.PartyNumber).IsRequired();
                cp.Property(p => p.PromissoryNumber).IsRequired();
            });

            builder.OwnsMany(c => c.CasePromissories, cp =>
            {
                cp.ToTable("CasePromissory");
                cp.WithOwner().HasForeignKey("CaseId");
                cp.Property(p => p.PromissoryNumber).IsRequired();
                cp.Property<int>("Id");
                cp.HasKey("Id");
            });

            builder.OwnsMany(c => c.CaseDetails, cd =>
            {
                cd.ToTable("CaseDetails");
                cd.WithOwner().HasForeignKey("CaseId");
                cd.Property<int>("Id");
                cd.HasKey("Id");
                cd.Property(a => a.CourtCode).HasMaxLength(15);
                cd.Property(a => a.DivisionCode).HasMaxLength(15);
                cd.Property(a => a.JudgeCode).HasMaxLength(15);
            });

            //TODO:Check RowVersion
        }
    }
}
