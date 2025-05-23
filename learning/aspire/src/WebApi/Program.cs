using Bogus;
using Confluent.Kafka;
using Microsoft.OpenApi.Models;
using ServiceDefaults;
using WebApi;
using WebApi.DTOs;
using WebApi.Endpoints;

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

app.MapSwagger().RequireAuthorization();

app.MapGroup("/api").AddWeatherForecastEndpoints();

app.MapDefaultEndpoints();

app.UseAuthentication();
app.UseAuthorization();

await app.RunAsync();