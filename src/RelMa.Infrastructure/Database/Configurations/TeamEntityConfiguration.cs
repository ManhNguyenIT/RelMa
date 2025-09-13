using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RelMa.Domain.Teams;
using RelMa.Domain.Users;

namespace RelMa.Infrastructure.Database.Configurations;

internal sealed class TeamEntityConfiguration : IEntityTypeConfiguration<TeamEntity>
{
    public void Configure(EntityTypeBuilder<TeamEntity> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .ValueGeneratedNever();

        builder.Property(t => t.TenantId)
            .HasMaxLength(36);

        builder.HasOne(t => t.Leader)
            .WithMany()
            .HasForeignKey(t => t.LeaderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(t => t.Members)
            .WithMany(t => t.Teams)
            .UsingEntity<Dictionary<string, object>>(
                "TeamsAndMembers",
                r => r.HasOne<UserEntity>().WithMany().HasForeignKey("UserId"),
                l => l.HasOne<TeamEntity>().WithMany().HasForeignKey("TeamId"),
                je =>
                {
                    je.HasKey("UserId", "TeamId");
                });
    }
}
