using Books.API.Endpoints.GetBooks;

namespace Books.API.IntegrationTests.Endpoints;

[IntegrationTests]
public class GetBook : IClassFixture<BookApiFixture>
{
    private readonly BookApiFixture _fixture;

    public GetBook(BookApiFixture fixture, ITestOutputHelper testOutput)
    {
        _fixture = fixture;
        _fixture.TestOutput = testOutput;
    }

    [Fact]
    public async Task Get_Book_Should_Return_Ok()
    {
        // Arrange
        var client = _fixture.CreateClient();

        // Act
        var response = await client.GetAsync($"/api/books/{ExpectedBooks.DesignPatterns.Id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var options = new JsonSerializerOptions()
        {
            Converters = { new JsonStringEnumConverter() },
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        var results = await response.Content.ReadFromJsonAsync<BookDto>(options);
        results.Should().BeEquivalentTo(ExpectedBooks.DesignPatterns);   
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
    }
}