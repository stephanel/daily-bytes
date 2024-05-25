namespace Books.Domain.Books;

public record Author(string FirstName, string LastName, string? KnownFor = null);
