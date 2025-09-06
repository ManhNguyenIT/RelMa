using RelMa.Domain.Teams;
using RelMa.Shared.Abstractions.Entity;

namespace RelMa.Domain.Users;

public class UserEntity : Entity<string>
{
    public UserEntity()
    {
        Teams = new HashSet<TeamEntity>();
    }
    public string? Name { get; set; }
    public string? Username { get; set; }
    public string? Company { get; set; }
    public string? PhoneNumber { get; set; }
    public virtual ICollection<TeamEntity> Teams { get; }
}
