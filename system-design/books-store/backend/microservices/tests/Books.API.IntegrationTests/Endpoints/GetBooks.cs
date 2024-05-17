using Books.API.Endpoints.GetBooks;

namespace Books.API.IntegrationTests.Endpoints;

[IntegrationTests]
public class GetBooks : IClassFixture<BookApiFixture>
{
    private readonly BookApiFixture _fixture;

    public GetBooks(BookApiFixture fixture, ITestOutputHelper testOutput)
    {
        _fixture = fixture;
        _fixture.TestOutput = testOutput;
    }

    [Fact]
    public async Task Get_Books_Should_Return_Ok()
    {
        // Arrange
        var client = _fixture.CreateClient();

        // Act
        var response = await client.GetAsync("/api/books");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var options = new JsonSerializerOptions()
        {
            Converters = { new JsonStringEnumConverter() },
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        var results = await response.Content.ReadFromJsonAsync<BookDto[]>(options);
        results.Should().ContainEquivalentOf(ExpectedBooks.DesignPatterns);        
        results.Should().ContainEquivalentOf(ExpectedBooks.CleanArchitecture);
    }

    static class ExpectedBooks
    {
        internal static readonly BookDto DesignPatterns = new()
        {
            Id = 10001,
            Title = "Design Patterns: Elements of Reusable Object-Oriented Software",
            ISBN = "978-0201633610",
            Authors = [
                    new() { FirstName = "Erich", LastName = "Gamma", KnownFor = "Gang of Four" },
                    new() { FirstName = "Richard", LastName = "Helm", KnownFor = "Gang of Four" },
                    new() { FirstName = "Ralph", LastName = "Jonhson", KnownFor = "Gang of Four" },
                    new() { FirstName = "John", LastName = "Vlissides", KnownFor = "Gang of Four" },
                ],
            Language = LanguageDto.English,
            ThumbnailUrl = "https://covers.openlibrary.org/b/id/1754351-M.jpg"
        };

        internal static readonly BookDto CleanArchitecture = new()
        {
            Id = 10002,
            Title = "Clean Architecture: A Craftsman's Guide to Software Structure and Design",
            ISBN = "978-0134494166",
            Authors = [
                        new() { FirstName = "Robert", LastName = "Martin", KnownFor = "Clean Code, Agile, Software Craftsmanship" },
                ],
            Language = LanguageDto.English,
            ThumbnailUrl = "https://covers.openlibrary.org/b/id/8510059-M.jpg"
        };
    }
}