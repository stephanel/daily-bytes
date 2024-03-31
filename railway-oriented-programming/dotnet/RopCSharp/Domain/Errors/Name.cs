namespace RopCSharp.Domain.Errors;

internal class Name
{
    public static readonly Error IsEmpty = new Error("NAME_IS_EMPTY", "Name must not be empty.");
}
