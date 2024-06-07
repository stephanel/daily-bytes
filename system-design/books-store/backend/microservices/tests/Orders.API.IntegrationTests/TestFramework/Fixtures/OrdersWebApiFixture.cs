using Common.TestFramework.Fixtures;

namespace Orders.API.IntegrationTests.TestFramework.Context;

public class OrdersWebApiFixture : WebApiFixture<Program>
{
    public OrdersWebApiFixture(IMessageSink diagnosticMessageSink) : base(diagnosticMessageSink)
    { }

    protected override async Task RunDatabaseMigration() => await Task.CompletedTask;
}
