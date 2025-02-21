using Serilog;
using Serilog.Context;

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

Log.Information("Hello Serilog");
// 简单类型
var count = 456;
Log.Information("Retrieved {Count} records", count);
bool isGood = true;
Log.Information("I am a good boy is {isGood}", isGood);
// 复杂类型
var fruit = new[] { "Apple", "Pear", "Orange" };
Log.Information("In my bowl I have {Fruit}", fruit);
var fruit1 = new Dictionary<string, int> { { "Apple", 1 }, { "Pear", 5 } };
Log.Information("In my bowl I have {Fruit}", fruit1);

// Enrichment
Log.Information("No contextual properties");

using (LogContext.PushProperty("A", 1))
{
    Log.Information("Carries property A = 1");

    using (LogContext.PushProperty("A", 2))
    using (LogContext.PushProperty("B", 1))
    {
        Log.Information("Carries A = 2 and B = 1");
    }

    Log.Information("Carries property A = 1, again");
}

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