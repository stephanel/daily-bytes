namespace RopCSharp;

internal class Validation
{
    internal Result<Input, Error> CombineValidation(Input input)
      => NameIsNotEmpty(input)
        .OnSuccess(EmailIsNotEmpty)
        .OnSuccess(EmailDoesNotExceedMaxLength)
        ;

    internal Result<Input, Error> NameIsNotEmpty(Input input)
      => string.IsNullOrWhiteSpace(input.Name)
          ? Name.IsEmpty
          : input;

    internal Result<Input, Error> EmailIsNotEmpty(Input input)
        => string.IsNullOrWhiteSpace(input.Email)
            ? Email.IsEmpty
            : input;

    internal Result<Input, Error> EmailDoesNotExceedMaxLength(Input input)
        => input.Email.Length <= 50
            ? input
            : Email.TooLong;
}