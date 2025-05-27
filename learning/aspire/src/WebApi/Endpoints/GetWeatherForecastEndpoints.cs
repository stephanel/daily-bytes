using Bogus;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.DTOs;

namespace WebApi.Endpoints;

internal static class GetWeatherForecastEndpoints
{
    private static readonly string[] Summaries =
        ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

    internal static async Task<WeatherForecastDto[]> Get(
        [FromServices] IProducer<Null, WeatherForecastDto> producer,
        [FromServices] ILogger<WeatherForecastDto> logger)
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecastDto
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    Summaries[Random.Shared.Next(Summaries.Length)],
                    LocationStore.PickRandom()
                ))
            .ToArray();

        var producerTasks = forecast
            .Select(value => producer.ProduceAsync("weatherforcast_topic",
                new Message<Null, WeatherForecastDto>()
                {
                    Value = value
                }));
        await Task.WhenAll(producerTasks);

        return forecast;
    }
}