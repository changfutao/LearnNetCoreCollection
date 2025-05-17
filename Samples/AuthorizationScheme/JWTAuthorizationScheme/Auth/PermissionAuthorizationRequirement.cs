using Microsoft.AspNetCore.Authorization;

namespace JWTAuthorizationScheme.Auth
{
    public class PermissionAuthorizationRequirement: IAuthorizationRequirement
    {
        public string Name;

        public PermissionAuthorizationRequirement(string name)
        {
            Name = name;
        }
    }
}
