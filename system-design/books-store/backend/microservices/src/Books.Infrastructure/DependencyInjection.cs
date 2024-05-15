using Books.Application.UseCases.GetBooks;
using Books.Infrastructure.Persistence;
using Books.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Books.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services)
    {
        return services
            .AddDbContext<BooksDbContext>()
            .AddSingleton<IGetBooksRepository, BooksRepository>();
    }
}
