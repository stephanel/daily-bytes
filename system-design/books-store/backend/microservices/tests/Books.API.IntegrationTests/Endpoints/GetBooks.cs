using BookStore.Books.API.Endpoints.GetBooks;

namespace BookStore.Books.API.IntegrationTests.Endpoints;

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
            Title = "Design Patterns: Elements of Reusable Object-Oriented Software",
            ISBN = "978-0201633610",
            Authors = [
                    new() { FirstName = "Erich", LastName = "Gamma" },
                    new() { FirstName = "Richard", LastName = "Helm" },
                    new() { FirstName = "Ralph", LastName = "Jonhson" },
                    new() { FirstName = "John", LastName = "Vlissides" },
                ],
            Language = LanguageDto.English
        };

        internal static readonly BookDto CleanArchitecture = new()
        {
            Title = "Clean Architecture: A Craftsman's Guide to Software Structure and Design",
            ISBN = "978-0134494166",
            Authors = [
                        new() { FirstName = "Robert", LastName = "Martin" },
                ],
            Language = LanguageDto.English
        };
    }
}