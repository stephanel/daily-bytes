using Books.Application.UseCases.GetBooks;
using Books.Domain.Books;
using System.Collections.Immutable;

namespace Books.UnitTests.UseCases;

[UnitTests]
public class GetBooks
{
    private readonly FakeBooksRepository _booksRepository = new FakeBooksRepository();
    private readonly GetBooksHandler _handler;

    public GetBooks() => _handler = new GetBooksHandler(_booksRepository);

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
            "Design Patterns: Elements of Reusable Object-Oriented Software",
            "978-0201633610",
            [
                new("Erich", "Gamma"),
                new("Richard", "Helm"),
                new("Ralph", "Jonhson"),
                new("John", "Vlissides"),
            ],
            Language.English);

        public static readonly Book CleanArchitecture = new(
            "Clean Architecture: A Craftsman's Guide to Software Structure and Design",
            "978-0134494166",
            [
                new("Robert", "Martin")
            ],
            Language.English);
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