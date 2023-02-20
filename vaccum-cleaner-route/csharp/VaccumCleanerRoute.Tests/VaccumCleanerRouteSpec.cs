namespace VaccumCleanerRoute.Tests;

public class VaccumCleanerRouteSpec
{
    //"LR", return true
    //"URURD", return false
    //"RUULLDRD", return true

    [Fact]
    public void ShouldReturnTrue_WhenCommandIsLR()
    {
        var actual = when("LR");
        actual.Should().BeTrue();
    }

    [Fact]
    public void ShouldReturnTrue_WhenCommandIsURURD()
    {
        var actual = when("URURD");
        actual.Should().BeFalse();
    }

    private bool when(string command)
    {
        return true;
    }
}