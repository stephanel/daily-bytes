namespace Common.Extensions.Rop;

public sealed record Error(int Code, string? Desccription = null)
{
    public static readonly Error None = new Error(0);
    public static readonly Error NullValue = new Error(999, "Null reference.");
    public static readonly Error InternalError = new Error(500, "Internal error.");   
    public static readonly Error NotFound = new Error(404, "Resource not found.");
    public static readonly Error BadRequest = new(400, "Bad request.");
}