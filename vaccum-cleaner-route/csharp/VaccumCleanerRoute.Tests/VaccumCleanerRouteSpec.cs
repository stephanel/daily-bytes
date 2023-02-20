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


    private bool when(string command)
    {
        return true;
    }
}