using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateBootstrapLogger();

try
{
    Log.Information("Starting web application");
    // ע��Razor Pages����
    builder.Services.AddRazorPages();
    // ע��API����������
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

    // ӳ��Razor Pages��Ĭ��·������Pages�ļ��нṹ��
    app.MapRazorPages();

    // ӳ��API��������֧������·�ɺ�Լ��·�ɣ�
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
