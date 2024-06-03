using Books.API.Mappers;
using Books.Domain.Books;

namespace Books.API.DTOs;

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