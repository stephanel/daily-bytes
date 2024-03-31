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
        var inputResult = new Result<Input, Error>.Success(input);

        Result<Input, Error> result = inputResult
            .OnSuccess(_validation.NameIsNotEmpty)
            .OnSuccess(_validation.EmailIsNotEmpty)
            .OnSuccess(_validation.EmailDoesNotExceedMaxLength)
            .OnSuccess(_emailFormatter.CanonicalizeEmail)
            ;

        return result;
    }

    //private Result<Input, Error> NameIsNotEmpty(Input input)
    //    => string.IsNullOrWhiteSpace(input.Name)
    //        ? Success(input)
    //        : Failure(Domain.Errors.Name.IsEmpty);

    //private Result<Input, Error> EmailIsNotEmpty(Input input)
    //    => string.IsNullOrWhiteSpace(input.Email)
    //        ? Success(input)
    //        : Failure(Domain.Errors.Email.IsEmpty);

    //private Result<Input, Error> EmailDoesNotExceedMaxLength(Input input)
    //    => input.Email.Length <= 50
    //        ? Success(input)
    //        : Failure(Domain.Errors.Email.TooLong);

    private Result<Input, Error> CanonicalizeEmail(Input input)
    {
        return _emailFormatter.CanonicalizeEmail(input);
        //return input with { Email = input.Email.Trim().ToLower() });
    }
}
