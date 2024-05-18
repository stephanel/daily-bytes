namespace Books.API.IntegrationTests.Endpoints;

[IntegrationTests]
public class GetBook : VerifyTestContext, IClassFixture<BookApiFixture>
{
    private readonly BookApiFixture _fixture;
    private readonly HttpClient _client;

    public GetBook(BookApiFixture fixture, ITestOutputHelper testOutput)
        : base(testDirectory: "Endpoints")
    {
        _fixture = fixture;
        _fixture.TestOutput = testOutput;
        _client = _fixture.CreateClient();
    }

    [Fact]
    public async Task Get_Book_Should_Return_Ok_When_Requested_Book_Exists()
        => await VerifyHttpResponseMessageAsync<BookDto>(
            await _client.GetAsync($"/api/books/100001"));

    [Fact]
    public async Task Get_Book_Should_Return_NotFound_When_Requested_Book_Does_Not_Exists()
        => await VerifyHttpResponseMessageAsync<BookDto>(
            await _client.GetAsync($"/api/books/123"));
}