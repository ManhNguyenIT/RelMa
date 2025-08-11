using Microsoft.AspNetCore.Authorization;

namespace RelMa.Authorization;
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public sealed class HasPermissionAttribute(string permission) : AuthorizeAttribute(permission)
{
    public string Permission { get; } = permission;
}
