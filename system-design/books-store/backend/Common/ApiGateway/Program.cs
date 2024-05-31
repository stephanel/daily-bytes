using Common.Extensions.API.Observability;
using Common.Extensions.DependencyInjection;
using static Common.Extensions.DependencyInjection.ServiceCollectionsExtensions;

var builder = WebApplication.CreateBuilder(args);

var apiServicesConfiguration = ApiServicesConfiguration.Default with
{
    AddFastEndpoints = false,
    CorsConfiguration = new("localhostPolicy", [
        "http://localhost:4200",
        "http://localhost:4201",
        "http://localhost:4202"
    ])
};

builder.ConfigureObservability();


builder.Services
    //.RegisterApplicationServices()
    //.RegisterInfrastructureServices()
    .RegisterApiServices(apiServicesConfiguration);

builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

app.ConfigureApi(apiServicesConfiguration);

//app.UseAuthentication();
//app.UseAuthorization();
app.MapReverseProxy();

await app.RunAsync();