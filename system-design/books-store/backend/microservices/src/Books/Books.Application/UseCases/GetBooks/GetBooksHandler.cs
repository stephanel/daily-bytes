using Books.Domain.Books;

namespace Books.Application.UseCases.GetBooks;

internal sealed class GetBooksHandler(
    IGetBooksRepository booksRepository,
    ILogger<GetBooksHandler> logger) : IRequestHandler<GetBooksRequest, Book[]>
{
    private readonly IGetBooksRepository _booksRepository = booksRepository;
    private readonly ILogger<GetBooksHandler> _logger = logger;

    public async ValueTask<Book[]> Handle(GetBooksRequest request, CancellationToken cancellationToken)
    {
        _logger.WriteGetBooksInvoked();
        return (await _booksRepository.GetAsync(cancellationToken)).ToArray();
    }
}

public record GetBooksRequest : IRequest<Book[]>;

internal static partial class Logging
{
    [LoggerMessage(
        Level = LogLevel.Information,
        Message = "Get books invoked")]
    public static partial void WriteGetBooksInvoked(this ILogger logger);
}