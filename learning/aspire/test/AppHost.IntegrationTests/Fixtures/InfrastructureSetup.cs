using Aspire.Hosting;
using Microsoft.Extensions.Logging;

// ReSharper disable once ClassNeverInstantiated.Global

namespace AppHost.IntegrationTests.Fixtures;

[CollectionDefinition(nameof(EnvironmentSetupCollection))]
public class EnvironmentSetupCollection : ICollectionFixture<InfrastructureSetup>
{
}

public sealed class InfrastructureSetup : IAsyncLifetime
{
    private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(60);

    private IDistributedApplicationTestingBuilder? _appHost;
    private DistributedApplication? _app;

    private const string WebApiServiceName = "apiservice";

    public HttpClient ApiClient { get; private set; } = null!;
    public CancellationToken CancellationToken { get; } = new CancellationTokenSource(DefaultTimeout).Token;

    public async Task InitializeAsync()
    {
        _appHost = await DistributedApplicationTestingBuilder.CreateAsync<Projects.AppHost>(CancellationToken);

        _appHost.Services.AddLogging(logging =>
        {
            logging.SetMinimumLevel(LogLevel.Debug);
            logging.AddFilter(_appHost.Environment.ApplicationName, LogLevel.Debug);
            logging.AddFilter("Aspire.", LogLevel.Debug);
        });

        _appHost.Services.ConfigureHttpClientDefaults(clientBuilder =>
        {
            clientBuilder.AddStandardResilienceHandler();
        });

        _app = await _appHost.BuildAsync(CancellationToken).WaitAsync(DefaultTimeout, CancellationToken);

        await _app.StartAsync(CancellationToken).WaitAsync(DefaultTimeout, CancellationToken);

        await _app.ResourceNotifications.WaitForResourceHealthyAsync(WebApiServiceName, CancellationToken)
            .WaitAsync(DefaultTimeout, CancellationToken);

        ApiClient = _app!.CreateHttpClient(WebApiServiceName);
    }

    public async Task DisposeAsync()
    {
        if (_app is not null)
        {
            await _app.DisposeAsync();
        }

        if (_appHost is not null)
        {
            await _appHost.DisposeAsync();
        }
    }
}