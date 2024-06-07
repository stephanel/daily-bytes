using Orders.Application.UseCases.AddItemToCurrentOrder;

namespace Orders.Infrastructure.ExternalServices;

internal interface IBooksApiCLient
{
    // TODO: add Refit
}

internal class BooksService : IBooksService
{
    public Task<Book> GetBookAsync(long Id)
    {
        throw new NotImplementedException();
    }
}
