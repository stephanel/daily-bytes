namespace VaccumCleanerRoute.Tests;

public class VaccumCleanerRouteSpec
{
    [Theory]
    [InlineData("LR", true)]
    [InlineData("URURD", false)]
    [InlineData("RUULLDRD", true)]
    public void ShouldReturnWetherOrNotTheVaccumCleanerReturnsToItsOriginalPosition(
        string commands, 
        bool expected)
    {
        var actual = when(commands);
        actual.Should().Be(expected);
    }

    private bool when(string commands)
    {
        var numL = countLetter(commands, 'L');
        var numR = countLetter(commands, 'R');
        var numU = countLetter(commands, 'U');
        var numD = countLetter(commands, 'D');

        if (numL == numR && numU == numD)
        {
            return true;
        }
        return false;
    }

    private int countLetter(string commands, char search)
    {
        // what is the best way to count the number of an occurence in a string?
        // for now, let's do it using LINQ
        return commands.Count(c => c.Equals(search));
    }
}