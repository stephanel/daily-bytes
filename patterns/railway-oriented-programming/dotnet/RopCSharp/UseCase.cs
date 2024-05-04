namespace RopCSharp;

internal class UseCase
{
    private readonly Validation _validation;
    private readonly EmailFormatter _emailFormatter;

    public UseCase(Validation validation, EmailFormatter emailFormatter)
    {
        _validation = validation;
        _emailFormatter = emailFormatter;
    }

    public record Input(string Name, string Email);

    public Result<Input, Error> Handle(Input input)
    {
        return ((Result<Input, Error>)input)
            .Match(_validation.NameIsNotEmpty, e => e)
            .Match(_validation.EmailIsNotEmpty, e => e)
            .Match(_validation.EmailDoesNotExceedMaxLength, e => e)
            .Match(_emailFormatter.CanonicalizeEmail, e => e);
    }
}
