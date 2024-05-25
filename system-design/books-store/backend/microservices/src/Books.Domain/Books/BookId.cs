namespace Books.Domain.Books;

public record struct BookId
{
    private long Id { get; }
    private BookId(long id) => Id = id;
    public static implicit operator BookId(long id) => new BookId(id);
    public static implicit operator long(BookId bookId) => bookId.Id;

    public override string ToString() => Id.ToString();
}