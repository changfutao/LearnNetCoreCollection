using LearnNetCore.Basic.Dtos;
using LearnNetCore.Basic.Middlewares;
using LearnNetCore.Basic.Options;
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
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .WriteTo.Console()
    .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Information)
    .WriteTo.File("logs/Info/info.txt", rollingInterval: RollingInterval.Day)
    .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Warning)
    .WriteTo.File("logs/Warn/warn.txt", rollingInterval: RollingInterval.Day)
    .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Error)
    .WriteTo.File("logs/Error/error.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
Log.Warning("项目启动中...");
#endregion

// 注入Controller
builder.Services.AddControllers();

var app = builder.Build();
// 添加Controller中间件
app.MapControllers();

app.Run();
