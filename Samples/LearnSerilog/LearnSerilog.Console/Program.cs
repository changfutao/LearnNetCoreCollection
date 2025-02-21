using Serilog;

// 1.基础配置
//Log.Logger = new LoggerConfiguration()
//             // MinimunLevel Verbose => Debug => Information => Warning => Error => Fatal
//             // 如果没有指定,默认是Information
//             .MinimumLevel.Debug()
//             .WriteTo.Console()
//             .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true)
//             .CreateLogger();

// 2.将日志按照不同的级别进行记录
Log.Logger = new LoggerConfiguration()
             // MinimunLevel Verbose => Debug => Information => Warning => Error => Fatal
             // 如果没有指定,默认是Information
             .MinimumLevel.Debug()
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

Log.Information("Hello Serilog");
int a = 1;
int b = 0;
try
{
    int c = a / b;
}
catch (Exception e)
{
    Log.Error(e, "error");
}
// 结构化日志
var position = new { Latitude = 25, Longitude = 134 };
var elapsedMs = 35;
Log.Information("Processed{Position} in {Elapsed} ms", position, elapsedMs);
// @符号表明将Position序列化
Log.Information("Processed{@Position} in {Elapsed} ms", position, elapsedMs);