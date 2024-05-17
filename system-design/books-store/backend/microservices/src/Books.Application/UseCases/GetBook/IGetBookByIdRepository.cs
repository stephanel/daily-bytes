using Books.Domain.Books;
using Common.Extensions;

namespace Books.Application.UseCases.GetBook;

public interface IGetBookByIdRepository
{
    Task<Result<Book, Error>> GetAsync(BookId bookId, CancellationToken cancellationToken);
}