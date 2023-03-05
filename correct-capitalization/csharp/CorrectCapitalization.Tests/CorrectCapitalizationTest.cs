namespace CorrectCapitalization.Tests;

public class CorrectCapitalizationTest
{
    //"USA", return true
    //"Calvin", return true
    //"compUter", return false
    //"coding", return true

    [Fact]
    public void UsaIsCorrectCapitalization()
    {
        IsCapitalizationCorrect("USA").Should().BeTrue();
    }

    private bool IsCapitalizationCorrect(string v)
        => "USA" == v;
}