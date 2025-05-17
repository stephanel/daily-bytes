using ConsoleApp1;
using Refit;

var service = RestService.For<IWeatherForecastService>("http://localhost:5087");

CancellationTokenSource cts = new();
try
{
    var token = cts.Token;

    Console.CancelKeyPress += (object? _, ConsoleCancelEventArgs e) =>
    {
        e.Cancel = true;
        cts.Cancel();
    };
    
    while (!token.IsCancellationRequested)
    {
        await service.GetWeatherForecastsAsync();
        await Task.Delay(1, token);
    }

    Console.WriteLine("Harassing API");
}
finally
{
    cts.Dispose();
}

namespace ConsoleApp1
{
    internal interface IWeatherForecastService
    {
        [Get("/weatherforecast")]
        Task<IEnumerable<WeatherForecastDto>> GetWeatherForecastsAsync();
    }


    record WeatherForecastDto(
        DateOnly Date,
        int TemperatureC,
        string? Summary,
        LocationDto Location)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }

    record LocationDto(string City, string Country, string CountryCode);
}