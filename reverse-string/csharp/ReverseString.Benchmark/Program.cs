using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using ReverseString.Application;

BenchmarkRunner.Run<ReverseStringBenchmark>();

[MemoryDiagnoser]
public class ReverseStringBenchmark
{
    private readonly string randomText;

    public ReverseStringBenchmark()
    {
        int length = 256;
        Random random = new();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz 0123456789";
        randomText = new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    [Benchmark]
    public void ReverseStringUsingForLoop()
        => new ReverseStringForLoop().Reverse(randomText);

    [Benchmark]
    public void ReverseStringUsingLinq()
        => new ReverseStringLinq().Reverse(randomText);

    [Benchmark]
    public void ReverseStringUsingSpan()
        => new ReverseStringSpan().Reverse(randomText);
}