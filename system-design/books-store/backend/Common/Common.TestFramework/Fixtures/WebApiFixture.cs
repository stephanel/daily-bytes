using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Common.TestFramework.Fixtures;

public abstract class WebApiFixture<TProgram> : IntegrationTestBaseFixture  where TProgram : class
{
    private TestWebApplicationFactory WebApplicationFactory { get; set; } = null!;

    public HttpClient Client => WebApplicationFactory.CreateClient();

    private IServiceProvider? _serviceProvider;

    protected override IServiceProvider ServiceProvider => _serviceProvider!;

    protected virtual IReadOnlyDictionary<string, string?> CustomSettings { get; } = new Dictionary<string, string?>();

    protected WebApiFixture(IMessageSink diagnosticMessageSink) : base(diagnosticMessageSink)
    { }

    protected override async Task CustomInitializeAsync()
    {
        WebApplicationFactory = new TestWebApplicationFactory(DatabaseConnectionString!, MessageSink!, CustomSettings);
        _serviceProvider = WebApplicationFactory.Services;
        await RunDatabaseMigration();
    }

    protected abstract Task RunDatabaseMigration();

    protected override void DisposeManagedResources()
    {
        Client?.Dispose();
        WebApplicationFactory.Dispose();
    }

    private class TestWebApplicationFactory : WebApplicationFactory<TProgram>
    {
        private readonly IMessageSink _messageSink;

        private readonly Dictionary<string, string?> _testConfig;

        public TestWebApplicationFactory(
            string databaseConnectionString, 
            IMessageSink messageSink,
            IReadOnlyDictionary<string, string?> customSettings)
        {
            _messageSink = messageSink;
            _testConfig = new Dictionary<string, string?>
            {
                ["ConnectionStrings:BooksDatabaseConnectionString"] = databaseConnectionString,// FIXME: move setings out of this project
                //["ApiGateway:BaseUrl"] = "https://localhost:3000",
            };

            _testConfig =  new[] { _testConfig, customSettings }
                .SelectMany(d => d)
                .ToDictionary();
        }

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
                config.AddInMemoryCollection(_testConfig);
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
                    .WriteTo.TestOutput(_messageSink)
                    .CreateLogger()
                    .ForContext<TestWebApplicationFactory>();
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
}
