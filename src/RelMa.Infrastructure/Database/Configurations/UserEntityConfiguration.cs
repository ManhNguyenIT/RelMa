using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RelMa.Domain.Users;

namespace RelMa.Infrastructure.Database.Configurations;

internal sealed class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.TenantId)
            .HasMaxLength(36);
    }
}

