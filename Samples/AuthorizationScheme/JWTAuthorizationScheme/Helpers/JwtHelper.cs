using JWTAuthorizationScheme.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace JWTAuthorizationScheme.Helpers
{
    public class JwtHelper
    {
        /// <summary>
        /// 生成Token
        /// </summary>
        /// <param name="claims">声明</param>
        /// <returns></returns>
        public static string CreateToken(List<Claim> claims)
        {
            var jwtSettings = AppSettings.Get<JwtSettings>("JwtSettings");
            // 1.生成签名密钥
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));
            // 2.选择加密算法
            var algorithm = SecurityAlgorithms.HmacSha256;
            // 3.生成Credentials
            var signingCredentials = new SigningCredentials(secretKey, algorithm);
            // 4.生成Token
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: jwtSettings.Issuer, // 发布者
                audience: jwtSettings.Audience, // 订阅者
                signingCredentials: signingCredentials, // 签名凭证
                claims: claims, // 声明
                notBefore: DateTime.Now, // 生效时间
                expires: DateTime.Now.AddMinutes(jwtSettings.ExpireTime) // 过期时间
                );
            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return token;
        }
    }
}
