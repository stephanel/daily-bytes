using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Extensions.DependencyInjection;

public static class ServiceCollectionsExtensions
{
    public static IServiceCollection RegisterApiServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer()
            .AddAuthorization()
            .AddFastEndpoints()
            .AddSwagger();

        return services;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.SwaggerDocument();
        return services;
    }
}
