using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orders.Application.UseCases.AddItemToCurrentOrder;
using Orders.Infrastructure.ExternalServices;
using Refit;

namespace Orders.DependencyInjection;

public static class DependencyInjection
{
    private const string ApiGatewayBaseUrl= "ApiGateway:BaseUrl";

    public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IBooksService, BooksService>()
            .AddSingleton<ISessionManager, SessionManager>()
            ;

        services.AddRefitClient<IBooksApiClient>()
            .ConfigureHttpClient(x =>
            {
                x.BaseAddress = new Uri(configuration.GetValue<string>(ApiGatewayBaseUrl)!);
            });

        return services;
    }
}