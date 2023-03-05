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
    public void ShouldHaveCorrectCapitalization(string input)
        => IsCapitalizationCorrect(input).Should().BeTrue();

    private bool IsCapitalizationCorrect(string v)
    {
        return v.Where(c => char.IsUpper(c)).Count() == v.Length;
    }
}