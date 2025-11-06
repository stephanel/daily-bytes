namespace Functional.Exercises;

internal class _8_WhereTests
{
    [Test]
    public async Task Where_with_option()
    {
        await Assert.That(Some(2).Where(IsNatural)).IsEqualTo(2);
        await Assert.That(Some(-2).Where(IsNatural)).IsEqualTo(None);

        await Assert.That(ToNatural("2")).IsEqualTo(2);
        await Assert.That(ToNatural("-2")).IsEqualTo(None);
        await Assert.That(ToNatural("hello")).IsEqualTo(None);
    }

    bool IsNatural(int value) => value >= 0;

    Option<int> ToNatural(string s) => Int.Parse(s).Where(IsNatural); 
}
