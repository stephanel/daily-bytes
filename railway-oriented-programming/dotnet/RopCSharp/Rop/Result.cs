namespace RopCSharp.Rop;

internal abstract class Result<TSuccess, TFailure>
{
    private Result() { }

    public abstract Result<TNextSuccess, TFailure> OnSuccess<TNextSuccess>(Func<TSuccess, Result<TNextSuccess, TFailure>> onSuccess);

    public abstract TReturn Handle<TReturn>(Func<TSuccess, TReturn> onSuccess, Func<TFailure, TReturn> onFailure);

    public sealed class Success : Result<TSuccess, TFailure>
    {
        public TSuccess Value { get; private init; }

        public Success(TSuccess value) => Value = value;

        public override Result<TNextSuccess, TFailure> OnSuccess<TNextSuccess>(Func<TSuccess, Result<TNextSuccess, TFailure>> onSuccess)
            => onSuccess(Value);

        public override TReturn Handle<TReturn>(Func<TSuccess, TReturn> onSuccess, Func<TFailure, TReturn> onFailure)
            => onSuccess(Value);
            }

    public sealed class Failure : Result<TSuccess, TFailure>
    {
        public TFailure Value { get; private init; }

        public Failure(TFailure value) => Value = value;

        public override Result<TNextSuccess, TFailure> OnSuccess<TNextSuccess>(Func<TSuccess, Result<TNextSuccess, TFailure>> onSuccess)
            => Result.Failure(Value);

        public override TReturn Handle<TReturn>(Func<TSuccess, TReturn> onSuccess, Func<TFailure, TReturn> onFailure)
            => onFailure(Value);
    }

    public static implicit operator Result<TSuccess, TFailure>(Result.GenericSuccess<TSuccess> ok)
        => new Success(ok.Value);

    public static implicit operator Result<TSuccess, TFailure>(Result.GenericFailure<TFailure> error)
        => new Failure(error.Value);
}

internal static class Result
{
    public static Result<TSuccess, TFailure> Success<TSuccess, TFailure>(TSuccess ok) => Success(ok);

    public static GenericSuccess<TSuccess> Success<TSuccess>(TSuccess ok) => new(ok);

    public static Result<TSuccess, TFailure> Failure<TSuccess, TFailure>(TFailure error) => Result.Failure(error);

    public static GenericFailure<TFailure> Failure<TFailure>(TFailure error) => new(error);

    public readonly struct GenericSuccess<T>
    {
        public T Value { get; }

        public GenericSuccess(T value)
        {
            Value = value;
        }
    }

    public readonly struct GenericFailure<T>
    {
        public T Value { get; }

        public GenericFailure(T value)
        {
            Value = value;
        }
    }
}
