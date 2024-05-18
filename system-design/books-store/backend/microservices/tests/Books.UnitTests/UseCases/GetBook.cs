using Books.Application.UseCases.GetBook;
using Books.Domain.Books;
using Common.Extensions.Rop;

namespace Books.UnitTests.UseCases;

[UnitTests]
public class GetBook
{
    private readonly FakeBookRepository _booksRepository = new FakeBookRepository();
    private readonly GetBookByIdHandler _handler;

    public GetBook() => _handler = new GetBookByIdHandler(_booksRepository);

    [Fact]
    public async Task Should_Return_All_Persisted_Books()
    {
        _booksRepository.Seed(ExpectedBooks.DesignPattern);

        var result = await _handler.Handle(new(ExpectedBooks.DesignPattern.Id), CancellationToken.None);

        result.IsSuccess.Should().BeTrue();

        var book = (Book)result;
        book.Should().BeEquivalentTo(ExpectedBooks.DesignPattern);
    }

    // FIXME: should we abstract? class repeats also in Books.API.IntegrationTests
    static class ExpectedBooks
    {
        public static readonly Book DesignPattern = new(
            Id: 100001,
            "Design Patterns: Elements of Reusable Object-Oriented Software",
            "978-0201633610",
            [
                new("Erich", "Gamma", "Gang of Four"),
                new("Richard", "Helm", "Gang of Four"),
                new("Ralph", "Jonhson", "Gang of Four"),
                new("John", "Vlissides", "Gang of Four"),
            ],
            Language.English,
            ThumbnailUrl: "https://covers.openlibrary.org/b/id/1754351-M.jpg");        
    }
}

// FIXME: abstract
internal class FakeBookRepository : IGetBookByIdRepository
{
    private readonly List<Book> _books = [];

    public FakeBookRepository(params Book[] books) => _books.AddRange(books);

    public Task<Result<Book, Error>> GetAsync(BookId bookId, CancellationToken cancellationToken)
    {
        var book = _books.Find(x => x.Id == bookId);
        Result<Book, Error> result = book ?? (Result<Book, Error>)new Error("NotFound");
        return Task.FromResult(result);
    }

    public void Seed(params Book[] books) => _books.AddRange(books);
}