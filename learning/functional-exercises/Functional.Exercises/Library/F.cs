using System.Collections.Immutable;
using System.Collections.Specialized;

namespace Functional.Exercises.Library;

internal static class F
{
    internal struct NoneType { }

    public static Option<R> Bind<T, R>(this Option<T> opt, Func<T, Option<R>> f)
        => opt.Match(() => None, t => f(t));

    public static IEnumerable<R> Bind<T, R>(this IEnumerable<T> source, Func<T, IEnumerable<R>> f)
    {
        foreach(T t in source)
            foreach(R r in f(t))
                yield return r;
    }

    /// <summary>
    /// Projects each element of a sequence into an option and flattens the resulting options into a single sequence of
    /// values.
    /// </summary>
    /// <remarks>This method is commonly used to chain operations that may or may not return a value,
    /// effectively filtering out elements where the function returns an empty option. It is functionally similar to
    /// SelectMany for option types.</remarks>
    /// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
    /// <typeparam name="R">The type of the elements returned by the option-producing function.</typeparam>
    /// <param name="list">The sequence of elements to bind.</param>
    /// <param name="func">A function to apply to each element that returns an option of the result type.</param>
    /// <returns>An enumerable sequence containing the values from each option returned by the function, excluding options with
    /// no value.</returns>
    public static IEnumerable<R> Bind<T, R>(this IEnumerable<T> list, Func<T, Option<R>> func)
        => list.Bind(t => func(t).AsEnumerable());

    /// <summary>
    /// Projects the value of the option into a sequence of results using the specified mapping function, or returns an
    /// empty sequence if the option has no value.
    /// </summary>
    /// <remarks>This method enables chaining of operations on optional values that produce sequences,
    /// following the monadic bind pattern. It is commonly used to flatten nested option and sequence
    /// operations.</remarks>
    /// <typeparam name="T">The type of the value contained in the option.</typeparam>
    /// <typeparam name="R">The type of the elements in the resulting sequence.</typeparam>
    /// <param name="opt">The option to bind. If the option has a value, the mapping function is applied; otherwise, an empty sequence is
    /// returned.</param>
    /// <param name="func">A mapping function to apply to the value of the option, returning a sequence of results. Cannot be null.</param>
    /// <returns>A sequence of results from applying the mapping function to the option's value, or an empty sequence if the
    /// option has no value.</returns>
    public static IEnumerable<R> Bind<T, R>(this Option<T> opt, Func<T, IEnumerable<R>> func)
        => opt.AsEnumerable().Bind(func);


    public static Func<T, Unit> ToFunc<T>(this Action<T> action)
        => (t) => { action(t); return default; };

    public static IEnumerable<Unit> ForEach<T>(this IEnumerable<T> source, Action<T> action)
        => source.Map(action.ToFunc()).ToImmutableList();

    public static Option<Unit> ForEach<T>(this Option<T> opt, Action<T> action)
        => Map(opt, action.ToFunc());

    /// <summary>
    /// the Return function for IEnumerable&lt;T&gt;
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items"></param>
    /// <returns></returns>
    public static IEnumerable<T> List<T>(params T[] items)
        => items.ToImmutableList();

    public static Option<string> Lookup(this NameValueCollection source, string key)
        => source[key]!;

    public static Option<T> Lookup<T>(this IDictionary<string, T> source, string key)
        => source.TryGetValue(key, out var result) ? Some(result) : None;

    public static Option<T> Lookup<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        => source.FirstOrDefault(predicate) is T result ? Some(result) : None;

    public static Option<R> Map<T, R>(this Option<T> optT, Func<T, R> f)
        => optT.Match(() => None, t => Some(f(t)));

    public static IEnumerable<R> Map<T, R>(this IEnumerable<T> source, Func<T, R> f)
        => source.Select(f);

    public static NoneType None => default;

    public static Option<TEnum> Parse<TEnum>(this string value) where TEnum : struct, Enum
        => Enum.TryParse<TEnum>(value, out var result) ? Some(result) : None;

    /// <summary>
    /// the Return function for Option&lt;T&gt;
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Option<T> Some<T>(T value) => new Option<T>(value);

    public static Func<Unit> ToFunc(this Action action)
        => () => { action(); return default; };

    public static Option<T> Where<T>(this Option<T> opt, Func<T, bool> f)
        => opt.Match(() => None, t => f(t) ? opt : None);
}
