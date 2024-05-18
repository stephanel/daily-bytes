using Common.Extensions.API.Observability;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using Serilog.Enrichers.Span;
using Serilog.Events;
using Serilog.Sinks.OpenTelemetry;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Common.Extensions.DependencyInjection;

public static class ObservabilityRegistration
{
    private const string DefaultServiceName = "UnknownService";

    private static readonly ReadOnlyDictionary<string, LogLevel?> defaultLogLevels
        = new Dictionary<string, LogLevel?>
        {
            ["Host"] = LogLevel.Information,
            ["MassTransit"] = LogLevel.Warning,
            ["Microsoft"] = LogLevel.Warning,
            ["System"] = LogLevel.Warning
        }.AsReadOnly();

    private static Func<string> GetOtelServiceName = () 
        => Environment.GetEnvironmentVariable("OTEL_SERVICE_NAME") ?? DefaultServiceName;

    private static Func<string?> GetOtelEndpoint = ()
        => Environment.GetEnvironmentVariable("OTEL_EXPORTER_OTPL_ENDPOINT");

    public static IHostApplicationBuilder ConfigureObservability(this IHostApplicationBuilder builder)
    {
        builder.AddOpenTelemetry();

        ((WebApplicationBuilder)builder).Host.ConfigureLogging();

        builder.Services.ConfigureMetrics();

        return builder;
    }

    private static IHostApplicationBuilder AddOpenTelemetry(this IHostApplicationBuilder builder)
    {
        builder.Logging.AddOpenTelemetry(logging =>
        {
            logging.IncludeFormattedMessage = true;
            logging.IncludeScopes = true;
            logging.AddOtlpExporter();
        });

        builder.Services.AddOpenTelemetry()
            .WithMetrics(ConfigureMeters)
            .WithTracing(tp => tp.ConfigureTraces(builder.Environment));

        builder.AddOpenTelemetryExporters();

        return builder;
    }

    private static void ConfigureMeters(this MeterProviderBuilder builder)
    {
        var serviceName = GetOtelServiceName();
        builder.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName))
            .AddMeter($"{serviceName.Replace(".", string.Empty)}Meter")
            .AddRuntimeInstrumentation()
            .AddProcessInstrumentation()
            .AddAspNetCoreInstrumentation()
            // Microsoft.AspNetCore.Hosting
            // Microsoft.AspNetCore.Server.Kestrel
            // System.Net.Http
            // Books.API
            .AddOtlpExporter()
            .AddConsoleExporter()
            //.AddPrometheusExporter()
            ;
    }

    private static void ConfigureTraces(this TracerProviderBuilder builder, IHostEnvironment environment)
    {
        var serviceName = GetOtelServiceName();
        if (environment.IsDevelopment())
        {
            // We want to view all traces in development
            builder.SetSampler(new AlwaysOnSampler());
        }

        builder.AddSource(new ActivitySource(serviceName).Name)
            .ConfigureResource(resource => resource.AddService(serviceName))
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddEntityFrameworkCoreInstrumentation(opt =>
            {
                opt.SetDbStatementForStoredProcedure = true;
                opt.SetDbStatementForText = true;
            })
            .AddNpgsql()
            .AddConsoleExporter()
            .AddOtlpExporter();
    }
    private static IServiceCollection ConfigureMetrics(this IServiceCollection services)
    {
        return services
            .AddMetrics()  // Microsoft.Extensions.Diagnostics
            .AddSingleton<APICustomMetrics>();
    }

    private static IHostBuilder ConfigureLogging(this IHostBuilder builder,
        ReadOnlyDictionary<string, LogLevel?>? logLevels = null)
    {
        builder.UseSerilog((ctx, config) =>
        {
            config.ReadFrom.Configuration(ctx.Configuration)
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithThreadId()
                .Enrich.WithSpan()
                .Enrich.WithProperty("app", ctx.HostingEnvironment.ApplicationName)
                .MinimumLevel.Is(LogEventLevel.Debug)
                ;

            //List<string> logSources = ["Host", "MassTransit", "Microsoft", "System"];
            //logSources.ForEach(source => config.MinimumLevel.Override(source, logLevels.GetLogEventLevel(source)));
            ConfigureLogLevels(["Host", "MassTransit", "Microsoft", "System"], config, logLevels);

            config.WriteTo.Console();
            //config.WriteTo.Async(wt => wt.Console(formatter: new ElasticsearchJsonFormatter(inlineFields: true)));
            //config.WriteTo.Seq("http://localhost:5341");

            var otelBaseEndpoint = GetOtelEndpoint();
            if(otelBaseEndpoint is not null)
            {
                config.WriteTo.OpenTelemetry(opt =>
                {
                    opt.Endpoint = $"{otelBaseEndpoint}/v1/logs";
                    opt.IncludedData = IncludedData.TraceIdField | IncludedData.SpanIdField;
                    opt.ResourceAttributes = new Dictionary<string, object>
                    {
                        ["service.name"] = Environment.GetEnvironmentVariable("OTEL_SERVICE_NAME") ?? "UnknownService"
                    };
                });
            }
        });

        return builder;
    }

    private static IHostApplicationBuilder AddOpenTelemetryExporters(this IHostApplicationBuilder builder)
    {
        var useOtlpExporter = !string.IsNullOrWhiteSpace(builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"]);

        if (useOtlpExporter)
        {
            builder.Services.Configure<OpenTelemetryLoggerOptions>(logging => logging.AddOtlpExporter());
            builder.Services.ConfigureOpenTelemetryMeterProvider(metrics => metrics.AddOtlpExporter());
            builder.Services.ConfigureOpenTelemetryTracerProvider(tracing => tracing.AddOtlpExporter());
        }

        // Uncomment the following lines to enable the Prometheus exporter (requires the OpenTelemetry.Exporter.Prometheus.AspNetCore package)
        // builder.Services.AddOpenTelemetry()
        //    .WithMetrics(metrics => metrics.AddPrometheusExporter());

        return builder;
    }

    private static void ConfigureLogLevels(
        List<string> logSources, 
        LoggerConfiguration config,
        ReadOnlyDictionary<string, LogLevel?>? logLevels)
    {
        var map = (string source) => Map((logLevels ?? defaultLogLevels).GetValueOrDefault(source));
        logSources.ForEach(source => config.MinimumLevel.Override(source, map(source)));
    }

    private static LogEventLevel Map(LogLevel? logLevel)
        => logLevel switch
        {
            _ => LogEventLevel.Debug
        };
}