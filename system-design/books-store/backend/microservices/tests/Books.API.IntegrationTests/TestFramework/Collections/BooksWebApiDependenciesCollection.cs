using Common.TestFramework.Fixtures;

namespace Books.API.IntegrationTests.TestFramework.Collections;

[CollectionDefinition(nameof(BooksWebApiDependenciesCollection))]
public class BooksWebApiDependenciesCollection :
    ICollectionFixture<DatabaseFixture>
{ }
