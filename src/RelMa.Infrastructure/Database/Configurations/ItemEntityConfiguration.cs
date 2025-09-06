using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RelMa.Domain.Items;

namespace RelMa.Infrastructure.Database.Configurations;

internal sealed class ItemEntityConfiguration : IEntityTypeConfiguration<ItemEntity>
{
    public void Configure(EntityTypeBuilder<ItemEntity> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.TenantId)
            .HasMaxLength(36);

        builder.HasOne(t => t.Storage)
            .WithMany(t => t.Items)
            .HasForeignKey(t => t.StorageId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(t => t.Location)
            .WithMany(t => t.Items)
            .HasForeignKey(t => t.LocationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(t => t.Material)
            .WithMany(t => t.Items)
            .HasForeignKey(t => t.MaterialId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

