using Common.TestFramework.Fixtures;

namespace Orders.API.IntegrationTests.TestFramework.Collections;

[CollectionDefinition(nameof(OrdersWebApiDependenciesCollection))]
public class OrdersWebApiDependenciesCollection : 
    ICollectionFixture<MountebackFixture>, 
    ICollectionFixture<DatabaseFixture> 
{ }