using FluentAssertions;
using Xunit.Sdk;

namespace ValidPalindrome.Tests;

public class ValidPalindromeSpec
{
    [Theory]
    [InlineData("level", true)]
    [InlineData("algorithm", false)]
    [InlineData("A man, a plan, a canal: Panama.", true)]
    [InlineData("ABCD;!*$dcba.", true)]
    public void ShouldValidatePalindrome(string input, bool expected)
    {
        bool actual = validate(input);
        actual.Should().Be(expected);
    }

    private bool validate(string v)
    {
        var cleaned = v
            .Replace(" ", string.Empty)
            .Replace(",", string.Empty)
            .Replace(":", string.Empty)
            .Replace(".", string.Empty)
            .ToLower()
            ;
        var reverted = string.Join(string.Empty, cleaned.Reverse());
        return cleaned == reverted;
    }
}