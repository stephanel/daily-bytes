using FluentAssertions;
using System.Text;
using Xunit.Sdk;

namespace ReverseString.Tests;

public class ReverseStringSpec
{
    [Fact]
    public void ShouldReverseString()
    {
        reverse("Cat").Should().Be("taC");
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