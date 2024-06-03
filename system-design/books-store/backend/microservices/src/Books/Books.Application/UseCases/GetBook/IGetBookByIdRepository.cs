using Books.Domain.Books;
using Common.Extensions.Rop;

namespace Books.Application.UseCases.GetBook;

public interface IGetBookByIdRepository
{
    Task<Result<Book, Error>> GetAsync(BookId bookId, CancellationToken cancellationToken);
}