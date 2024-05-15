using Books.Application.UseCases.GetBooks;
using Books.Domain.Books;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Immutable;

namespace Books.Infrastructure.Persistence.Repositories;

internal class BooksRepository : IGetBooksRepository
{
    private readonly IServiceScopeFactory scopeFactory;

    public BooksRepository(IServiceScopeFactory scopeFactory) => this.scopeFactory = scopeFactory;


    public Task<IImmutableList<Book>> GetAsync(CancellationToken cancellationToken)
    {
        // FIXME: abstract the resolution of scoped dbcontext
        //using var scope = scopeFactory.CreateScope();
        //var db = scope.ServiceProvider.GetRequiredService<BooksDataContext>();
        //return db.Books.ToListAsync(cancellationToken);

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

        return Task.FromResult((IImmutableList<Book>)books.ToImmutableList());
    }
}
