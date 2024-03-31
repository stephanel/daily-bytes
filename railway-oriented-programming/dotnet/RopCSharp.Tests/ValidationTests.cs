namespace RopCSharp.Tests;

public class ValidationTests
{
    [Fact]
    public void Empty_Name_Should_Not_Pass_Validation()
    {
        Input input = new(Name: "", Email: "");
        Result<Input, Error> result = new Validation().NameIsNotEmpty(input);
        result.Should().BeEquivalentTo(
            (Result<Input, Error>.Failure)Result.Failure(Domain.Errors.Name.IsEmpty));
    }

    [Fact]
    public void Empty_Email_Should_Not_Pass_Validation()
    {
        Input input = new(Name: "Bob", Email: "");
        Result<Input, Error> result = new Validation().EmailIsNotEmpty(input);
        result.Should().BeEquivalentTo(
            (Result<Input, Error>.Failure)Result.Failure(Domain.Errors.Email.IsEmpty));
    }

    [Fact]
    public void Too_Long_Email_Should_Not_Pass_Validation()
    {
        Input input = new(Name: "Bob", Email: string.Concat(Enumerable.Repeat("email", 11)));
        Result<Input, Error> result = new Validation().EmailDoesNotExceedMaxLength(input);
        result.Should().BeEquivalentTo(
            (Result<Input, Error>.Failure)Result.Failure(Domain.Errors.Email.TooLong));
    }

    [Fact]
    public void Validation_Should_Pass_When_Neither_Name_Nor_Email_Is_Empty()
    {
        Input input = new(Name: "Bob", Email: "email");
        Result<Input, Error> result = new Validation().CombineValidation(input);

        result.Should().BeOfType<Result<Input, Error>.Success>();
    }
}
