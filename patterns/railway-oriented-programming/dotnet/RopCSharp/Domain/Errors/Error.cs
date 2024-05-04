namespace RopCSharp.Domain.Errors;

internal sealed record Error(string Code, string? Desccription = null)
{
    public static readonly Error None = new Error(string.Empty);
    public static readonly Error NullValue = new Error("NULL_VALUE", "Null reference.");
}
