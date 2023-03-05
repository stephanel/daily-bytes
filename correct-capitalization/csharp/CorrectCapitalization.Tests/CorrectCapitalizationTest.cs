namespace CorrectCapitalization.Tests;

public class CorrectCapitalizationTest
{
    //"USA", return true
    //"Calvin", return true
    //"compUter", return false
    //"coding", return true

    private readonly IRule allLettersAreCapitalized;
    private readonly IRule noLettersAreCapitalized;
    private readonly IRule firstLetterOnlyIsCapitalized;

    private readonly IRule[] rules;

    public CorrectCapitalizationTest()
    {
        allLettersAreCapitalized = new AllLettersAreCapitalized();
        noLettersAreCapitalized = new NoLettersAreCapitalized();
        firstLetterOnlyIsCapitalized = new FirstLetterOnlyIsCapitalized(
            allLettersAreCapitalized,
            noLettersAreCapitalized);

        rules = new IRule[]
        {
            allLettersAreCapitalized,
            noLettersAreCapitalized,
            firstLetterOnlyIsCapitalized
        };
    }

    [Theory]
    [InlineData("USA")]
    [InlineData("NATO")]
    [InlineData("Calvin")]
    [InlineData("coding")]
    public void ShouldHaveCorrectCapitalization(string input)
        => rules.Any(_ => _.Validate(input)).Should().BeTrue();

    [Theory]
    [InlineData("compUter")]
    public void ShouldNotHaveCorrectCapitalization(string input)
        => rules.All(_ => _.Validate(input)).Should().BeFalse();
}