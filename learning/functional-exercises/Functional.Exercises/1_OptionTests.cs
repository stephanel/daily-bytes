namespace Functional.Exercises;

internal class _1_OptionTests
{
    [Test]
    public async Task Option_can_be_an_integer()
        => await Assert.That((Option<int>)12).IsEqualTo(12);

    [Test]
    public async Task Option_can_be_a_boolean()
        => await Assert.That((Option<bool>)true).IsEqualTo(true);

    [Test]
    public async Task Option_can_be_a_string()
        => await Assert.That((Option<string>)"a string value").IsEqualTo("a string value");

    [Test]
    public async Task Option_can_be_None()
    {
        Option<string> option = null!;
        await Assert.That(option).IsEqualTo(None);
    }

    [Test]
    public async Task Match_invoke_success_path_when_value_is_some()
        => await Assert.That(Greet(Some("Bob"))).IsEqualTo($"Hello, Bob!");

    [Test]
    public async Task Match_invoke_failure_path_when_value_is_none()
        => await Assert.That(Greet(None)).IsEqualTo($"Hello, World!");

    string Greet(Option<string> greetee)
        => greetee.Match(
            () => "Hello, World!",
            (name) => $"Hello, {name}!");
}
