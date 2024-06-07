using Orders.DependencyInjection;
using Common.Extensions.API.Observability;
using Common.Extensions.DependencyInjection;
using static Common.Extensions.DependencyInjection.ServiceCollectionsExtensions;

namespace Orders.API;

public class Program
{
    protected Program() { }

    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var apiServicesConfiguration = ApiServicesConfiguration.Default with
        {
            CorsConfiguration = new("localhostPolicy", [
               "http://localhost:4200",
                "http://localhost:4201",
                "http://localhost:4202"
           ])
        };

        builder.ConfigureObservability();

        builder.Services
            .RegisterApplicationServices()
            .RegisterInfrastructureServices()
            .RegisterApiServices(apiServicesConfiguration);

        var app = builder.Build();

        app.ConfigureApi(apiServicesConfiguration);

        await app.RunAsync();
    }
}