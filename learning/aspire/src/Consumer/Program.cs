using Aspire.Confluent.Kafka;
using Confluent.Kafka;
using Consumer;
using Consumer.Entities;
using Consumer.Persistence;
using ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddNpgsqlDbContext<WeatherForcastHistoryDatabaseContext>(connectionName: "weatherforcasthistory");

builder.AddKafkaConsumer<Null, WeatherForecastDto>("kafka",
    configureSettings => { configureSettings.Config.GroupId = "consumer_1"; },
    consumerBuilder => { consumerBuilder.SetValueDeserializer(new JsonDeserializer<WeatherForecastDto>()); });

builder.Services.AddHostedService<Worker>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // The app is in development mode
}

app.UseHttpsRedirection();

app.MapDefaultEndpoints();

await app.RunAsync();