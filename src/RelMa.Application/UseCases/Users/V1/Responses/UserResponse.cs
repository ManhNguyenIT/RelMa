namespace RelMa.Application.UseCases.Users.V1.Responses;

public class UserResponse
{
    public required string Id { get; set; }
    public string? Name { get; set; }
    public string? Username { get; set; }
    public string? Company { get; set; }
    public string? PhoneNumber { get; set; }
}
