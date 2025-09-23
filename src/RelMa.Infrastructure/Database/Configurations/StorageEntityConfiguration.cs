using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RelMa.Domain.Storages;

namespace RelMa.Infrastructure.Database.Configurations;

internal sealed class StorageEntityConfiguration : IEntityTypeConfiguration<StorageEntity>
{
    public void Configure(EntityTypeBuilder<StorageEntity> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .ValueGeneratedNever();

        builder.Property(t => t.TenantId)
            .HasMaxLength(36);

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(t => t.Description)
            .HasMaxLength(500);

        builder.HasOne(t => t.Location)
            .WithMany(t => t.Storages)
            .HasForeignKey(t => t.LocationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

