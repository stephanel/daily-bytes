using FastEndpoints;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Common.Extensions;

public static class ServiceCollectionsExtensions
{
    public static IServiceCollection RegisterApiServices(this IServiceCollection services)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddAuthorization();
        services.AddFastEndpoints();
        services.AddSwaggerGen();
        return services;
    }
}
