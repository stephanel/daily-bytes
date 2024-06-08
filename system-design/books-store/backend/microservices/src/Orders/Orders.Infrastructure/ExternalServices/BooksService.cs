using Common.Extensions.Rop;
using Orders.Application.UseCases.AddItemToCurrentOrder;
using Refit;
using System.Net;

namespace Orders.Infrastructure.ExternalServices;

internal interface IBooksApiClient
{
    [Get("/books-service/books/{id}")]
    Task<ApiResponse<Book>> GetBookAsync(long id);
}

internal class BooksService(IBooksApiClient booksApiClient) : IBooksService
{
    readonly IBooksApiClient _booksApiClient = booksApiClient;

    public async Task<Result<Book, Error>> GetBookAsync(long Id)
    {
        var response = await _booksApiClient.GetBookAsync(Id);

        Func<ApiResponse<Book>, Result<Book, Error>> returnContent = r => r.Content!;
        Func<ApiResponse<Book>, Result<Book, Error>> returnError = r => 
            r.StatusCode == HttpStatusCode.NotFound
            ? Error.NotFound
            : Error.InternalError;

        return response.StatusCode  == HttpStatusCode.OK 
            ? returnContent(response)
            : returnError(response);
    }
}
