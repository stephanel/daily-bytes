using Books.API.IntegrationTests.TestFramework.Collections;
using Books.API.IntegrationTests.TestFramework.Context;
using Common.TestFramework.TestContexts;

namespace Books.API.IntegrationTests.Endpoints;

[IntegrationTests]
[Collection(nameof(BooksWebApiDependenciesCollection))]
public class GetBooks : VerifyTestContext, IClassFixture<BooksWebApiFixture>
{
    private readonly BooksWebApiFixture _fixture;

    public GetBooks(BooksWebApiFixture fixture) : base(testOutputDirectory: "../Endpoints")
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Get_Books_Should_Return_Ok()
        => await VerifyHttpResponseMessageAsync<BookDto[]>(
            await _fixture.Client.GetAsync("/api/books"));
}