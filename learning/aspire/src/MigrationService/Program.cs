using Consumer.Persistence;
using MigrationService;
using ServiceDefaults;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddHostedService<Worker>();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

builder.AddNpgsqlDbContext<WeatherForcastHistoryDatabaseContext>("weatherforcasthistory");

var host = builder.Build();
await host.RunAsync();