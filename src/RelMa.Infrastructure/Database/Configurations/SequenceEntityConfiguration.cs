using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RelMa.Domain.Sequences;

namespace RelMa.Infrastructure.Database.Configurations;

internal sealed class SequenceEntityConfiguration : IEntityTypeConfiguration<SequenceEntity>
{
    public void Configure(EntityTypeBuilder<SequenceEntity> builder)
    {
        builder.HasKey(t => new { t.SeqDate, t.TableName, t.TenantId });

        builder.Property(t => t.TenantId)
            .HasMaxLength(36);

        builder.Property(t => t.CurrentValue)
            .HasDefaultValue(0);

        builder.Property(t => t.TableName)
            .HasMaxLength(50);
    }
}
