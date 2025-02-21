using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateBootstrapLogger();

try
{
    Log.Information("Starting web application");
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

    int i = 1;
    while (i < 100000)
    {
        Log.Information("I am {i}", i);
        Log.Information("Haha");
        i++;
    }
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }

    app.UseAuthorization();

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
