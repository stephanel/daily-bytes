using Books.API.IntegrationTests.TestFramework.Context;

namespace Books.API.IntegrationTests.Endpoints;

[IntegrationTests]
public sealed class GetWeather : IClassFixture<BooksWebApiFixture>
{
    private readonly BooksWebApiFixture _fixture;

    public GetWeather(BooksWebApiFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Get_Weather_Forecast_Should_Return_Ok()
        => (await _fixture.Client.GetAsync("/weatherforecast"))
            .StatusCode.Should().Be(HttpStatusCode.OK);
}