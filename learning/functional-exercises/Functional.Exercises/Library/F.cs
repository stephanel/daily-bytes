using System.Collections.Immutable;
using System.Collections.Specialized;

namespace Functional.Exercises.Library;

internal static class F
{
    internal struct NoneType { }

    public static Option<T> Some<T>(T value) => new Option<T>(value);

    public static NoneType None => default;

    public static Option<R> Bind<T, R>(this Option<T> opt, Func<T, Option<R>> f)
        => opt.Match(() => None, t => f(t));

    public static IEnumerable<R> Bind<T, R>(this IEnumerable<T> source, Func<T, IEnumerable<R>> f)
    {
        foreach(T t in source)
            foreach(R r in f(t))
                yield return r;
    }

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

    public static Option<TEnum> Parse<TEnum>(this string value) where TEnum : struct, Enum
        => Enum.TryParse<TEnum>(value, out var result) ? Some(result) : None;

    public static Func<Unit> ToFunc(this Action action)
        => () => { action(); return default; };

    public static Option<T> Where<T>(this Option<T> opt, Func<T, bool> f)
        => opt.Match(() => None, t => f(t) ? opt : None);
}
