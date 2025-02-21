using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateBootstrapLogger();

try
{
    Log.Information("Starting web application");
    // 注册Razor Pages服务
    builder.Services.AddRazorPages();
    // 注册API控制器服务
    builder.Services.AddControllers();

    builder.Services.AddOpenApi();
    builder.Services.AddSerilog();
    var app = builder.Build();

    try
    {
        int a = 1;
        int b = 0;
        int c = a / b;
    }
    catch (Exception ex)
    {
        Log.Error(ex, "Application Start");
    }

    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }

    app.UseAuthorization();

    // 映射Razor Pages（默认路径基于Pages文件夹结构）
    app.MapRazorPages();

    // 映射API控制器（支持属性路由和约定路由）
    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
