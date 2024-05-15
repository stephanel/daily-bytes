using Books.Domain.Books;

namespace Books.Application.UseCases.GetBooks;

public record GetBooksRequest : IRequest<Book[]>;

