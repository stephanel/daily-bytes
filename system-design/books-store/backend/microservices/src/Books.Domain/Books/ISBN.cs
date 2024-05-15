namespace Books.Domain.Books;

public record struct ISBN(string value)
{
    public static implicit operator string(ISBN isbn) => isbn.value;
    public static implicit operator ISBN(string value) => new ISBN(value);
}