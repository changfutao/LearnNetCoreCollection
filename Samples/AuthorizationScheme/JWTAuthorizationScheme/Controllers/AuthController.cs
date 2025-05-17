using JWTAuthorizationScheme.Attributes;
using JWTAuthorizationScheme.Dtos;
using JWTAuthorizationScheme.Helpers;
using JWTAuthorizationScheme.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace JWTAuthorizationScheme.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public IResultOutput Login(LoginInput input)
        {
            if (input.UserName == "admin" && input.Password == "admin") 
            {
                var claims = new List<Claim>
                { 
                    new Claim(ClaimAttributes.UserName, "admin"),
                    new Claim(ClaimAttributes.UserId, "1"),
                    new Claim(ClaimAttributes.UserNickName, "超级管理员"),
                };
                string token = JwtHelper.CreateToken(claims);
                return ResultOutput.Ok(token);
            }
            return ResultOutput.NotOk<string>("用户名或密码错误");
        }
    }
}
