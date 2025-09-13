using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RelMa.Domain.Files;

namespace RelMa.Infrastructure.Database.Configurations;

internal sealed class FileEntityConfiguration : IEntityTypeConfiguration<FileEntity>
{
    public void Configure(EntityTypeBuilder<FileEntity> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .ValueGeneratedNever();

        builder.Property(t => t.TenantId)
            .HasMaxLength(36);

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(t => t.Size)
            .HasDefaultValue(0);

        builder.Property(t => t.Ext)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(t => t.Source)
            .IsRequired()
            .HasMaxLength(250);
    }
}

