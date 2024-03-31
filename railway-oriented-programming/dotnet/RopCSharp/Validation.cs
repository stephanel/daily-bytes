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
          ? Failure(Domain.Errors.Name.IsEmpty)
          : Success(input);

    internal Result<Input, Error> EmailIsNotEmpty(Input input)
        => string.IsNullOrWhiteSpace(input.Email)
            ? Failure(Domain.Errors.Email.IsEmpty)
            : Success(input);

    internal Result<Input, Error> EmailDoesNotExceedMaxLength(Input input)
        => input.Email.Length <= 50
            ? Success(input)
            : Failure(Domain.Errors.Email.TooLong);
}