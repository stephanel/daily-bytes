namespace CorrectCapitalization.Tests;

public class CorrectCapitalizationTest
{
    //"USA", return true
    //"Calvin", return true
    //"compUter", return false
    //"coding", return true

    [Theory]
    [InlineData("USA")]
    [InlineData("NATO")]
    [InlineData("Calvin")]
    public void ShouldHaveCorrectCapitalization(string input)
        => IsCapitalizationCorrect(input).Should().BeTrue();

    private bool IsCapitalizationCorrect(string v)
    {
        return v.Where(c => char.IsUpper(c)).Count() == v.Length
            || (char.IsUpper(v.First())
                && v.Skip(1).Where(c => char.IsLower(c)).Count() == v.Length-1);
    }
}