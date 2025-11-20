namespace Functional.Exercises.Features;

internal struct Age
{
    public int Value { get; }

    public static Option<Age> Create(int age) => IsValid(age) ? F.Some(new Age(age)) : F.None;

    private Age(int value) => Value = value;

    private static bool IsValid(int age) => 0 <= age && age < 120;

    public static bool operator <(Age left, Age right) => left.Value < right.Value;
    public static bool operator >(Age left, Age right) => left.Value > right.Value;

    public static bool operator <(Age left, int right) => left < new Age(right);
    public static bool operator >(Age left, int right) => left > new Age(right);
}