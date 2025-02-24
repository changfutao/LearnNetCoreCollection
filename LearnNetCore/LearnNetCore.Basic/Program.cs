using LearnNetCore.Basic.Dtos;
using LearnNetCore.Basic.Middlewares;
using LearnNetCore.Basic.Options;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);



#region ѡ��
builder.Services.AddCustomOptions(builder.Configuration);
#endregion

#region Startupɸѡ��
builder.Services.AddTransient<IStartupFilter, RequestSetOptionsStartupFilter>();
#endregion

#region ����ע�����
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
Log.Warning("��Ŀ������...");
#endregion

// ע��Controller
builder.Services.AddControllers();

var app = builder.Build();

#region ����ע��
// ��Ӧ������ʱ��������
// 1.transient
var myDependencyTransient= app.Services.GetRequiredService<IMyDependencyTransient>();
Console.WriteLine($"����ע��, Transient��{myDependencyTransient.Id.ToString("N")}");
// 2.scoped
using (var serviceScope = app.Services.CreateScope())
{
    var sevices = serviceScope.ServiceProvider;
    var myDependencyScoped = sevices.GetRequiredService<IMyDependencyScoped>();
    Console.WriteLine($"����ע��, Scoped��{myDependencyScoped.Id.ToString("N")}");
}

#endregion

#region �м��
app.Use(async (context, next) =>
{
    context.Response.Headers["test"] = "test";
    await next.Invoke();
});
#endregion

// ���Controller�м��
app.MapControllers();

app.Run();
