using Microsoft.Extensions.DependencyInjection;

namespace Orders.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        => services.AddMediator();
}
