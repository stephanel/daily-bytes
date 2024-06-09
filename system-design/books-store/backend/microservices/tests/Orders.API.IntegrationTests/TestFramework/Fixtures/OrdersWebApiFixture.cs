using Common.TestFramework.Fixtures;
using Orders.API.IntegrationTests.TestFramework.Collections;

namespace Orders.API.IntegrationTests.TestFramework.Context;

[Collection(nameof(TestDependenciesCollection))]
public class OrdersWebApiFixture : WebApiFixture<Program>
{
    private readonly MountebackFixture _mountebackFixture;

    protected override IReadOnlyDictionary<string, string?> CustomSettings => new Dictionary<string, string?>
    {
        // FIXME: move setings out of this project
        ["ApiGateway:BaseUrl"] = $"http://localhost:{_mountebackFixture.ImpostersPort}",
    };

    public OrdersWebApiFixture(MountebackFixture mountebackFixture, IMessageSink diagnosticMessageSink) 
        : base(diagnosticMessageSink)
    {
        _mountebackFixture = mountebackFixture;
    }

    protected override async Task RunDatabaseMigration() => await Task.CompletedTask;

    public Task StubEndpointAsync<T>(string endpoint, T data) where T : class
        => _mountebackFixture.StubGetEndpointAsync(endpoint, data);
}
