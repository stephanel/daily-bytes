using Books.Application.UseCases.GetBooks;
using Books.API.Mappers;
using Books.Domain.Books;
using Books.API.DTOs;

namespace Books.API.Endpoints.GetBooks;

internal sealed class Endpoint(IMediator mediator) : EndpointWithoutRequest
{
    readonly IMediator _mediator = mediator;

    public override void Configure()
    {
        Get("api/books");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var books = await _mediator.Send(new GetBooksRequest(), ct);
        await SendAsync(Map(books), 200, ct);
    }

    BookDto[] Map(Book[] books) => books.Select(ObjectMapper.Map<Book, BookDto>).ToArray();
}