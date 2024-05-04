using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using ValidPalindrome.Application;

namespace ValidPalindrome.Benchmarks;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest, MethodOrderPolicy.Declared)]
[RankColumn]
public class PalindromBenchmarks
{
    private string _input = null!;

    [Params(5, 50, 100)]
    public int Size { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        _input = new PalindromeGenerator().GenerateRandomPalindrome(Size);
    }

    [Benchmark]
    public bool UsingAsciiCodeLinq() => new Palindrome().ValidateUsingAsciiCodeLinq(_input);

    [Benchmark]
    public bool UsingAsciiCodeForLoop() => new Palindrome().ValidateUsingAsciiCodeForLoop(_input);

    [Benchmark]
    public bool UsingRegexForLoop() => new Palindrome().ValidateUsingRegexForLoop(_input);

    [Benchmark]
    public bool UsingAsciiCodeForLoopAlternativeLogic() => new Palindrome().ValidateUsingAsciiCodeForLoopAlternativeLogic(_input);
}