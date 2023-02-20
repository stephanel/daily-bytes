using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Running;
using System.Text.RegularExpressions;

BenchmarkRunner.Run<CountingOccurencesInString>();

[MemoryDiagnoser]
public class CountingOccurencesInString
{
    private string randomText;

    [Params(1000, 10000, 100000)]
    public int stringLength;

    public readonly string search = "E";

    [GlobalSetup]
    public void Setup()
    {
        Random random = new();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz 0123456789";
        randomText = new string(Enumerable.Repeat(chars, stringLength)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    [Benchmark(Description = "String.Count(c => c.Equals(search))", Baseline = true)]
    public int Solution1()
    {
        return randomText.Count(c => c.Equals(search));
    }

    [Benchmark(Description = @"String.Length - String.Replace(search, "").Length")]
    public int Solution2()
    {
        return randomText.Length - randomText.Replace(search, string.Empty).Length;
    }

    [Benchmark(Description = "String.Split(search).Length - 1")]
    public int Solution3()
    {
        return randomText.Split(search).Length - 1;
    }

    [Benchmark(Description = "foreach(var c in String) count++")]
    public int Solution4()
    {
        var count = 0;
        var searchedChar = char.Parse(search);
        foreach (var c in randomText)
        {
            if (c == searchedChar)
            {
                count++;
            }
        }
        return count;
    }

    [Benchmark(Description = "foreach(var c in String.ToCharArray()) count++")]
    public int Solution5()
    {
        var count = 0;
        var searchedChar = char.Parse(search);
        foreach (var c in randomText.ToCharArray())
        {
            if (c == searchedChar)
            {
                count++;
            }
        }
        return count;
    }

    [Benchmark(Description = "for (int i = 0; i < length; i++) count++")]
    public int Solution6()
    {
        var count = 0;
        char[] testchars = randomText.ToCharArray();
        int length = testchars.Length;
        for (int i = 0; i < length; i++)
        {
            if (testchars[i] == '/')
                count++;
        }
        return count;
    }

    [Benchmark(Description = "for (int i = length - 1; i >= 0; i--) count++")]
    public int Solution7()
    {
        var count = 0;
        char[] testchars = randomText.ToCharArray();
        int length = testchars.Length;
        for (int i = length - 1; i >= 0; i--)
        {
            if (testchars[i] == '/')
                count++;
        }
        return count;
    }

    [Benchmark(Description = "while(String.indexOf() != 1) count++")]
    public int Solution8()
    {
        var count = 0;
        var position = 0;
        var searchedChar = char.Parse(search);

        while ((position = randomText.IndexOf(search, position, StringComparison.InvariantCulture)) != -1)
        {
            position += search.Length;
            count++;
        }
        return count;
    }

    [Benchmark(Description = "Regex(search).Matches(String).Count")]
    public int Solution9()
    {
        return new Regex(search).Matches(randomText).Count;
    }

    [Benchmark(Description = "Regex.Match(String, Regex.Escape(search)).Count")]
    public int Solution10()
    {
        return Regex.Matches(randomText, Regex.Escape(search)).Count;
    }

    [Benchmark(Description = "foreach (var c in String.AsSpan()) count++")]
    public int Solution11()
    {
        var count = 0;
        var searchedChar = char.Parse(search);
        foreach (var c in randomText.AsSpan())
        {
            if (c == searchedChar)
            {
                count++;
            }
        }
        return count;
    }
}