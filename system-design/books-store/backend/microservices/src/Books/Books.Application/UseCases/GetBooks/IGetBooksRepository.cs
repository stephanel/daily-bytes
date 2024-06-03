using Books.Domain.Books;
using System.Collections.Immutable;

namespace Books.Application.UseCases.GetBooks;

public interface IGetBooksRepository
{
    Task<IImmutableList<Book>> GetAsync(CancellationToken cancellationToken);
}