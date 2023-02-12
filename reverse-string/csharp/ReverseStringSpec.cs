using FluentAssertions;
using Xunit.Sdk;

namespace ReverseString.Tests;

public class ReverseStringSpec
{
    [Fact]
    public void ShouldReverseString()
    {
        reverse("Cat").Should().Be("taC");
    }

    private string reverse(string v)
    {
        throw new NotImplementedException();
    }
}