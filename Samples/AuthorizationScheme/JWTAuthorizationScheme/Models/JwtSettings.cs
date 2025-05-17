namespace JWTAuthorizationScheme.Models
{
    /// <summary>
    /// JWT配置
    /// </summary>
    public class JwtSettings
    {
        /// <summary>
        /// 密钥key
        /// </summary>
        public string SecretKey { get; set; }
        /// <summary>
        /// 发布者
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// 订阅者
        /// </summary>
        public string Audience { get; set; }
        /// <summary>
        /// 过期时间(分钟)
        /// </summary>
        public int ExpireTime { get; set; }
    }
}
