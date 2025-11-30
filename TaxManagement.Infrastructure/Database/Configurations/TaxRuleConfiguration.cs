using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaxManagement.Domain.Entities;

namespace TaxManagement.Infrastructure.Database.Configurations;

internal sealed class TaxRuleConfiguration : IEntityTypeConfiguration<TaxRule>
{
    public void Configure(EntityTypeBuilder<TaxRule> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.OriginState)
            .HasMaxLength(2)
            .IsFixedLength()
            .IsRequired();

        builder.Property(e => e.DestinationState)
            .HasMaxLength(2)
            .IsFixedLength()
            .IsRequired();

        builder.Property(e => e.EffectiveDate)
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.Property(e => e.InterstateRate)
            .HasPrecision(10, 4)
            .IsRequired();

        builder.Property(e => e.DifalRate)
            .HasPrecision(10, 4)
            .IsRequired();

        builder.Property(e => e.FcpRate)
            .HasPrecision(10, 4)
            .IsRequired();

        builder.Property(e => e.CreatedAt)
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.HasIndex(e => new { e.OriginState, e.DestinationState, e.EffectiveDate })
            .HasDatabaseName("idx_taxrules_origin_dest_date")
            .IsUnique();
    }
}
