namespace RopCSharp;

internal class EmailFormatter
{
    internal Result<Input, Error> CanonicalizeEmail(Input input)
        => input with { Email = input.Email.Trim().ToLower() };
}
