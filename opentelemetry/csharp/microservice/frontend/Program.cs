using frontend;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHttpClient<WeatherClient>(client =>
{
    var backendSection = builder.Configuration.GetSection("Backend");
    client.BaseAddress = new Uri(backendSection["BaseAddress"]);
});

builder.Services.AddOpenTelemetry()
    .WithTracing(tracerProviderBuilder =>
        tracerProviderBuilder
            .AddSource(DiagnosticsConfig.ActivitySource.Name)
            .ConfigureResource(resource => resource
                .AddService(DiagnosticsConfig.ServiceName))
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddConsoleExporter()
            .AddOtlpExporter())
    //.WithMetrics(metricsProviderBuilder =>
    //    metricsProviderBuilder
    //        .ConfigureResource(resource => resource
    //            .AddService(DiagnosticsConfig.ServiceName))
    //        .AddAspNetCoreInstrumentation()
    //        .AddConsoleExporter()
    //        .AddOtlpExporter())
    ;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
