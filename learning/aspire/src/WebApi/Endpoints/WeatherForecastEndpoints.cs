namespace WebApi.Endpoints;

public static class WeatherForecastEndpoints
{
    public static IEndpointRouteBuilder AddWeatherForecastEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/weatherforecast", GetWeatherForecastEndpoints.Get)
            .WithName("GetWeatherForecast")
            .WithOpenApi();

        builder.MapGet("/weatherforecast-secured", GetWeatherForecastEndpoints.Get)
            .WithName("GetWeatherForecastSecured")
            .RequireAuthorization()
            .WithOpenApi();

        builder.MapGet("/weatherforecast-role-based", GetWeatherForecastEndpoints.Get)
            .WithName("GetWeatherForecastRoleBased")
            .RequireAuthorization(policy =>
            {
                policy.RequireRole("role1");
            })
            .WithOpenApi();

        return builder;
    }
}