namespace WebApi.Endpoints;

public static class WeatherForecastEndpoints
{
    public static IEndpointRouteBuilder AddWeatherForecastEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/weatherforecast", GetWeatherForecastEndpoints.Get)
            .WithName("GetWeatherForecast")
            .WithOpenApi();

        builder.MapGet("/weatherforecast-secured", GetWeatherForecastEndpoints.GetSecured)
            .WithName("GetWeatherForecastSecured")
            .RequireAuthorization()
            .WithOpenApi();

        return builder;
    }
}