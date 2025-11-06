using System.Collections.Specialized;

namespace Functional.Exercises;

internal class _2_LookupTests
{
    [Test]
    public async Task NameValueCollection_Lookup_returns_value_when_key_exists()
    {
        NameValueCollection collection = new()
        {
            { "Key1", "Value1" },
            { "Key2", null }
        };
        await Assert.That(collection.Lookup("Key1")).IsEqualTo("Value1");
        await Assert.That(collection.Lookup("Key2")).IsEqualTo(None);
        await Assert.That(collection.Lookup("Key3")).IsEqualTo(None);
    }

    [Test]
    public async Task Dictionary_Lookup_returns_value_when_key_exists()
    {
        Dictionary<string, string> collection = new()
        {
            { "Key1", "Value1" },
        };

        await Assert.That(collection.Lookup("Key1")).IsEqualTo("Value1");
        await Assert.That(collection.Lookup("Key2")).IsEqualTo(None);
    }

    [Test]
    public async Task List_Lookup_returns_first_value_when_found()
    {
        var isOdd = (int x) => x % 2 != 0;
        List<int> values = [2, 4, 6, 7, 8];
        await Assert.That(values.Lookup(isOdd)).IsEqualTo(7);
    }

}
