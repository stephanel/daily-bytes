namespace RopCSharp.Test;

public class UseCasesTests
{
    private readonly Validation _validation = new();
    private readonly EmailFormatter _emailFormatter = new();

    [Fact]
    public void UseCase_Should_Successfully_Handle()
    {
        Input input = new(Name: "Bob", Email: "email");
        var result = new UseCase(_validation, _emailFormatter).Handle(input);
        result.Should().BeEquivalentTo(
            (Result<Input, Error>.Success)Result.Success(input));

    }
}