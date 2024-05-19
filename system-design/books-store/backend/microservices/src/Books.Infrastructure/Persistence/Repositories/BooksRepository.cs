using Books.Application.UseCases.GetBook;
using Books.Application.UseCases.GetBooks;
using Books.Domain.Books;
using Books.Infrastructure.Persistence.Models;
using Common.Extensions.Rop;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Immutable;

namespace Books.Infrastructure.Persistence.Repositories;

internal class BooksRepository : IGetBooksRepository, IGetBookByIdRepository
{
    private readonly IServiceScopeFactory scopeFactory;

    public BooksRepository(IServiceScopeFactory scopeFactory) => this.scopeFactory = scopeFactory;

    public async Task<IImmutableList<Book>> GetAsync(CancellationToken cancellationToken)
    {
        // FIXME: abstract the resolution of scoped dbcontext
        using var scope = scopeFactory.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<BooksDbContext>();

        var booksDb = await db.Books.ToListAsync(cancellationToken);
        return booksDb.Select(DbEntitiesExtensions.Map).ToImmutableList();
    }

    public async Task<Result<Book, Error>> GetAsync(BookId bookId, CancellationToken cancellationToken)
    {
        // FIXME: abstract the resolution of scoped dbcontext
        using var scope = scopeFactory.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<BooksDbContext>();

        var book = await db.Books.SingleOrDefaultAsync(x => x.Id == (long)bookId, cancellationToken);
        return book is null ? Error.NotFound : book!.Map();
    }
}
