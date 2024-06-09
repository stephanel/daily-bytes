using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Net.WebSockets;

namespace Common.TestFramework.Fixtures;

public abstract class WebApiFixture<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    private readonly IMessageSink _messageSink;

    public HttpClient Client => CreateClient();

    public T GetRequiredService<T>() where T : notnull => Services.GetRequiredService<T>();

    protected virtual IReadOnlyDictionary<string, string?> AppSettings { get; } = new Dictionary<string, string?>();

    protected WebApiFixture(IMessageSink diagnosticMessageSink)
    {
        _messageSink = diagnosticMessageSink;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        ConfigureAppConfiguration(builder);
        ConfigureLogging(builder);
        ConfigureServices(builder);

        builder.UseEnvironment("Development");
    }

    private void ConfigureAppConfiguration(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(config =>
        {
            config.AddInMemoryCollection(AppSettings);
        });
    }

    private void ConfigureLogging(IWebHostBuilder builder)
    {
        builder.ConfigureLogging(hostBuilder =>
        {
            hostBuilder.ClearProviders();
            var _output = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.TestOutput(_messageSink)
                .CreateLogger()
                .ForContext<WebApiFixture<TProgram>>();
            hostBuilder.AddSerilog(_output);
        });
    }

    private void ConfigureServices(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services => 
        {
            foreach(var serviceDescriptor in ServicesToReplace)
            {

                services
                    .Where(descriptor => descriptor.ServiceType == serviceDescriptor.ServiceType)
                    .ToList()
                    .ForEach(s => services.Remove(s));
                services.Add(serviceDescriptor);
            }

            // customize service configuration here
        });
    }

    protected virtual List<ServiceDescriptor> ServicesToReplace => [];

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        Client?.Dispose();
    }
}

public record ServiceToReplace(Type Key, Type NewType);