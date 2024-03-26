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
        var cleaned = CleanInput(input);
        return Enumerable.SequenceEqual(cleaned, cleaned.Reverse());
    }

    private bool ValidateUsingAsciiCodeForLoop(string input)
    {
        var cleaned = CleanInput(input);

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

    [Fact]
    public void ConvertToLower_Returns_LowerCharacter()
    {         
        ConvertToLower('A').Should().Be('a');
        ConvertToLower('d').Should().Be('d');
        ConvertToLower('H').Should().Be('h');
        ConvertToLower('Z').Should().Be('z');
    }

    private char[] CleanInput(string input)
        => input
        .Where(char.IsAsciiLetter)
        .Select(ConvertToLower)
        .ToArray();

    private char ConvertToLower(char c)
    {
        if ((short)c >= 65 && (short)c <= 90)   // c in [A..Z]
        {
            return (char)((short)c + 32);   // tolower
        }
        return c;
    }
}