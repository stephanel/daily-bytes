namespace Functional.Exercises;

internal class _5_ForEachTests
{
    void Write<T>(List<T> bucket, T value) => bucket.Add(value);

    [Test]
    public async Task ForEach_with_collections()
    {
        List<int> bucket = [];

        new List<int> { 1, 2, 3 }.ForEach(bucket.Add);  // Linq method
        Enumerable.Range(4, 3).ForEach(bucket.Add); // custom ForEach method

        await Assert.That(bucket).IsEquivalentTo([1, 2, 3, 4, 5, 6]);
    }

    [Test]
    public async Task ForEach_with_option()
    {
        List<string> bucket = [];

        Some(1).Map(x => x.ToString()).ForEach(bucket.Add);
        Some("Cactus").Map(name => name.ToUpper()).ForEach(bucket.Add);
        Some("Bob").Map(name => $"Hello, {name}!").ForEach(bucket.Add);

        await Assert.That(bucket).IsEquivalentTo(["1", "CACTUS", "Hello, Bob!"]);
    }
}
