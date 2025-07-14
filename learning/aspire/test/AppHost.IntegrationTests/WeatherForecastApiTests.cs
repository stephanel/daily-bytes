using AppHost.IntegrationTests.Fixtures;

namespace AppHost.IntegrationTests;

[Collection(nameof(EnvironmentSetupCollection))]
public class WeatherForecastApiTests(InfrastructureSetup infrastructure)
{
    private HttpClient Client => infrastructure.ApiClient;
    private CancellationToken CancellationToken => infrastructure.CancellationToken;

    [Fact]
    public async Task GetWeatherForecast()
    {
        // act
        var response = await Client.GetAsync("/api/weatherforecast", CancellationToken);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}