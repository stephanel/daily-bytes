using Functional.Exercises.Features;
using TUnit.Assertions.Extensions;
using Pet = System.String;

namespace Functional.Exercises;

internal class _6_BindTests
{
    Func<string, Option<Age>> parseAge = s => s.Parse().Bind(Age.Create);

    [Test]
    public async Task Bind_with_option()
    {
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

    [Test]
    public async Task Bind_flatten_IEnumerable()
    {
        IEnumerable<Subject> Population = [
            new Subject(Age.Create(20), Gender.Male),
            new Subject(None, None),
            new Subject(Age.Create(22), Gender.Male),
        ];


        var optionalAges = Population.Map(x => x.Age);

        await Assert.That(optionalAges).IsEquivalentTo(
            [Age.Create(20), None, Age.Create(22)]);

        IEnumerable<Age> stagedAges = Population.Bind(x => x.Age);

        await Assert.That(stagedAges).IsEquivalentTo(
            [.. Age.Create(20).AsEnumerable(), .. Age.Create(22).AsEnumerable()]);

        await Assert.That(stagedAges.Map(x => x.Value).Average()).IsEqualTo(21);
    }

    record Neighbor(string Name, IEnumerable<Pet> Pets);
}
