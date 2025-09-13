using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RelMa.Domain.Checklists;

namespace RelMa.Infrastructure.Database.Configurations;

internal sealed class ChecklistEntityConfiguration : IEntityTypeConfiguration<ChecklistEntity>
{
    public void Configure(EntityTypeBuilder<ChecklistEntity> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .ValueGeneratedNever();

        builder.Property(t => t.TenantId)
            .HasMaxLength(36);

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(100);

        //builder.HasOne(t => t.Task)
        //    .WithMany(t => t.Checklists)
        //    .HasForeignKey(t => t.LocationId)
        //    .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(t => t.WorkOrder)
            .WithMany(t => t.Checklists)
            .HasForeignKey(t => t.WorkOrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

