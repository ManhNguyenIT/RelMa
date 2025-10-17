using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RelMa.Domain.Assets;
using RelMa.Domain.Maintenances;

namespace RelMa.Infrastructure.Database.Configurations;

internal sealed class MaintenanceEntityConfiguration : IEntityTypeConfiguration<MaintenanceEntity>
{
    public void Configure(EntityTypeBuilder<MaintenanceEntity> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .ValueGeneratedNever();

        builder.Property(t => t.TenantId)
            .HasMaxLength(36);

        builder.Property(t => t.CronExpression)
            .IsRequired()
            .HasMaxLength(20);

        builder.HasMany(t => t.Assets)
            .WithMany(t => t.Maintenances)
            .UsingEntity<Dictionary<DefaultIdType, DefaultIdType>>(
                "AssetsAndMaintenances",
                r => r.HasOne<AssetEntity>().WithMany().HasForeignKey("AssetId"),
                l => l.HasOne<MaintenanceEntity>().WithMany().HasForeignKey("MaintenanceId"),
                je =>
                {
                    je.HasKey("AssetId", "MaintenanceId");
                });
    }
}

