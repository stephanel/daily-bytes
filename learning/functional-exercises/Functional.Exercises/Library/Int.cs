namespace Functional.Exercises.Library;

internal static class Int
{
    public static Option<int> Parse(this string value)
        => int.TryParse(value, out int result) ? Some(result) : None;
}