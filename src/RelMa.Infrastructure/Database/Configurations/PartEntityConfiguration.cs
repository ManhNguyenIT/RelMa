using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RelMa.Domain.Items;
using RelMa.Domain.Parts;

namespace RelMa.Infrastructure.Database.Configurations;

internal sealed class PartEntityConfiguration : IEntityTypeConfiguration<PartEntity>
{
    public void Configure(EntityTypeBuilder<PartEntity> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.TenantId)
            .HasMaxLength(36);

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(t => t.Category)
            .HasMaxLength(100);

        builder.Property(t => t.Description)
            .HasMaxLength(500);

        builder.Property(t => t.PartNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(t => t.Image)
            .HasMaxLength(250);

        builder.Property(t => t.Quantity)
            .HasDefaultValue(0);

        builder.HasMany(t => t.Items)
            .WithMany(t => t.Parts)
            .UsingEntity<Dictionary<string, object>>(
                "PartsAndItems",
                r => r.HasOne<ItemEntity>().WithMany().HasForeignKey("ItemId"),
                l => l.HasOne<PartEntity>().WithMany().HasForeignKey("PartId"),
                je =>
                {
                    je.HasKey("PartId", "ItemId");
                });

    }
}

