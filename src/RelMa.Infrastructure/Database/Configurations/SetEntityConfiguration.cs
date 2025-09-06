using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RelMa.Domain.Parts;
using RelMa.Domain.Sets;

namespace RelMa.Infrastructure.Database.Configurations;

internal sealed class SetEntityConfiguration : IEntityTypeConfiguration<SetEntity>
{
    public void Configure(EntityTypeBuilder<SetEntity> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.TenantId)
            .HasMaxLength(36);

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany(t => t.Parts)
            .WithMany(t => t.Sets)
            .UsingEntity<Dictionary<string, object>>(
                "SetsAndParts",
                r => r.HasOne<PartEntity>().WithMany().HasForeignKey("PartId"),
                l => l.HasOne<SetEntity>().WithMany().HasForeignKey("SetId"),
                je =>
                {
                    je.HasKey("PartId", "SetId");
                });

    }
}

