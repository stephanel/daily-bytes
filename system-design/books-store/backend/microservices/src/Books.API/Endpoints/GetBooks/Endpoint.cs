using Books.Application.Features.GetBooks;
using BookStore.Books.API.Mappers;
using BookStore.Books.Domain.Books;

namespace BookStore.Books.API.Endpoints.GetBooks;

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

internal record BookDto
{
    public BookDto() { }

    public BookDto(Book book) 
    {
        Title = book.Title;
        ISBN = book.ISBN;
        Authors = book.Authors.Select(ObjectMapper.Map<Author, AuthorDto>).ToArray();
        Language = book.Language.Map<Language, LanguageDto>();
    }

    public string? Title { get; init; }
    public string? ISBN { get; init; }
    public AuthorDto[] Authors { get; init; } = [];
    public LanguageDto? Language { get; init; }
}

internal record AuthorDto
{
    public AuthorDto() { }

    public AuthorDto(Author author)
    {
        FirstName = author.FirstName;
        LastName = author.LastName;
    }

    public string? FirstName { get; init; }
    public string? LastName { get; init; }
}

internal enum LanguageDto
{
    English,
    Spanish,
    French
}