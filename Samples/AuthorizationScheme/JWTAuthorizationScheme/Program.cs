using JWTAuthorizationScheme.Auth;
using JWTAuthorizationScheme.Helpers;
using JWTAuthorizationScheme.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Info = new()
        {
            Title = "WebAPI",
            Version = "V1",
            Description = "接口文档"
        };
        return Task.CompletedTask;
    });
});
new AppSettings(builder.Configuration);

#region Jwt认证
var jwtSettings= AppSettings.Get<JwtSettings>("JwtSettings");
builder.Services.AddAuthentication(options =>
{
    // 认证方案: JWT
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    // 配置Token验证参数
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, // 是否验证Issuer【发布者】
        ValidIssuer = jwtSettings.Issuer, // 发布者Issuer
        ValidateAudience = true, // 是否验证Audience 【订阅者】
        ValidAudience = jwtSettings.Audience, // 订阅者Audience
        ValidateIssuerSigningKey = true, // 是否验证SecurityKey
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)), // 发布者加密签名
        ValidateLifetime = true, // 是否验证失效时间【Token过期】
        ClockSkew = TimeSpan.FromSeconds(30), // 过期时间容错值,解决服务器端时间不同步问题(秒)
        RequireExpirationTime = true
    };
    options.Events = new JwtBearerEvents()
    {
        OnChallenge = async context =>
        {
            context.Response.StatusCode = 401;
            // 报错
            await context.Response.WriteAsync("401");
            context.HandleResponse();
        },
        OnForbidden = async context =>
        {
            context.Response.StatusCode = 403;
            // 报错
            await context.Response.WriteAsync("403");
        }
    };
});
#endregion

#region 授权
builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
// 注册授权策略
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(Permissions.UserCreate, policy => policy.AddRequirements(new PermissionAuthorizationRequirement(Permissions.UserCreate)));
    options.AddPolicy(Permissions.UserUpdate, policy => policy.AddRequirements(new PermissionAuthorizationRequirement(Permissions.UserUpdate)));
    options.AddPolicy(Permissions.UserDelete, policy => policy.AddRequirements(new PermissionAuthorizationRequirement(Permissions.UserDelete)));
});
builder.Services.AddSingleton<IAuthorizationPolicyProvider, TestAuthorizationPolicyProvider>();
#endregion
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference(); // scalar/v1
    app.MapOpenApi();
}
// 调用中间件：UseAuthentication（认证），必须在所有需要身份认证的中间件前调用，比如 UseAuthorization（授权）。
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
