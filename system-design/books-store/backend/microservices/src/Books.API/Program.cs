using Books.DependencyInjection;
using Common.Extensions.API.Observability;
using Common.Extensions.Configuration;
using Common.Extensions.DependencyInjection;

namespace Books.API;

public class Program
{
    protected Program() { }

    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        CORSConfiguration corsConfiguration = new("localhostPolicy", ["http://localhost:4200"]);

        builder.ConfigureObservability();

        builder.Services
            .RegisterApplicationServices()
            .RegisterInfrastructureServices()
            .RegisterApiServices(corsConfiguration);

        var app = builder.Build();

        app.ConfigureApi(corsConfiguration);

        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        app.MapGet("/weatherforecast", () =>
        {
            var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
                .ToArray();

            return forecast;
        })
        .WithName("GetWeatherForecast")
        .WithOpenApi();

        await app.RunAsync();
    }
}


internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}