using FluentAssertions;
using System.Text.RegularExpressions;
using Xunit.Sdk;

namespace ValidPalindrome.Tests;

public class ValidPalindromeSpec
{
    [Theory]
    [InlineData("level", true)]
    [InlineData("algorithm", false)]
    [InlineData("A man, a plan, a canal: Panama.", true)]
    [InlineData("ABCD;!*$dcba.", true)]
    public void ShouldValidatePalindromeAsciiCodeLinq(string input, bool expected)
    {
        ValidateUsingAsciiCodeLinq(input).Should().Be(expected);
    }

    [Theory]
    [InlineData("level", true)]
    [InlineData("algorithm", false)]
    [InlineData("A man, a plan, a canal: Panama.", true)]
    [InlineData("ABCD;!*$dcba.", true)]
    public void ShouldValidatePalindromeAsciiCodeForLoop(string input, bool expected)
    {
        ValidateUsingAsciiCodeForLoop(input).Should().Be(expected);
    }

    [Theory]
    [InlineData("level", true)]
    [InlineData("algorithm", false)]
    [InlineData("A man, a plan, a canal: Panama.", true)]
    [InlineData("ABCD;!*$dcba.", true)]
    public void ShouldValidatePalindromeRegexForLoop(string input, bool expected)
    {
        ValidateUsingRegexForLoop(input).Should().Be(expected);
    }

    static Regex rgx = new Regex("[^a-zA-Z-]"); // to remove all except alpha characters
    private object ValidateUsingRegexForLoop(string input)
    {
        var cleaned = rgx.Replace(input, string.Empty).ToLower();
        var reverted = cleaned.Reverse();
        return Enumerable.SequenceEqual(cleaned, reverted);
    }

    private bool ValidateUsingAsciiCodeLinq(string input)
    {
        var cleaned = input
            .ToLower()
            .Where(c => (short)c >= 97 && (short)c <= 122)
            ;

        var reverted = cleaned.Reverse();
        return Enumerable.SequenceEqual(cleaned, reverted);
    }

    private bool ValidateUsingAsciiCodeForLoop(string input)
    {
        var cleaned = input
            .ToLower()
            .Where(c => (short)c >= 97 && (short)c <= 122)
            .ToArray();

        var midLength = (cleaned.Length - 1) / 2;
        for (int i = 0; i <= midLength; i++)
        {
            var j = cleaned.Length - 1 - i;
            if (cleaned[i] != cleaned[j])
            {
                return false;
            }
        }
        return true;
    }
}