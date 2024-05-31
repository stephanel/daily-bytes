using Common.Extensions.API.Observability;
using Common.Extensions.DependencyInjection;

namespace Auth.WebApi;

public class Program
{
    protected Program() { }

    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var apiServicesConfiguration = ApiServicesConfiguration.Default with
        {
            AddFastEndpoints = false
        };

        builder.ConfigureObservability();

        builder.Services
            //.RegisterApplicationServices()
            //.RegisterInfrastructureServices()
            .RegisterApiServices(apiServicesConfiguration);

        var app = builder.Build();

        app.ConfigureApi(apiServicesConfiguration);

        await app.RunAsync();
    }
}