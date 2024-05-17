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

        await SendErrorsAsync((int)HttpStatusCode.NotFound);

    }

    BookDto Map(Book book) => ObjectMapper.Map<BookDto>(book);
}

internal record BookRequest(long? BookId);

internal record BookDto
{
    public BookDto() { }

    public BookDto(Book book) 
    {
        Id = book.Id;
        Title = book.Title;
        ISBN = book.ISBN;
        Authors = book.Authors.Select(ObjectMapper.Map<Author, AuthorDto>).ToArray();
        Language = book.Language.Map<Language, LanguageDto>();
        ThumbnailUrl = book.ThumbnailUrl;
    }

    public long? Id { get; init; }
    public string? Title { get; init; }
    public string? ISBN { get; init; }
    public AuthorDto[] Authors { get; init; } = [];
    public LanguageDto? Language { get; init; }
    public string? ThumbnailUrl { get; init; }
}

internal record AuthorDto
{
    public AuthorDto() { }

    public AuthorDto(Author author)
    {
        FirstName = author.FirstName;
        LastName = author.LastName;
        KnownFor = author.KnownFor;
    }

    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? KnownFor { get; init; }
}

internal enum LanguageDto
{
    English,
    Spanish,
    French
}