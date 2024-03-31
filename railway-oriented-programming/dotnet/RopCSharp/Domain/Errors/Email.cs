namespace RopCSharp.Domain.Errors;

internal class Email
{
    public static readonly Error IsEmpty = new Error("EMAIL_IS_EMPTY", "Email must not be empty.");

    public static readonly Error TooLong = new Error("EMAIL_IS_TOO_LONG", "Email must not be longer than 50 chars.");
}
