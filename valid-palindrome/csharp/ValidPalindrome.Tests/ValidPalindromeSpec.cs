using FluentAssertions;
using Xunit.Sdk;

namespace ValidPalindrome.Tests;

public class ValidPalindromeSpec
{
    //"level", return true
    //"algorithm", return false
    //"A man, a plan, a canal: Panama.", return true

    [Fact]
    public void ShouldValidatePalindrome()
    {
        bool actual = validate("level");
        actual.Should().Be(true);
    }

    private bool validate(string v)
    {
        return v == string.Join(string.Empty, v.Reverse());
    }
}