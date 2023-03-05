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
    [InlineData("coding")]
    public void ShouldHaveCorrectCapitalization(string input)
        => IsCapitalizationCorrect(input).Should().BeTrue();

    [Theory]
    [InlineData("compUter")]
    public void ShouldNotHaveCorrectCapitalization(string input)
        => IsCapitalizationCorrect(input).Should().BeFalse();

    private bool IsCapitalizationCorrect(string v)
    {
        return AllLettersAreCapitalized(v)
            || FirstLetterOnlyIsCapitalized(v)
            || NoLettersAreCapitalized(v);
    }

    private bool NoLettersAreCapitalized(string v)
        => v.Where(x => char.IsLower(x)).Count() == v.Length;

    private bool AllLettersAreCapitalized(string v)
        => v.Where(c => char.IsUpper(c)).Count() == v.Length;

    private bool FirstLetterOnlyIsCapitalized(string v)
        => char.IsUpper(v.First())
                && v.Skip(1).Where(c => char.IsLower(c)).Count() == v.Length - 1;
}