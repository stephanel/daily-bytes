using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Enrichers.Span;
using Serilog.Events;
using Serilog.Formatting.Elasticsearch;
using System.Collections.ObjectModel;

namespace Common.Extensions.DependencyInjection;

public static class HostBuilderExtensions
{
    private static readonly ReadOnlyDictionary<string, LogLevel?> defaultLogLevels
        = new Dictionary<string, LogLevel?>
        {
            ["Host"]= LogLevel.Information,
            ["MassTransit"] = LogLevel.Warning,
            ["Microsoft"] = LogLevel.Warning,
            ["System"] = LogLevel.Warning
        }.AsReadOnly();

public static IHostBuilder ConfigureLogging(this IHostBuilder builder,
        ReadOnlyDictionary<string, LogLevel?>? logLevels = null)
    {
        return builder.UseSerilog((ctx, config) =>
        {
            config.ReadFrom.Configuration(ctx.Configuration)
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithThreadId()
                .Enrich.WithSpan()
                .Enrich.WithProperty("app", ctx.HostingEnvironment.ApplicationName)
                .MinimumLevel.Is(LogEventLevel.Debug)
                ;

            List<string> logSources = ["Host", "MassTransit", "Microsoft", "System"];
            logSources.ForEach(source 
                => config.MinimumLevel.Override(source, logLevels.GetLogEventLevel(source)));

            config.WriteTo.Console();
            //config.WriteTo.Async(wt => wt.Console(formatter: new ElasticsearchJsonFormatter(inlineFields: true)));
            //config.WriteTo.Seq("http://localhost:5341");
        });
    }

    private static LogEventLevel GetLogEventLevel(
        this ReadOnlyDictionary<string, LogLevel?>? logLevels, 
        string source)
        => Map(
            (logLevels ?? defaultLogLevels).GetValueOrDefault(source));

    private static LogEventLevel Map(LogLevel? logLevel)
        => logLevel switch
        {
            _ => LogEventLevel.Debug
        };
}