namespace Consumer.Entities;

public class WeatherForecast
{
    public int Id { get; } 
    
    public DateOnly Date { get; init; }

    public int TemperatureC { get; init; }
    
    public string? Summary { get; init; }

    public string City { get; set; }
    public string Country { get; set; }
    public string CountryCode { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}