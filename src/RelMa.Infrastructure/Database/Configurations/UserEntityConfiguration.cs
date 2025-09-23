using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RelMa.Domain.Users;

namespace RelMa.Infrastructure.Database.Configurations;

internal sealed class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .ValueGeneratedNever();

        builder.Property(t => t.TenantId)
            .HasMaxLength(36);

        builder.Property(t => t.Name)
            .HasMaxLength(100);

        builder.Property(t => t.Username)
            .HasMaxLength(20);

        builder.Property(t => t.Company)
            .HasMaxLength(200);

        builder.Property(t => t.PhoneNumber)
            .HasMaxLength(20);
    }
}

