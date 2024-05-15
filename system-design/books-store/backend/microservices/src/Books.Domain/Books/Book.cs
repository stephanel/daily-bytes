namespace Books.Domain.Books;

public record Book(string Title, ISBN ISBN, Author[] Authors, Language Language);

public record Author(string FirstName, string LastName);

public enum Language
{
    English,
    Spanish,
    French
}