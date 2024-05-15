using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Common.TestFramework.Fixtures;

public class WebApiFixtureBase<TProgram> : WebApplicationFactory<TProgram>
     where TProgram : class
{
    public ITestOutputHelper? TestOutput { get; set; }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseContentRoot(Directory.GetCurrentDirectory());
        ConfigureAppConfiguration(builder);
        ConfigureLogging(builder);
        ConfigureServices(builder);
    }

    private void ConfigureAppConfiguration(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(config =>
        {
            //config.AddInMemoryCollection(new Dictionary<string, string>
            //{
            //    { "IsIntegrationTest", "true" },
            //});
            //config.AddEnvironmentVariables();
            //config.AddJsonFile(Path.Combine(".", "appsettings.IntegrationTests.json"), false);
            //Configuration = config.Build();
        });
    }

    private void ConfigureLogging(IWebHostBuilder builder)
    {
        builder.ConfigureLogging(hostBuilder =>
        {
            hostBuilder.ClearProviders();
            var _output = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.TestOutput(TestOutput)
                .CreateLogger()
                .ForContext<WebApiFixtureBase<TProgram>>();
            hostBuilder.AddSerilog(_output);
        });

    }

    private void ConfigureServices(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
        });
    }
}
