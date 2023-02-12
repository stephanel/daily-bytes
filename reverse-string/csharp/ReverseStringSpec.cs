using FluentAssertions;
using System.Text;
using Xunit.Sdk;

namespace ReverseString.Tests;

public class ReverseStringSpec
{
    [Theory]
    [InlineData("Cat", "taC")]
    [InlineData("The Daily Byte", "etyB yliaD ehT")]
    [InlineData("civic", "civic")]
    public void ShouldReverseString(string input, string expected)
    {
        reverse(input).Should().Be(expected);
    }

    [Theory]
    [InlineData("Cat", "taC")]
    [InlineData("The Daily Byte", "etyB yliaD ehT")]
    [InlineData("civic", "civic")]
    public void ShouldReverseStringUsingLinq(string input, string expected)
    {
        reverseLinq(input).Should().Be(expected);
    }

    private string reverseLinq(string input)
    {
        return string.Join(string.Empty, input.Reverse());
    }

    private string reverse(string text)
    {
        StringBuilder builder = new();
        for(int i= text.Length - 1; i >= 0; i--)
        {
            builder.Append(text[i]);
        }
        return builder.ToString();
    }
}