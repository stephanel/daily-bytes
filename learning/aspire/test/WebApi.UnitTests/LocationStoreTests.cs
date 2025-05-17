namespace WebApi.UnitTests;

public class LocationStoreTests
{
    [Fact]
    public void PickRandom_ReturnsRandomLocation()
    {
        var location = LocationStore.PickRandom();
        Assert.NotNull(location);
        Assert.NotNull(location.City);
        Assert.NotNull(location.Country);
        Assert.NotNull(location.CountryCode);
    }
}