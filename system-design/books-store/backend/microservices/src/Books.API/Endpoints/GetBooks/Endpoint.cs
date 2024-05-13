using FastEndpoints;

namespace BookStore.Books.API.Endpoints.GetBooks;

internal class Endpoint : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("api/books");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        Book[] books = [
            new Book(
                "Design Patterns: Elements of Reusable Object-Oriented Software",
                "978-0201633610",
                [
                    new("Erich", "Gamma"),
                    new("Richard", "Helm"),
                    new("Ralph", "Jonhson"),
                    new("John", "Vlissides"),
                ],
                Language.English),
            new Book(
                "Clean Architecture: A Craftsman's Guide to Software Structure and Design",
                "978-0134494166",
                [
                    new("Robert", "Martin")
                ],
                Language.English)
            ];

        await SendAsync(books, 200, ct);
    }
}

internal record Book(string Title, string ISBN, Author[] Authors, Language Language);

internal record Author(string FirstName, string LastName);

enum Language
{
    English,
    Spanish,
    French
}
