using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RelMa.Domain.WorkOrders;

namespace RelMa.Infrastructure.Database.Configurations;

internal sealed class WorkOrderEntityConfiguration : IEntityTypeConfiguration<WorkOrderEntity>
{
    public void Configure(EntityTypeBuilder<WorkOrderEntity> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.TenantId)
            .HasMaxLength(36);

        builder.HasOne(t => t.Maintenance)
            .WithOne(t => t.WorkOrder)
            .HasForeignKey<WorkOrderEntity>(t => t.MaintenanceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(t => t.Request)
            .WithOne(t => t.WorkOrder)
            .HasForeignKey<WorkOrderEntity>(t => t.RequestId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

