using FluentAssertions;
using ReverseString.Application;

namespace ReverseString.Tests;

public class ReverseStringSpec
{
    [Theory]
    [InlineData("Cat", "taC")]
    [InlineData("The Daily Byte", "etyB yliaD ehT")]
    [InlineData("civic", "civic")]
    public void ShouldReverseString(string input, string expected)
        => new ReverseStringForLoop()
            .Reverse(input)
            .Should()
            .Be(expected);

    [Theory]
    [InlineData("Cat", "taC")]
    [InlineData("The Daily Byte", "etyB yliaD ehT")]
    [InlineData("civic", "civic")]
    public void ShouldReverseStringUsingLinq(string input, string expected)
        => new ReverseStringLinq()
            .ReverseLinq(input)
            .Should()
            .Be(expected);

    [Theory]
    [InlineData("Cat", "taC")]
    [InlineData("The Daily Byte", "etyB yliaD ehT")]
    [InlineData("civic", "civic")]
    public void ShouldReverseStringUsingSpan(string input, string expected)
        => new ReverseStringSpan()
            .Reverse(input)
            .Should()
            .Be(expected);

}