using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RelMa.Domain.Requests;

namespace RelMa.Infrastructure.Database.Configurations;

internal sealed class RequestEntityConfiguration : IEntityTypeConfiguration<RequestEntity>
{
    public void Configure(EntityTypeBuilder<RequestEntity> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .ValueGeneratedNever();

        builder.Property(t => t.TenantId)
            .HasMaxLength(36);

        builder.HasOne(t => t.Asset)
            .WithMany(t => t.Requests)
            .HasForeignKey(t => t.AssetId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(t => t.WorkOrder)
            .WithOne(t => t.Request)
            .HasForeignKey<RequestEntity>(t => t.WorkOrderId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}

