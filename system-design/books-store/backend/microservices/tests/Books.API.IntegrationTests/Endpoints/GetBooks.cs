namespace Books.API.IntegrationTests.Endpoints;

[IntegrationTests]
public class GetBooks : VerifyTestContext, IClassFixture<BookApiFixture>
{
    private readonly BookApiFixture _fixture;
    private readonly HttpClient _client;

    public GetBooks(BookApiFixture fixture, ITestOutputHelper testOutput)
        : base(testDirectory: "Endpoints")
    {
        _fixture = fixture;
        _fixture.TestOutput = testOutput;
        _client = _fixture.CreateClient();
    }

    [Fact]
    public async Task Get_Books_Should_Return_Ok()
        => await VerifyHttpResponseMessageAsync<BookDto[]>(
            await _client.GetAsync("/api/books"));
}