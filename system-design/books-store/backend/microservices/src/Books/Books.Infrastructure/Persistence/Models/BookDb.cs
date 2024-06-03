namespace Books.Infrastructure.Persistence.Models;

internal record BookDb
{ 
    public long Id { get; init; }
    public string Title { get; init; } = null!;
    public BookPayload? Payload { get; init; }

    public class BookPayload
    {
        public string ISBN { get; init; } = null!;
        public List<AuthorDb> Authors { get; init; } = [];
        public string Language { get; init; } = null!;
        public string? ThumbnailUrl { get; init; }
    }
}

internal class AuthorDb
{
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string? KnownFor { get; init; }
}