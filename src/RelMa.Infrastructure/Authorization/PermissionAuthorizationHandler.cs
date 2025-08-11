using Microsoft.AspNetCore.Authorization;

namespace RelMa.Authorization;
internal sealed class PermissionAuthorizationHandler(PermissionProvider permissionProvider) : AuthorizationHandler<PermissionRequirement>
{
    private readonly PermissionProvider _permissionProvider = permissionProvider ?? throw new ArgumentNullException(nameof(permissionProvider));

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        // Reject unauthenticated users
        if (context.User?.Identity?.IsAuthenticated != true)
        {
            context.Fail();
            return;
        }

        // Retrieve permissions for the user
        var permissions = await _permissionProvider.GetUserPermissions();

        // Check if the user has the required permission
        if (permissions?.Contains(requirement.Permission) == true)
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }
    }
}
