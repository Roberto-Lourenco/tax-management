using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaxManagement.Domain.Entities;
using TaxManagement.Domain.ValueObjects;

namespace TaxManagement.Infrastructure.Database.Configurations;

internal sealed class TaxEntryConfiguration : IEntityTypeConfiguration<TaxEntry>
{
    public void Configure(EntityTypeBuilder<TaxEntry> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.OrderDate)
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.Property(t => t.Status)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(t => t.CompetenceDate)
            .HasConversion(
                   v => v.Value,
                   v => CompetenceDate.Load(v))
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.Property(t => t.TotalOrderAmount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(t => t.TotalOrderTax)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(t => t.PaymentAuthenticationCode)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(t => t.PaymentDate)
            .HasColumnType("timestamp with time zone")
            .IsRequired(false);

        builder.Property(t => t.CreatedAt)
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.HasOne<TaxRule>()
            .WithMany()
            .HasForeignKey(t => t.TaxRuleId)
            .HasConstraintName("fk_taxentry_taxrule_taxruleid")
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(t => t.OrderId)
            .HasDatabaseName("idx_taxentry_orderid")
            .IsUnique();

        builder.HasIndex(t => t.CompetenceDate)
            .HasDatabaseName("idx_taxentry_competencedate");

    }
}
