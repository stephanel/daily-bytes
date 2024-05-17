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
        results.Should().ContainEquivalentOf(ExpectedBooks.TestDrivenDevelopment);        
    }

    static class ExpectedBooks
    {
        internal static readonly BookDto DesignPatterns = new()
        {
            Id = 100001,
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
            Id = 100002,
            Title = "Clean Architecture: A Craftsman's Guide to Software Structure and Design",
            ISBN = "978-0134494166",
            Authors = [
                        new() { FirstName = "Robert", LastName = "Martin", KnownFor = "Clean Code, Agile, Software Craftsmanship" },
                ],
            Language = LanguageDto.English,
            ThumbnailUrl = "https://ia903000.us.archive.org/view_archive.php?archive=/3/items/m_covers_0008/m_covers_0008_51.tar&file=0008510059-M.jpg"
        };

        internal static readonly BookDto TestDrivenDevelopment = new()
        {
            Id = 100003,
            Title = "Test-driven development, by example",
            ISBN = "978-0321146533",
            Authors = [
                new() { FirstName = "Kent", LastName = "Beck", KnownFor = "Extreme Programming (XP), TDD" }
            ],
            Language = LanguageDto.English,
            ThumbnailUrl = "https://ia800505.us.archive.org/view_archive.php?archive=/5/items/m_covers_0012/m_covers_0012_38.zip&file=0012381947-M.jpg"
        };
    }
}