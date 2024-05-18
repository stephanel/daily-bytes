using Books.Application.UseCases.GetBook;
using Books.Application.UseCases.GetBooks;
using Books.Domain.Books;
using Common.Extensions.Rop;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Immutable;

namespace Books.Infrastructure.Persistence.Repositories;

internal class BooksRepository : IGetBooksRepository, IGetBookByIdRepository
{
    private readonly IServiceScopeFactory scopeFactory;

    public BooksRepository(IServiceScopeFactory scopeFactory) => this.scopeFactory = scopeFactory;


    public Task<IImmutableList<Book>> GetAsync(CancellationToken cancellationToken)
    {
        // FIXME: abstract the resolution of scoped dbcontext
        //using var scope = scopeFactory.CreateScope();
        //var db = scope.ServiceProvider.GetRequiredService<BooksDataContext>();
        //return db.Books.ToListAsync(cancellationToken);

        Book[] books = _books.ToArray();
        return Task.FromResult((IImmutableList<Book>)books.ToImmutableList());
    }

    public Task<Result<Book, Error>> GetAsync(BookId bookId, CancellationToken cancellationToken)
    {
        // FIXME: abstract the resolution of scoped dbcontext
        //using var scope = scopeFactory.CreateScope();
        //var db = scope.ServiceProvider.GetRequiredService<BooksDataContext>();
        //return db.Books.FindAsync(bookId, cancellationToken);

        Book? book = _books.Find(x => x.Id == bookId);
        Result<Book, Error> result = book is null ? new Error("Book not found") : book!;
        return Task.FromResult(result);
    }

    private readonly List<Book> _books = [
           new Book(
               100001,
                "Design Patterns: Elements of Reusable Object-Oriented Software",
                "978-0201633610",
                [
                    new("Erich", "Gamma", KnownFor: "Gang of Four"),
                    new("Richard", "Helm", KnownFor: "Gang of Four"),
                    new("Ralph", "Jonhson", KnownFor: "Gang of Four"),
                    new("John", "Vlissides", KnownFor: "Gang of Four"),
                ],
                Language.English,
                ThumbnailUrl: "https://covers.openlibrary.org/b/id/1754351-M.jpg"),
            new Book(
               100002,
                "Clean Architecture: A Craftsman's Guide to Software Structure and Design",
                "978-0134494166",
                [
                    new("Robert", "Martin", KnownFor: "Clean Code, Agile, Software Craftsmanship")
                ],
                Language.English,
                ThumbnailUrl: "https://ia903000.us.archive.org/view_archive.php?archive=/3/items/m_covers_0008/m_covers_0008_51.tar&file=0008510059-M.jpg"),
            new Book(
                100003,
               "Test-driven development, by example",
                "978-0321146533",
                [
                    new("Kent", "Beck", KnownFor: "Extreme Programming (XP), TDD")
                ],
                Language.English,
                ThumbnailUrl: "https://ia800505.us.archive.org/view_archive.php?archive=/5/items/m_covers_0012/m_covers_0012_38.zip&file=0012381947-M.jpg")
           ];
}
