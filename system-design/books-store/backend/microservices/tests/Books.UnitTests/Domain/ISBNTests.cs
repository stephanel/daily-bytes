using BookStore.Books.Domain.Books;

namespace BookStore.Books.UnitTests.Domain;

[UnitTests]
public class ISBNTests
{
    private readonly string data = "978-3-16-148410-0";

    [Fact]
    public void ImplicitConversionToString()
    {
        var isbn = (ISBN)data;
        string isbnString = isbn;
        Assert.Equal(data, isbnString);
    }

    [Fact]
    public void ImplicitConversionToISBN()
    {
        string isbnString = data;
        ISBN isbn = isbnString;
        Assert.Equal(data, isbn);
    }
}
