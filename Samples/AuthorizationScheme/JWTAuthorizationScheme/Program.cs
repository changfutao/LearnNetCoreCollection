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
            Description = "�ӿ��ĵ�"
        };
        return Task.CompletedTask;
    });
});
new AppSettings(builder.Configuration);

#region Jwt��֤
var jwtSettings= AppSettings.Get<JwtSettings>("JwtSettings");
builder.Services.AddAuthentication(options =>
{
    // ��֤����: JWT
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    // ����Token��֤����
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, // �Ƿ���֤Issuer�������ߡ�
        ValidIssuer = jwtSettings.Issuer, // ������Issuer
        ValidateAudience = true, // �Ƿ���֤Audience �������ߡ�
        ValidAudience = jwtSettings.Audience, // ������Audience
        ValidateIssuerSigningKey = true, // �Ƿ���֤SecurityKey
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)), // �����߼���ǩ��
        ValidateLifetime = true, // �Ƿ���֤ʧЧʱ�䡾Token���ڡ�
        ClockSkew = TimeSpan.FromSeconds(30), // ����ʱ���ݴ�ֵ,�����������ʱ�䲻ͬ������(��)
        RequireExpirationTime = true
    };
    options.Events = new JwtBearerEvents()
    {
        OnChallenge = async context =>
        {
            context.Response.StatusCode = 401;
            // ����
            await context.Response.WriteAsync("401");
            context.HandleResponse();
        },
        OnForbidden = async context =>
        {
            context.Response.StatusCode = 403;
            // ����
            await context.Response.WriteAsync("403");
        }
    };
});
#endregion

#region ��Ȩ
builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
// ע����Ȩ����
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
// �����м����UseAuthentication����֤����������������Ҫ�����֤���м��ǰ���ã����� UseAuthorization����Ȩ����
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
