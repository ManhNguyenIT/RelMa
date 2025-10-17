using RelMa.Application.UseCases.Users.V1.Responses;

namespace RelMa.Application.UseCases.Teams.V1.Responses;

public class TeamResponse
{
    public required DefaultIdType Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required DefaultIdType LeaderId { get; set; }
    public virtual UserResponse? Leader { get; set; }
    public virtual IEnumerable<UserResponse>? Members { get; set; }
}
