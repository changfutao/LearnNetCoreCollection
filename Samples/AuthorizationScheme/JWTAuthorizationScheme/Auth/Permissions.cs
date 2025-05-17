namespace JWTAuthorizationScheme.Auth
{
    /// <summary>
    /// 定义权限常量
    /// </summary>
    public static class Permissions
    {
        public const string User = "User";
        public const string UserCreate = User + ".Create";
        public const string UserUpdate = User + ".Update";
        public const string UserDelete = User + ".Delete";
    }
}
