using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RelMa.Domain.AssetLogs;

namespace RelMa.Infrastructure.Database.Configurations;

internal sealed class AssetLogEntityConfiguration : IEntityTypeConfiguration<AssetLogEntity>
{
    public void Configure(EntityTypeBuilder<AssetLogEntity> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .ValueGeneratedNever();

        builder.Property(t => t.TenantId)
            .HasMaxLength(36);

        builder.Property(t => t.Description)
            .HasMaxLength(500);

        builder.HasOne(t => t.Asset)
            .WithMany(t => t.AssetLogs)
            .HasForeignKey(t => t.AssetId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

