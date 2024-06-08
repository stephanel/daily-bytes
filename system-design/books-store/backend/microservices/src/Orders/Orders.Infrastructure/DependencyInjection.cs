using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orders.Application.UseCases.AddItemToCurrentOrder;
using Orders.Infrastructure.ExternalServices;
using Refit;
using Common.Extensions.DependencyInjection;

namespace Orders.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IBooksService, BooksService>()
            .AddSingleton<ISessionManager, SessionManager>()
            ;

        var booksServiceConfiguration = configuration.GetConfig<ApiConfiguration>(ApiConfiguration.ApiGatewaySection);
        services.AddRefitClient<IBooksApiClient>()
            .ConfigureHttpClient(x =>
            {
                x.BaseAddress = new Uri(booksServiceConfiguration.BaseUrl!);
            });

        return services;
    }
}

internal record ApiConfiguration(string BaseUrl)
{
    public const string ApiGatewaySection = "ApiGateway";
}
