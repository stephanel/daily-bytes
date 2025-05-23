using System.Reflection;
using Bogus;
using Confluent.Kafka;
using Csv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using ServiceDefaults;
using WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddLogging();
builder.AddKafkaProducer<Null, WeatherForecastDto>("kafka",
    producerBuilder => { producerBuilder.SetValueSerializer(new JsonSerializer<WeatherForecastDto>()); });

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT", // Optional: if using JWT
        Name = "Authorization", // Name of the header
        In = ParameterLocation.Header
    });

    // Add the security requirement
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
                { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } },
            []
        }
    });
});

builder.Services.AddAuthorization();
builder.Services.AddAuthentication()
    .AddKeycloakJwtBearer("keycloak", "weatherforcast", options =>
    {
        options.RequireHttpsMetadata = false; // cause dev env
        options.Audience = "account";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapSwagger().RequireAuthorization();

app.MapGet("/weatherforecast", async ([FromServices] IProducer<Null, WeatherForecastDto> producer,
        [FromServices] ILogger<WeatherForecastDto> logger) =>
    {
        var address = new Faker().Address;
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecastDto
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)],
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
    })
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.MapGet("/weatherforecast-secured", async ([FromServices] IProducer<Null, WeatherForecastDto> producer,
        [FromServices] ILogger<WeatherForecastDto> logger) =>
    {
        var address = new Faker().Address;
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecastDto
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)],
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
    })
    .WithName("GetWeatherForecastSecured")
    .RequireAuthorization()
    .WithOpenApi();

app.MapDefaultEndpoints();


app.UseAuthentication();
app.UseAuthorization();

await app.RunAsync();

namespace WebApi
{
    record WeatherForecastDto(
        DateOnly Date,
        int TemperatureC,
        string? Summary,
        LocationDto Location)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }

    record LocationDto(string City, string Country, string CountryCode);

    internal static class LocationStore
    {
        private static readonly CsvDocument CsvDocumentCities;

        static LocationStore()
        {
            using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("WebApi.Resources.cities.csv");
            using var ms = new MemoryStream();
            stream?.CopyTo(ms);
            CsvOptions options = new()
            {
                HasHeader = true,
                Separator = SeparatorType.Semicolon
            };
            CsvDocumentCities = CsvSerializer.ConvertToDocument(ms.ToArray(), options);
        }

        public static LocationDto PickRandom()
        {
            var rows = CsvDocumentCities.Rows;
            var row = rows[Random.Shared.Next(rows.Length)];
            return new(
                City: row[1].GetValue<string>()!,
                Country: row[7].GetValue<string>()!,
                CountryCode: row[6].GetValue<string>()!
            );
        }
    }
}