using Microsoft.Extensions.DependencyInjection;
using Orders.Application.UseCases.AddItemToCurrentOrder;
using Orders.Infrastructure.ExternalServices;

namespace Orders.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services)
    {
        services.AddSingleton<IBooksService, BooksService>()
            .AddSingleton<ISessionManager, SessionManager>()
            ;

        return services;
    }
}
