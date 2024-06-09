using Common.TestFramework.Fixtures;

namespace Orders.API.IntegrationTests.TestFramework.Collections;

[CollectionDefinition(nameof(TestDependenciesCollection))]
public class TestDependenciesCollection : ICollectionFixture<MountebackFixture> { }