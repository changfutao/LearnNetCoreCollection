using Microsoft.AspNetCore.Authorization;

namespace JWTAuthorizationScheme.Auth
{
    /// <summary>
    /// 授权处理器
    /// </summary>
    public class PermissionAuthorizationHandler: AuthorizationHandler<PermissionAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionAuthorizationRequirement requirement)
        {
            var permissions = context.User.Claims.Where(a => a.Type == "Permission").Select(a => a.Value).ToList();
            if (permissions.Any(a => a.StartsWith(requirement.Name)))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
