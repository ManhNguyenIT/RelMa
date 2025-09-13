using RelMa.Domain.Users;
using RelMa.Shared.Abstractions.Entity;

namespace RelMa.Domain.Teams;

public class TeamEntity : Entity<DefaultIdType>
{
    public TeamEntity()
    {
        Members = new HashSet<UserEntity>();
    }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required string LeaderId { get; set; }
    public virtual UserEntity? Leader { get; set; }
    public virtual ICollection<UserEntity> Members { get; }
}
