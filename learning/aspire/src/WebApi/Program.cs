using Bogus;
using Confluent.Kafka;
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
                { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } }, []
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
        public static LocationDto PickRandom() =>
            Locations[Random.Shared.Next(Locations.Length)];

        private static LocationDto[] Locations =>
        [
            new("Abu Dhabi", "United Arab Emirates", "AE"),
            new("Accra", "Ghana", "GH"),
            new("Addis Ababa", "Ethiopia", "ET"),
            new("Algiers", "Algeria", "DZ"),
            new("Amman", "Jordan", "JO"),
            new("Amsterdam", "Netherlands", "NL"),
            new("Ankara", "Turkey", "TR"),
            new("Antananarivo", "Madagascar", "MG"),
            new("Antwerp", "Belgium", "BE"),
            new("Athens", "Greece", "GR"),
            new("Auckland", "New Zealand", "NZ"),
            new("Baghdad", "Iraq", "IQ"),
            new("Baku", "Azerbaijan", "AZ"),
            new("Bamako", "Mali", "ML"),
            new("Bangkok", "Thailand", "TH"),
            new("Barcelona", "Spain", "ES"),
            new("Beijing", "China", "CN"),
            new("Beirut", "Lebanon", "LB"),
            new("Belgrade", "Serbia", "RS"),
            new("Belmopan", "Belize", "BZ"),
            new("Berlin", "Germany", "DE"),
            new("Bern", "Switzerland", "CH"),
            new("Bishkek", "Kyrgyzstan", "KG"),
            new("Bogotá", "Colombia", "CO"),
            new("Bratislava", "Slovakia", "SK"),
            new("Brazzaville", "Republic of the Congo", "CG"),
            new("Bridgetown", "Barbados", "BB"),
            new("Brisbane", "Australia", "AU"),
            new("Brussels", "Belgium", "BE"),
            new("Bucharest", "Romania", "RO"),
            new("Budapest", "Hungary", "HU"),
            new("Buenos Aires", "Argentina", "AR"),
            new("Cairo", "Egypt", "EG"),
            new("Calgary", "Canada", "CA"),
            new("Canberra", "Australia", "AU"),
            new("Cape Town", "South Africa", "ZA"),
            new("Caracas", "Venezuela", "VE"),
            new("Casablanca", "Morocco", "MA"),
            new("Chisinau", "Moldova", "MD"),
            new("Colombo", "Sri Lanka", "LK"),
            new("Copenhagen", "Denmark", "DK"),
            new("Cotonou", "Benin", "BJ"),
            new("Dakar", "Senegal", "SN"),
            new("Damascus", "Syria", "SY"),
            new("Dar es Salaam", "Tanzania", "TZ"),
            new("Dhaka", "Bangladesh", "BD"),
            new("Doha", "Qatar", "QA"),
            new("Dublin", "Ireland", "IE"),
            new("Durban", "South Africa", "ZA"),
            new("Edinburgh", "United Kingdom", "GB"),
            new("Frankfurt", "Germany", "DE"),
            new("Freetown", "Sierra Leone", "SL"),
            new("Gaborone", "Botswana", "BW"),
            new("Geneva", "Switzerland", "CH"),
            new("Gothenburg", "Sweden", "SE"),
            new("Guatemala City", "Guatemala", "GT"),
            new("Hanoi", "Vietnam", "VN"),
            new("Harare", "Zimbabwe", "ZW"),
            new("Havana", "Cuba", "CU"),
            new("Helsinki", "Finland", "FI"),
            new("Ho Chi Minh City", "Vietnam", "VN"),
            new("Hong Kong", "Hong Kong", "HK"),
            new("Houston", "United States", "US"),
            new("Istanbul", "Turkey", "TR"),
            new("Jakarta", "Indonesia", "ID"),
            new("Jerusalem", "Israel", "IL"),
            new("Johannesburg", "South Africa", "ZA"),
            new("Kampala", "Uganda", "UG"),
            new("Karachi", "Pakistan", "PK"),
            new("Kathmandu", "Nepal", "NP"),
            new("Khartoum", "Sudan", "SD"),
            new("Kigali", "Rwanda", "RW"),
            new("Kingston", "Jamaica", "JM"),
            new("Kinshasa", "Democratic Republic of the Congo", "CD"),
            new("Kolkata", "India", "IN"),
            new("Kuala Lumpur", "Malaysia", "MY"),
            new("Kuwait City", "Kuwait", "KW"),
            new("Kyiv", "Ukraine", "UA"),
            new("Lagos", "Nigeria", "NG"),
            new("Lahore", "Pakistan", "PK"),
            new("Lima", "Peru", "PE"),
            new("Lisbon", "Portugal", "PT"),
            new("Ljubljana", "Slovenia", "SI"),
            new("London", "United Kingdom", "GB"),
            new("Los Angeles", "United States", "US"),
            new("Luanda", "Angola", "AO"),
            new("Lusaka", "Zambia", "ZM"),
            new("Luxembourg", "Luxembourg", "LU"),
            new("Madrid", "Spain", "ES"),
            new("Managua", "Nicaragua", "NI"),
            new("Manila", "Philippines", "PH"),
            new("Maputo", "Mozambique", "MZ"),
            new("Marseille", "France", "FR"),
            new("Melbourne", "Australia", "AU"),
            new("Mexico City", "Mexico", "MX"),
            new("Milan", "Italy", "IT"),
            new("Minsk", "Belarus", "BY"),
            new("Monaco", "Monaco", "MC"),
            new("Monrovia", "Liberia", "LR"),
            new("Montevideo", "Uruguay", "UY"),
            new("Montreal", "Canada", "CA"),
            new("Moscow", "Russia", "RU"),
            new("Mumbai", "India", "IN"),
            new("Munich", "Germany", "DE"),
            new("Muscat", "Oman", "OM"),
            new("Nairobi", "Kenya", "KE"),
            new("Nassau", "Bahamas", "BS"),
            new("New Delhi", "India", "IN"),
            new("New York", "United States", "US"),
            new("Nice", "France", "FR"),
            new("Nicosia", "Cyprus", "CY"),
            new("Nouakchott", "Mauritania", "MR"),
            new("Nur-Sultan", "Kazakhstan", "KZ"),
            new("Oslo", "Norway", "NO"),
            new("Ottawa", "Canada", "CA"),
            new("Ouagadougou", "Burkina Faso", "BF"),
            new("Panama City", "Panama", "PA"),
            new("Papeete", "French Polynesia", "PF"),
            new("Paris", "France", "FR"),
            new("Perth", "Australia", "AU"),
            new("Phnom Penh", "Cambodia", "KH"),
            new("Podgorica", "Montenegro", "ME"),
            new("Prague", "Czech Republic", "CZ"),
            new("Quito", "Ecuador", "EC"),
            new("Rabat", "Morocco", "MA"),
            new("Reykjavik", "Iceland", "IS"),
            new("Riga", "Latvia", "LV"),
            new("Rio de Janeiro", "Brazil", "BR"),
            new("Riyadh", "Saudi Arabia", "SA"),
            new("Rome", "Italy", "IT"),
            new("San Jose", "Costa Rica", "CR"),
            new("San Salvador", "El Salvador", "SV"),
            new("Santiago", "Chile", "CL"),
            new("Santo Domingo", "Dominican Republic", "DO"),
            new("Sao Paulo", "Brazil", "BR"),
            new("Sarajevo", "Bosnia and Herzegovina", "BA"),
            new("Seoul", "South Korea", "KR"),
            new("Shanghai", "China", "CN"),
            new("Singapore", "Singapore", "SG"),
            new("Skopje", "North Macedonia", "MK"),
            new("Sofia", "Bulgaria", "BG"),
            new("Stockholm", "Sweden", "SE"),
            new("Suva", "Fiji", "FJ"),
            new("Sydney", "Australia", "AU"),
            new("Tallinn", "Estonia", "EE"),
            new("Tashkent", "Uzbekistan", "UZ"),
            new("Tbilisi", "Georgia", "GE"),
            new("Tehran", "Iran", "IR"),
            new("Tel Aviv", "Israel", "IL"),
            new("The Hague", "Netherlands", "NL"),
            new("Tirana", "Albania", "AL"),
            new("Tokyo", "Japan", "JP"),
            new("Toronto", "Canada", "CA"),
            new("Tripoli", "Libya", "LY"),
            new("Tunis", "Tunisia", "TN"),
            new("Ulaanbaatar", "Mongolia", "MN"),
            new("Valletta", "Malta", "MT"),
            new("Vancouver", "Canada", "CA"),
            new("Vienna", "Austria", "AT"),
            new("Vilnius", "Lithuania", "LT"),
            new("Warsaw", "Poland", "PL"),
            new("Washington", "United States", "US"),
            new("Wellington", "New Zealand", "NZ"),
            new("Windhoek", "Namibia", "NA"),
            new("Yaoundé", "Cameroon", "CM"),
            new("Yerevan", "Armenia", "AM"),
            new("Zagreb", "Croatia", "HR"),
            new("Zurich", "Switzerland", "CH")
        ];
    }
}