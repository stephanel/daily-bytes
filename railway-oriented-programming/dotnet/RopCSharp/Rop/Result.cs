namespace RopCSharp.Rop;

internal class Result<TValue, TError>
{
    public readonly TValue? Value = default;
    public readonly TError? Error = default;

    public bool IsSuccess { get; private set; }

    private Result(TValue value)
    {
        Value = value;
        IsSuccess = true;
    }

    private Result(TError error)
    {
        Error = error;
    }

    public static implicit operator Result<TValue, TError>(TValue value)
        => new Result<TValue, TError>(value);

    public static implicit operator Result<TValue, TError>(TError error)
        => new Result<TValue, TError>(error);
}


internal static class ResultExtensions
{
    internal static Result<TValue, TError> OnSuccess<TValue, TError>(
        this Result<TValue, TError> result,
        Func<TValue, Result<TValue, TError>> onSuccess)
    {
        if (result.IsSuccess)
        {
            onSuccess(result.Value!);
        }
        return result;
    }

    internal static Result<TValue, TError> OnFailure<TValue, TError>(
        this Result<TValue, TError> result,
        Func<TError, Result<TValue, TError>> onFailure)
    {
        if (!result.IsSuccess)
        {
            onFailure(result.Error!);
        }
        return result;
    }

    internal static Result<TValue, TError> Match<TValue, TError>(
        this Result<TValue, TError> result,
        Func<TValue, Result<TValue, TError>> success,
        Func<TError, Result<TValue, TError>> error)
        => result.IsSuccess ? success(result.Value!) : error(result.Error!);
}