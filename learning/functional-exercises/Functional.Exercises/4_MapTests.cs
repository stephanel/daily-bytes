using Functional.Exercises.Features;
using static System.Linq.Enumerable;

namespace Functional.Exercises;

internal class _4_MapTests
{
    [Test]
    public async Task Map_returns_Option_result_when_success()
        => await Assert.That(Some("Alice").Map(Greet)).IsEqualTo("Hello, Alice!");

    [Test]
    public async Task Map_returns_None_result_when_failure()
    {
        Option<string> empty = None;
        await Assert.That(empty.Map(Greet)).IsEqualTo(None);
    }

    [Test]
    public async Task Map_can_make_complex_transformations()
    {
        Option<Apples> apples = Some(new Apples());

        await Assert
            .That(apples.Map(MakePie).Map(x => x.Apples))
            .IsEqualTo(apples);

        Option<Apples> empty = None;
        await Assert.That(empty.Map(MakePie)).IsEqualTo(None);
    }

    [Test]
    public async Task Map_returns_from_IEnumerable()
    {
        var triple = (int x) => x * 3;
        await Assert.That(Range(1, 3).Map(triple)).IsEquivalentTo([3, 6, 9]);
    }

    [Test]
    public async Task Map_Example1()
    {
        var subject = new Subject(Age.Create(60), Gender.Female);
        await Assert.That(RiskOf(subject)).IsEqualTo(Some(Risk.Medium));
    }

    Option<Risk> RiskOf(Subject subject)
        => subject.Age.Map(CalculateRiskProfile);

    public static Risk CalculateRiskProfile(Age age)
       => (age < 60) ? Risk.Low : Risk.Medium;

    Func<string, string> Greet = name => $"Hello, {name}!";

    Func<Apples, ApplePie> MakePie = apples => new ApplePie(apples);

    record Apples();

    record ApplePie(Apples Apples);
}
