namespace Functional.Exercises;

internal class _7_ListTests
{
    [Test]
    public async Task List_return_collections()
    {
        await Assert.That(List<string>()).IsEquivalentTo(Enumerable.Empty<string>());
        await Assert.That(List("Bob")).IsEquivalentTo(new List<string> { "Bob" });
        await Assert.That(List("Bob", "Mike")).IsEquivalentTo(new List<string> { "Bob", "Mike" });
        await Assert.That(List(1, 2, 3)).IsEquivalentTo(new List<int> { 1, 2,  3 });
    }
}
