namespace Books.Domain.Books;

public record Book(BookId Id, string Title, ISBN ISBN, List<Author> Authors, Language Language, string? ThumbnailUrl = null);