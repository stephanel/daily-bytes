using ValidPalindrome.Application;
using ValidPalindrome.Benchmarks;

namespace ValidPalindrome.Tests;

public class ValidPalindromeSpec
{
    private readonly Palindrome _palindrome = new();

    [Theory]
    [InlineData("level", true)]
    [InlineData("algorithm", false)]
    [InlineData("A man, a plan, a canal: Panama.", true)]
    [InlineData("ABCD;!*$dcba.", true)]
    public void ShouldValidatePalindromeAsciiCodeLinq(string input, bool expected)
    {
        _palindrome.ValidateUsingAsciiCodeLinq(input).Should().Be(expected);
    }

    [Theory]
    [InlineData("level", true)]
    [InlineData("algorithm", false)]
    [InlineData("A man, a plan, a canal: Panama.", true)]
    [InlineData("ABCD;!*$dcba.", true)]
    public void ShouldValidatePalindromeAsciiCodeForLoop(string input, bool expected)
    {
        _palindrome.ValidateUsingAsciiCodeForLoop(input).Should().Be(expected);
    }

    [Theory]
    [InlineData("level", true)]
    [InlineData("algorithm", false)]
    [InlineData("A man, a plan, a canal: Panama.", true)]
    [InlineData("ABCD;!*$dcba.", true)]
    public void ShouldValidatePalindromeRegexForLoop(string input, bool expected)
    {
        _palindrome.ValidateUsingRegexForLoop(input).Should().Be(expected);
    }


    [Theory]
    [InlineData("level", true)]
    [InlineData("algorithm", false)]
    [InlineData("A man, a plan, a canal: Panama.", true)]
    [InlineData("ABCD;!*$dcba.", true)]
    public void ShouldValidateUsingAsciiCodeForLoopAlternativeLogic(string input, bool expected)
    {
        _palindrome.ValidateUsingAsciiCodeForLoopAlternativeLogic(input).Should().Be(expected);
    }


    [Fact]
    public void ConvertToLower_Returns_LowerCharacter()
    {         
        ConvertToLower('A').Should().Be('a');
        ConvertToLower('d').Should().Be('d');
        ConvertToLower('H').Should().Be('h');
        ConvertToLower('Z').Should().Be('z');
    }

    [Fact]
    public void PalindromeGeneratorShouldGenerate()
    {
        var input = new PalindromeGenerator().GenerateRandomPalindrome(10);
        _palindrome.ValidateUsingAsciiCodeLinq(input).Should().BeTrue();
        _palindrome.ValidateUsingAsciiCodeForLoop(input).Should().BeTrue();
        _palindrome.ValidateUsingRegexForLoop(input).Should().BeTrue();
        _palindrome.ValidateUsingAsciiCodeForLoopAlternativeLogic(input).Should().BeTrue();
    }

    private object ConvertToLower(char v) => _palindrome.ConvertToLower(v);
}