using BookStore.Common.TestFramework.TestMetadata.Traits;

namespace Books.UnitTests;

[UnitTests]
public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        true.Should().BeTrue();
    }
}