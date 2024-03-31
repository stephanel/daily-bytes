namespace RopCSharp.Tests;

public class EmailFormatterTests
{
    [Fact]
    public void Uppercase_Email_Should_Be_Changed_To_Lowercase()
        => Execute(new Input(Name: "Bob", Email: "EMAIL"));

    [Fact]
    public void Untrimmed_Email_Should_Be_Trimmed()
        => Execute(new Input(Name: "Bob", Email: " email   "));

    private void  Execute(Input input)
    {
        var result = new EmailFormatter().CanonicalizeEmail(input);
        var expected = new Input(Name: "Bob", Email: "email");
        result.Should().BeEquivalentTo(
            (Result<Input, Error>.Success)Result.Success(expected));
    }
}
