using Microsoft.Extensions.DependencyInjection;

namespace Orders.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services)
    {
        return services;
    }
}
