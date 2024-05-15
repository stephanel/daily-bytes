using Microsoft.Extensions.DependencyInjection;

namespace Books.Application;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        => services.AddMediator();
}
