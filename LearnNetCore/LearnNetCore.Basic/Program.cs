using LearnNetCore.Basic.DbContexts;
using LearnNetCore.Basic.Dtos;
using LearnNetCore.Basic.Hubs;
using LearnNetCore.Basic.Middlewares;
using LearnNetCore.Basic.Options;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);



#region 选项
builder.Services.AddCustomOptions(builder.Configuration);
#endregion

#region Startup筛选器
builder.Services.AddTransient<IStartupFilter, RequestSetOptionsStartupFilter>();
#endregion

#region 依赖注入测试
builder.Services.AddConfig();
#endregion

#region Serilog
Log.Logger = new LoggerConfiguration()
             // MinimunLevel Verbose => Debug => Information => Warning => Error => Fatal
             // 如果没有指定,默认是Information
             .MinimumLevel.Information()
             .Enrich.FromLogContext()
             .WriteTo.Console()
             .WriteTo.Logger(lc =>
                    lc.Filter.ByIncludingOnly(l => l.Level == Serilog.Events.LogEventLevel.Information)
                    .WriteTo.File("logs/Info/info-.txt", rollingInterval: RollingInterval.Day)
                    )
             .WriteTo.Logger(lc =>
                    lc.Filter.ByIncludingOnly(l => l.Level == Serilog.Events.LogEventLevel.Error)
                    .WriteTo.File("logs/Error/error-.txt", rollingInterval: RollingInterval.Day)
             )
             .CreateLogger();
Log.Information("项目启动中...");
#endregion

#region SignalR
builder.Services.AddSignalR();
#endregion

#region CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("allow", policy =>
    {
        policy.SetIsOriginAllowed(origin => true)
           .AllowAnyHeader()
           .AllowAnyMethod()
           .AllowCredentials();
    });
});
#endregion

#region EFCore
builder.Services.AddDbContext<TestDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
#endregion

// 注入Controller
builder.Services.AddControllers();

var app = builder.Build();

#region 依赖注入
// 在应用启动时解析服务
// 1.transient
var myDependencyTransient= app.Services.GetRequiredService<IMyDependencyTransient>();
Console.WriteLine($"依赖注入, Transient：{myDependencyTransient.Id.ToString("N")}");
// 2.scoped
using (var serviceScope = app.Services.CreateScope())
{
    var sevices = serviceScope.ServiceProvider;
    var myDependencyScoped = sevices.GetRequiredService<IMyDependencyScoped>();
    Console.WriteLine($"依赖注入, Scoped：{myDependencyScoped.Id.ToString("N")}");
}

#endregion
app.UseStaticFiles();
#region 中间件
app.Use(async (context, next) =>
{
    context.Response.Headers["test"] = "test";
    await next.Invoke();
});
#endregion

app.MapHub<MyHub>("myhub");
app.UseCors("allow");
// 添加Controller中间件
app.MapControllers();

app.Run("http://*:5000");

