using Books.Domain.Books;

namespace Books.Infrastructure.Persistence.Models;

internal static class DbEntitiesExtensions
{
    public static BookDb Map(this Book book) => new BookDb
    {
        Id = book.Id,
        Payload = new()
        {
            ISBN = book.ISBN,
            Authors = book.Authors.Select(Map).ToList(),
            Language = book.Language.ToString(),
            ThumbnailUrl = book.ThumbnailUrl
        }
    };
    public static Book Map(this BookDb book) 
    {
        Enum.TryParse(book.Payload?.Language, out Language parsedLanguage);
        return new Book(
            book.Id, 
            book.Title, 
            book.Payload!.ISBN,
            book.Payload!.Authors.Select(Map).ToList(),
            parsedLanguage, 
            book.Payload?.ThumbnailUrl);
    }

    public static AuthorDb Map(this Author author) => new AuthorDb
    {
        FirstName = author.FirstName,
        LastName = author.LastName,
        KnownFor = author.KnownFor
    };

    public static Author Map(this AuthorDb author) => new Author(author.FirstName, author.LastName, author.KnownFor);
}
