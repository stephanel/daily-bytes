using Books.API.IntegrationTests.TestFramework.Context;
using Common.TestFramework.TestContexts;

namespace Books.API.IntegrationTests.Endpoints;

[IntegrationTests]
public class GetBook : VerifyTestContext, IClassFixture<BooksWebApiFixture>
{
    private readonly BooksWebApiFixture _fixture;

    public GetBook(BooksWebApiFixture fixture) : base(testOutputDirectory: "../Endpoints")
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Get_Book_Should_Return_Ok_When_Requested_Book_Exists()
        => await VerifyHttpResponseMessageAsync<BookDto>(
            await _fixture.Client.GetAsync($"/api/books/1"));

    [Fact]
    public async Task Get_Book_Should_Return_NotFound_When_Requested_Book_Does_Not_Exists()
        => await VerifyHttpResponseMessageAsync<BookDto>(
            await _fixture.Client.GetAsync($"/api/books/123"));
}