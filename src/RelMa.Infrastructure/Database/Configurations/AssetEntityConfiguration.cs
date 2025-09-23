using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RelMa.Domain.Assets;

namespace RelMa.Infrastructure.Database.Configurations;

internal sealed class AssetEntityConfiguration : IEntityTypeConfiguration<AssetEntity>
{
    public void Configure(EntityTypeBuilder<AssetEntity> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .ValueGeneratedNever();

        builder.Property(t => t.TenantId)
            .HasMaxLength(36);

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(t => t.Area)
            .HasMaxLength(250);

        builder.Property(t => t.SerialNumber)
            .HasMaxLength(100);

        builder.Property(t => t.Category)
            .HasMaxLength(100);

        builder.Property(t => t.Description)
            .HasMaxLength(500);

        builder.Property(t => t.Model)
            .HasMaxLength(50);

        builder.Property(t => t.Code)
            .HasMaxLength(50);

        builder.HasOne(t => t.Location)
            .WithMany(t => t.Assets)
            .HasForeignKey(t => t.LocationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

