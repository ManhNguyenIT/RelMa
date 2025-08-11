namespace RelMa.Application.Abstractions.Authentication;

public interface IUserContext
{
    string UserId { get; }
    string TenantId { get; }
    Task<string> GetConnectionString();
}
