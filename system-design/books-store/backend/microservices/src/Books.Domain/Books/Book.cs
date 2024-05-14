namespace BookStore.Books.Domain.Books;

public record Book(string Title, string ISBN, Author[] Authors, Language Language);

public record Author(string FirstName, string LastName);

public enum Language
{
    English,
    Spanish,
    French
}