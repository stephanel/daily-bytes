using Books.Domain.Books;

namespace Books.Application.UseCases.GetBooks;

internal sealed class GetBooksHandler : IRequestHandler<GetBooksRequest, Book[]>
{
    public ValueTask<Book[]> Handle(GetBooksRequest request, CancellationToken cancellationToken)
    {
        // FIXME: This is a fake implementation. You should replace this a real data source.
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

        return ValueTask.FromResult(books);
    }
}

public record GetBooksRequest : IRequest<Book[]>;

