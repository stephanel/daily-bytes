using BenchmarkDotNet.Running;

namespace ValidPalindrome.Benchmarks;

internal static class Program
{
    static void Main(string[] args)
    {
        try
        {
            BenchmarkRunner.Run<PalindromBenchmarks>();

            Console.WriteLine("Done");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
