namespace Books.API.IntegrationTests.Endpoints;

[IntegrationTests]
public sealed class GetWeather : IClassFixture<BookApiFixture>
{
    private readonly BookApiFixture _fixture;

    public GetWeather(BookApiFixture fixture, ITestOutputHelper testOutput)
    {
        _fixture = fixture;
        _fixture.TestOutput = testOutput;
    }

    [Fact]
    public async Task Get_Weather_Forecast_Should_Return_Ok()
    {
        // Arrange
        var client = _fixture.CreateClient();

        // Act
        var response = await client.GetAsync("/weatherforecast");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
