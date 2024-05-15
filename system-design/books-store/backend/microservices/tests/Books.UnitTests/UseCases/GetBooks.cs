using Books.Application.UseCases.GetBooks;
using Books.Domain.Books;

namespace Books.UnitTests.UseCases;

[UnitTests]
public class GetBooks
{
    [Fact]
    public async Task Test()
    {
        var results = await new GetBooksHandler().Handle(new(), CancellationToken.None);

        results.Should().HaveCount(2);

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