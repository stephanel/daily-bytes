using BookStore.Books.API.Endpoints.GetBooks;

namespace BookStore.Books.API.IntegrationTests.Endpoints;

public class GetBooks : IClassFixture<BookApiFixture>
{

    private readonly BookApiFixture _fixture;

    public GetBooks(BookApiFixture fixture, ITestOutputHelper testOutput)
    {
        _fixture = fixture;
        _fixture.TestOutput = testOutput;
    }

    [Fact]
    [Trait("Category", "IntegrationTests")]
    public async Task Get_Books_Should_Return_Ok()
    {
        // Arrange
        var client = _fixture.CreateClient();

        // Act
        var response = await client.GetAsync("/api/books");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var results = await response.Content.ReadFromJsonAsync<Book[]>();
        results.Should().ContainEquivalentOf(
            new Book(
                "Design Patterns: Elements of Reusable Object-Oriented Software",
                "978-0201633610",
                [
                    new("Erich", "Gamma"),
                    new("Richard", "Helm"),
                    new("Ralph", "Jonhson"),
                    new("John", "Vlissides"),
                ],
                Language.English));
        results.Should().ContainEquivalentOf(
            new Book(
                "Clean Architecture: A Craftsman's Guide to Software Structure and Design",
                "978-0134494166",
                [
                    new("Robert", "Martin")
                ],
                Language.English));
    }
}