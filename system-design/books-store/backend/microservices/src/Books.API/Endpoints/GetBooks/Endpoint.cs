using Books.Application.Features.GetBooks;
using BookStore.Books.Domain.Books;

namespace BookStore.Books.API.Endpoints.GetBooks;

internal sealed class Endpoint : EndpointWithoutRequest
{
    private readonly IMediator _mediator;

    public Endpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

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

    private BookDto[] Map(Book[] books)
        => books.Select(Map).ToArray();

    private BookDto Map(Book book) => new(book.Title, book.ISBN, Map(book.Authors), Map(book.Language));

    private AuthorDto[] Map(Author[] authors)
        => authors.Select(Map).ToArray();

    private AuthorDto Map(Author author)
        => new(author.FirstName, author.LastName);

    private LanguageDto Map(Language language)
        => language switch
        {
            Language.English => LanguageDto.English,
            Language.Spanish => LanguageDto.Spanish,
            Language.French => LanguageDto.French,
            _ => throw new NotSupportedException()
        };
}

internal record BookDto(string Title, string ISBN, AuthorDto[] Authors, LanguageDto Language);
//{
//    internal BookDto(Book book)
//         : this(book.Title, Map(book.Authors), book.Language.As<LanguageDto>())
//    { }

//    private AuthorDto[] Map(Author[] author) => author.Select(Map).ToArray();

//    private AuthorDto Map(Author author) => new(author);

//    private LanguageDto Map(Language language)
//        => language switch
//        {
//            0 => LanguageDto.English,
//            //Language.English => LanguageDto.English,
//            //Language.Spanish => LanguageDto.Spanish,
//            //Language.French => LanguageDto.French,
//            _ => throw new NotSupportedException()
//        };
//}

internal record AuthorDto(string FirstName, string LastName);
//{
//    internal AuthorDto(Author author)
//        : this(author.FirstName, author.LastName)
//    { }
//}

internal enum LanguageDto
{
    English,
    Spanish,
    French
}