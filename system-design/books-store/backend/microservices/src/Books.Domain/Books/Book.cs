namespace Books.Domain.Books;

public record Book(BookId Id, string Title, ISBN ISBN, List<Author> Authors, Language Language, string? ThumbnailUrl = null);

public record Author(string FirstName, string LastName, string? KnownFor = null);

public enum Language
{
    English,
    Spanish,
    French
}

public record struct BookId
{
    private long Id { get; }
    private BookId(long id) => Id = id;
    public static implicit operator BookId(long id) => new BookId(id);
    public static implicit operator long(BookId bookId) => bookId.Id;
}