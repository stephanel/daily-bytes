using FluentAssertions;
using ReverseString.Application;

namespace ReverseString.Tests;

public class ReverseStringSpec
{
    [Theory]
    [InlineData("Cat", "taC")]
    [InlineData("The Daily Byte", "etyB yliaD ehT")]
    [InlineData("civic", "civic")]
    public void ShouldReverseString8StringBuilderForLoop(string input, string expected)
        => new ReverseStringStringBuilderForLoop()
            .Reverse(input)
            .Should()
            .Be(expected);

    [Theory]
    [InlineData("Cat", "taC")]
    [InlineData("The Daily Byte", "etyB yliaD ehT")]
    [InlineData("civic", "civic")]
    public void ShouldReverseString_LinqReverse(string input, string expected)
        => new ReverseStringLinqReverse()
            .Reverse(input)
            .Should()
            .Be(expected);

    [Theory]
    [InlineData("Cat", "taC")]
    [InlineData("The Daily Byte", "etyB yliaD ehT")]
    [InlineData("civic", "civic")]
    public void ShouldReverseString_SpanReverse(string input, string expected)
        => new ReverseStringSpanReverse()
            .Reverse(input)
            .Should()
            .Be(expected);

    [Theory]
    [InlineData("Cat", "taC")]
    [InlineData("The Daily Byte", "etyB yliaD ehT")]
    [InlineData("civic", "civic")]
    public void ShouldReverseString_SpanForLoop(string input, string expected)
        => new ReverseStringSpanForLoop()
            .Reverse(input)
            .Should()
            .Be(expected);
}