using Books.API.DTOs;
using Books.API.Mappers;
using Books.Application.UseCases.GetBook;
using Books.Domain.Books;
using System.Net;

namespace Books.API.Endpoints.GetBook;

internal sealed class Endpoint(IMediator mediator) : Endpoint<BookRequest>
{
    readonly IMediator _mediator = mediator;

    public override void Configure()
    {
        Get("api/books/{BookId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(BookRequest request, CancellationToken ct)
    {
        if(request.BookId is null)
        {
            await SendErrorsAsync((int)HttpStatusCode.BadRequest);
            return;
        }

        var result = await _mediator.Send(new GetBookByIdRequest((long)request.BookId), ct);

        if (result.IsSuccess)
        {
            await SendAsync(Map((Book)result), 200, ct);
            return;
        }

        await SendErrorsAsync(result.Error!.Code, ct);
    }

    BookDto Map(Book book) => ObjectMapper.Map<BookDto>(book);
}

internal record BookRequest(long? BookId);
