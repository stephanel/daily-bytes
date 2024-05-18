using Books.Domain.Books;
using Common.Extensions.Rop;

namespace Books.Application.UseCases.GetBook;

internal partial class GetBookByIdHandler(
    IGetBookByIdRepository bookRepository,
    ILogger<GetBookByIdHandler> logger)
    : IRequestHandler<GetBookByIdRequest, Result<Book, Error>>
{
    private readonly IGetBookByIdRepository _bookRepository = bookRepository;
    private readonly ILogger<GetBookByIdHandler> _logger = logger;

    public async ValueTask<Result<Book, Error>> Handle(GetBookByIdRequest request, CancellationToken cancellationToken)
    {
        _logger.WriteGetBookIdInvoked(request.BookId);
        return await _bookRepository.GetAsync(request.BookId, cancellationToken);
    }
}

public record GetBookByIdRequest(BookId BookId) : IRequest<Result<Book, Error>>;

internal static partial class Logging
{
    [LoggerMessage(
        Level = LogLevel.Information,
        Message = "Get book `{bookid}` invoked")]
    public static partial void WriteGetBookIdInvoked(this ILogger logger, BookId bookid);
}