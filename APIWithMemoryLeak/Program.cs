using Serilog;
using Serilog.Sinks.TestCorrelator;

Log.Logger = new LoggerConfiguration()
    .WriteTo.TestCorrelator()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

int i = 0;

app.MapGet("/logMessage", () =>
{
    var message = new string('#', 1024 * 100);
    using (TestCorrelator.CreateContext())
    {
        Log.Information(message);
    }
    return $"Message Log Count {++i}";
});

app.Run();
