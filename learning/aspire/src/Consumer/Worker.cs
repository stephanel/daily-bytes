using System.Text.Json;
using Confluent.Kafka;
using Consumer.Entities;
using Consumer.Persistence;

namespace Consumer;

internal sealed partial class Worker(
    IConsumer<Null, WeatherForecastDto> consumer,
    IServiceScopeFactory serviceScopeFactory,
    ILogger<Worker> logger) : BackgroundService
{
    private readonly IConsumer<Null, WeatherForecastDto> _consumer = consumer;
    private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;
    private readonly ILogger<Worker> _logger = logger;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _consumer.Subscribe("weatherforcast_topic");

        while (!stoppingToken.IsCancellationRequested)
        {
            var msg = _consumer.Consume(stoppingToken);
            var offset = msg.Offset;

            try
            {

                var payload = msg.Message.Value;
                LogConsumedMessage(offset, JsonSerializer.Serialize(payload));

                var entity = Map(payload);
            
                using var scope = _serviceScopeFactory.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<WeatherForcastHistoryDatabaseContext>();
                db.Set<WeatherForecast>().Add(entity);
                await db.SaveChangesAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                LogErroConsumingMessage(offset, ex);
            }
        }
    }
    
    private WeatherForecast Map(WeatherForecastDto dto) => new()
    {
        Date = dto.Date,
        Summary = dto.Summary,
        TemperatureC = dto.TemperatureC,
        City = dto.Location.City,
        Country = dto.Location.Country,
        CountryCode = dto.Location.CountryCode,
    };
    
    [LoggerMessage(LogLevel.Information, "Consumed message from offset {offset}... Persisting {Entity}")]
    private partial void LogConsumedMessage(long offset, string entity);
    
    
    [LoggerMessage(LogLevel.Information, "Error consuming offset {Offset}")]
    private partial void LogErroConsumingMessage(long offset, Exception exception);

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