using Common.TestFramework.Fixtures;
using Orders.API.IntegrationTests.TestFramework.Collections;

namespace Orders.API.IntegrationTests.TestFramework.Context;

[Collection(nameof(OrdersWebApiDependenciesCollection))]
public class OrdersWebApiFixture : WebApiFixture<Program>
{
    private readonly DatabaseFixture _databaseFixture;
    
    public MountebackFixture MountebackFixture { get; private init; }

    protected override IReadOnlyDictionary<string, string?> AppSettings => new Dictionary<string, string?>
    {
        ["ConnectionStrings:OrdersDatabaseConnectionString"] = _databaseFixture.DatabaseConnectionString,
        ["ApiGateway:BaseUrl"] = $"http://localhost:{MountebackFixture.ImpostersPort}",
    };

    public OrdersWebApiFixture(DatabaseFixture databaseFixture, MountebackFixture mountebackFixture, IMessageSink diagnosticMessageSink) 
        : base(diagnosticMessageSink)
    {
        _databaseFixture = databaseFixture;
        MountebackFixture = mountebackFixture;
    }
}
