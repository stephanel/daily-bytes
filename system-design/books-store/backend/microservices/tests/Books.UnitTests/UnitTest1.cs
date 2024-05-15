using Common.TestFramework.TestMetadata.Traits;

namespace Books.UnitTests;

public class UnitTest1
{
    [Fact]
    [UnitTest]
    public void Test1()
    {
        true.Should().BeTrue();
    }
}