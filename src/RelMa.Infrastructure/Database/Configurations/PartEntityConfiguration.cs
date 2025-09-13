using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RelMa.Domain.Parts;

namespace RelMa.Infrastructure.Database.Configurations;

internal sealed class PartEntityConfiguration : IEntityTypeConfiguration<PartEntity>
{
    public void Configure(EntityTypeBuilder<PartEntity> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .ValueGeneratedNever();

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

