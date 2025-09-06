using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RelMa.Domain.Tasks;

namespace RelMa.Infrastructure.Database.Configurations;

internal sealed class TaskEntityConfiguration : IEntityTypeConfiguration<TaskEntity>
{
    public void Configure(EntityTypeBuilder<TaskEntity> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.TenantId)
            .HasMaxLength(36);

        builder.HasOne(t => t.Asset)
            .WithMany(t => t.Tasks)
            .HasForeignKey(t => t.AssetId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

