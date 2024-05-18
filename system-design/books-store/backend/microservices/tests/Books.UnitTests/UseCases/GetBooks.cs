using Books.Application.UseCases.GetBooks;
using Books.Domain.Books;
using Common.TestFramework.Fakes;
using System.Collections.Immutable;

namespace Books.UnitTests.UseCases;

[UnitTests]
public class GetBooks
{
    private readonly FakeBooksRepository _booksRepository = new FakeBooksRepository();
    private readonly FakeLogger<GetBooksHandler> _logger = new();

    private readonly GetBooksHandler _handler;

    public GetBooks() => _handler = new GetBooksHandler(_booksRepository, _logger);

    [Fact]
    public async Task Should_Return_All_Persisted_Books()
    {
        _booksRepository.Seed(ExpectedBooks.GetAll());

        var results = await _handler.Handle(new(), CancellationToken.None);

        results.Should().HaveCount(2);
        results.Should().ContainEquivalentOf(ExpectedBooks.DesignPattern);
        results.Should().ContainEquivalentOf(ExpectedBooks.CleanArchitecture);
    }

    // FIXME: should we abstract? class repeats also in Books.API.IntegrationTests
    static class ExpectedBooks
    {
        public static Book[] GetAll() => [DesignPattern, CleanArchitecture];

        public static readonly Book DesignPattern = new(
            Id: 10001,
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

        public static readonly Book CleanArchitecture = new(
            Id: 10002,
            "Clean Architecture: A Craftsman's Guide to Software Structure and Design",
            "978-0134494166",
            [
                new("Robert", "Martin", "Clean Code, Agile, Software Craftsmanship")
            ],
            Language.English,
            ThumbnailUrl: "https://covers.openlibrary.org/b/id/8510059-M.jpg");
    }
}

// FIXME: abstract
internal class FakeBooksRepository : IGetBooksRepository
{
    private readonly List<Book> _books = [];

    public FakeBooksRepository(params Book[] books) => _books.AddRange(books);

    public Task<IImmutableList<Book>> GetAsync(CancellationToken cancellationToken)
        => Task.FromResult((IImmutableList<Book>)_books.ToImmutableList());

    public void Seed(params Book[] books) => _books.AddRange(books);
}