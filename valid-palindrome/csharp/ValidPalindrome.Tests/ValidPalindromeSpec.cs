using FluentAssertions;
using Xunit.Sdk;

namespace ValidPalindrome.Tests;

public class ValidPalindromeSpec
{
    //"level", return true
    //"algorithm", return false
    //"A man, a plan, a canal: Panama.", return true

    [Theory]
    [InlineData("level", true)]
    [InlineData("algorithm", false)]
    public void ShouldValidatePalindrome(string input, bool expected)
    {
        bool actual = validate(input);
        actual.Should().Be(expected);
    }

    private bool validate(string v)
    {
        return v == string.Join(string.Empty, v.Reverse());
    }
}