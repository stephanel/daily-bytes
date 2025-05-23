namespace WebApi.DTOs;

record WeatherForecastDto(
    DateOnly Date,
    int TemperatureC,
    string? Summary,
    LocationDto Location)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}