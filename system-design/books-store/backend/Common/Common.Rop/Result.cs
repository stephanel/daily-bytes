namespace Common.Rop;

public record Result<TValue, TError>
{
    public readonly TValue? Value = default;
    public readonly TError? Error = default;

    public bool IsSuccess { get; private set; }
    public bool IsFailure => !IsSuccess;

    private Result(TValue value)
    {
        Value = value;
        IsSuccess = true;
    }

    private Result(TError error)
    {
        Error = error;
    }

    public static implicit operator Result<TValue, TError>(TValue value) => new Result<TValue, TError>(value);
    public static implicit operator TValue(Result<TValue, TError> result) => result.Value!;

    public static implicit operator Result<TValue, TError>(TError error) => new Result<TValue, TError>(error);
    public static implicit operator TError(Result<TValue, TError> result) => result.Error!;
}