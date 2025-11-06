using Functional.Exercises.Features;
using Pet = System.String;

namespace Functional.Exercises;

internal class _6_BindTests
{
    [Test]
    public async Task Bind_with_option()
    {
        Func<string, Option<Age>> parseAge = s => s.Parse().Bind(Age.Create);

        await Assert.That(parseAge("26")).IsEqualTo(Age.Create(26));
        await Assert.That(parseAge("NotAnAge")).IsEqualTo(None);
        await Assert.That(parseAge("180")).IsEqualTo(None);
    }

    [Test]
    public async Task Bind_with_collection()
    {
        List<Pet> johnsPets = ["Fluffy", "Thor"];
        List<Pet> carlsPets = ["Sybil"];

        List<Neighbor> neightbors = [
            new("John", johnsPets),
            new("Bob", []),
            new("Carl", carlsPets),
        ];

        IEnumerable<IEnumerable<Pet>> nested = neightbors.Map(n => n.Pets);

        List<List<Pet>> expected = [johnsPets, [], carlsPets];
        await Assert.That(nested).IsEquivalentTo(expected);

        IEnumerable<Pet> flat = neightbors.Bind(n => n.Pets);
        await Assert.That(flat).IsEquivalentTo(johnsPets.Concat(carlsPets).ToArray());
    }

    record Neighbor(string Name, IEnumerable<Pet> Pets);
}
