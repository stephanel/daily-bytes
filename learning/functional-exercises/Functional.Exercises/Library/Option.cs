namespace Functional.Exercises.Library;

internal struct Option<T>
{
    public T Value { get; private init; }

    readonly bool _isSome;

    internal Option(T value)
    {
        Value = value ?? throw new ArgumentNullException();
        _isSome = true;
    }

    public static implicit operator Option<T>(NoneType _) => default;

    public static implicit operator Option<T>(T value)
        => value is null ? None : Some(value);

    public R Match<R>(Func<R> none, Func<T, R> some)
        => _isSome ? some(Value!) : none();

    public IEnumerable<T> AsEnumerable()
    {
        if (_isSome) yield return Value!;
    }
}