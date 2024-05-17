using Books.Domain.Books;

namespace Books.Application.UseCases.GetBooks;

internal sealed class GetBooksHandler : IRequestHandler<GetBooksRequest, Book[]>
{
    private readonly IGetBooksRepository _booksRepository;

    public GetBooksHandler(IGetBooksRepository booksRepository)
    {
        _booksRepository = booksRepository;
    }

    public async ValueTask<Book[]> Handle(GetBooksRequest request, CancellationToken cancellationToken)
        => (await _booksRepository.GetAsync(cancellationToken)).ToArray();
}

public record GetBooksRequest : IRequest<Book[]>;