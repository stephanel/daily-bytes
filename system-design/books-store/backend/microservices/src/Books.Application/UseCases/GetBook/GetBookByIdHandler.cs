using Books.Domain.Books;
using Common.Extensions;

namespace Books.Application.UseCases.GetBook;

internal class GetBookByIdHandler : IRequestHandler<GetBookByIdRequest, Result<Book, Error>>
{
    private readonly IGetBookByIdRepository _bookRepository;

    public GetBookByIdHandler(IGetBookByIdRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async ValueTask<Result<Book, Error>> Handle(GetBookByIdRequest request, CancellationToken cancellationToken)
        => await _bookRepository.GetAsync(request.BookId, cancellationToken);
}

public record GetBookByIdRequest(BookId BookId) : IRequest<Result<Book, Error>>;